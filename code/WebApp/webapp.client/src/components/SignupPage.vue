
<style>
    body {
        font-family: Arial, sans-serif;
        background-color: #f4f4f4;
    }

    .signup-container {
        width: 450px;
        flex:auto;
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
<template>
    <router-view :userId=1></router-view>
    <div class="signup-container">
        <h2>Sign Up</h2>

        <label for="username">Enter a Username:</label>
        <input type="text" name="username" placeholder="Username" ref="usrField" required>

        <label for="password">Enter a Password:</label>
        <input type="password" name="password" placeholder="Password" ref="passField" required>
        <input @click="ValidateFields" type="submit" value="Sign Up">
        
        <router-link to="/" custom
                     v-slot="{ href, navigate, isActive }">
            <a style="display: block; text-align: center; margin-top: 20px" :href="href" @click="navigate">
                Already have an account? Login
            </a>
        </router-link>

    </div>
</template>

<script>
    export default {
        data() {

        },
        methods: {
            async ValidateFields() {
                var invalidFields = false;

                if (!this.$refs.usrField.value || /\s/.test(this.$refs.usrField.value)) {
                    alert("UserName cannot be Empty or contain spaces")
                    invalidFields = true;
                }
                if (!this.$refs.passField.value || /\s/.test(this.$refs.passField.value)) {
                    alert("Password cannot be Empty or contain spaces")
                    invalidFields = true;
                }
                if (invalidFields) {
                    return;
                }


                const formData = new FormData();
                formData.append('UserName', this.$refs.usrField.value);
                formData.append('Password', this.$refs.passField.value);


                try {
                    const response = await fetch('User/createAccount', {
                        method: 'POST',
                        body: formData,
                    });

                    if (response.ok) {
                        const result = await response.json();
                        alert('Account successfully created!');
                        this.$router.push({
                            name: `LoginPage`
                        })
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
