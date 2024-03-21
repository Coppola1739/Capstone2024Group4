<template>
    <router-view :userId=1></router-view>
    <div class="login-container">
        <h2>Login</h2>
        <input type="text" name="username" placeholder="Username" ref="usrField" required>
        <input type="password" name="password" placeholder="Password" ref="passField" required>
        <input @click="getUserIdBy" type="submit" value="Login">
        
        <router-link to="/signup" custom
                     v-slot="{ href, navigate, isActive }">
            <a style="display: block; text-align: center; margin-top: 20px" :href="href" @click="navigate">
                Create an Account
            </a>
        </router-link>
    </div>
</template>

<script>
    export default {
        methods: {
            async getUserIdBy() {
                try {
                    const response = await fetch(`/User/getUserIdByLogin?user=${this.$refs.usrField.value}&pass=${this.$refs.passField.value}`);
                    if (response.ok) {
                        alert('Login Credentials are valid');
                        const data = await response.json();

                        this.$router.push({
                            name: `HomePage`,
                            query: { userId: data } // Pass userId as a param
                        })
                    } else {
                        alert('Invalid Credentials');
                        console.error('Failed to fetch source details');
                    }
                } catch (error) {
                    console.error('Error', error);
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

    .login-container {
        width: 450px;
        margin: 0 auto;
        padding: 20px;
        background-color: #fff;
        border-radius: 5px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }

        .login-container h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        .login-container input[type="text"],
        .login-container input[type="password"] {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 3px;
            box-sizing: border-box;
        }

        .login-container input[type="submit"] {
            width: 100%;
            padding: 10px;
            background-color: #4CAF50;
            border: none;
            border-radius: 3px;
            color: white;
            cursor: pointer;
        }

            .login-container input[type="submit"]:hover {
                background-color: #45a049;
            }
</style>