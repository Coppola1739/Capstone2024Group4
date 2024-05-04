<template>
    <router-view :userId=1></router-view>
    <div class="signup-container">
        <h2>Sign Up</h2>

        <label for="username">Enter a Username:</label>
        <input type="text" name="username" placeholder="Username" ref="usrField" required>

        <label for="password">Enter a Password:</label>
        <input type="password" name="password" placeholder="Password" ref="passField" required>
        <p class="password-requirements">Password must:</p>
        <ul class="password-requirements-list">
            <li>Be at least 6 characters long</li>
            <li>Contain at least one uppercase letter</li>
            <li>Contain at least one symbol [!@$%^&*()]</li>
        </ul>

        <label for="confirmPassword">Confirm Password:</label>
        <input type="password" name="confirmPassword" placeholder="Confirm Password" ref="confirmPassField" required>

        <input @click="validateFields" type="submit" value="Sign Up">

        <router-link to="/" custom v-slot="{ href, navigate, isActive }">
            <a style="display: block; text-align: center; margin-top: 20px" :href="href" @click="navigate">
                Already have an account? Login
            </a>
        </router-link>

    </div>
</template>

<script>
    export default {
        data() {
            return {
                passwordMismatch: false
            };
        },
        methods: {
            async validateFields() {
                var invalidFields = false;
                var password = this.$refs.passField.value;
                var alertMessage = "";

                if (!this.$refs.usrField.value || /\s/.test(this.$refs.usrField.value)) {
                    alertMessage += "Username cannot be empty or contain spaces.\n";
                    invalidFields = true;
                }
                if (!password || /\s/.test(password)) {
                    alertMessage += "Password cannot be empty or contain spaces.\n";
                    invalidFields = true;
                }
                if (password.length < 6) {
                    alertMessage += "Password must be at least 6 characters long.\n";
                    invalidFields = true;
                }
                if (!/[A-Z]/.test(password)) {
                    alertMessage += "Password must contain at least one uppercase letter.\n";
                    invalidFields = true;
                }
                if (!/^[!@$%^&*()]+$/.test(password)) {
                    alertMessage += "Password must contain at least one of the following symbols (only these symbols): !@$%^&*().\n";
                    invalidFields = true;
                }
                if (this.$refs.passField.value !== this.$refs.confirmPassField.value) {
                    alertMessage += "Passwords do not match.\n";
                    this.passwordMismatch = true;
                    invalidFields = true;
                }

                if (invalidFields) {
                    alert(alertMessage);
                    return;
                }

                const formData = new FormData();
                formData.append('UserName', this.$refs.usrField.value);
                formData.append('Password', password);

                try {
                    const response = await fetch('User/createAccount', {
                        method: 'POST',
                        body: formData,
                    });

                    if (response.status === 200) {
                        const result = await response.json();
                        alert('Account successfully created!');
                        this.$router.push({
                            name: `LoginPage`
                        });
                    } else if (response.status === 409) {
                        const result = await response.json();
                        alert('Username already exists');
                    } else {
                        console.error('Account creation failed');
                        alert('Account creation failed');
                        console.log(response);
                    }

                } catch (error) {
                    console.error('Error uploading file:', error);
                    alert('Error uploading file: ' + error.message);
                }
            }
        }

    }
</script>


<style>
    body {
        font-family: Arial, sans-serif;
        background-color: #f4f4f4;
    }

    .signup-container {
        width: 450px;
        flex: auto;
        margin: 0 auto;
        padding: 20px;
        background-color: #fff;
        border-radius: 5px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }

        .signup-container h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        .signup-container input[type="text"],
        .signup-container input[type="password"] {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 3px;
            box-sizing: border-box;
        }

        .signup-container input[type="submit"] {
            width: 100%;
            padding: 10px;
            background-color: #4CAF50;
            border: none;
            border-radius: 3px;
            color: white;
            cursor: pointer;
        }

            .signup-container input[type="submit"]:hover {
                background-color: #45a049;
            }
</style>