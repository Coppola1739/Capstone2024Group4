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
                            query: { userId: data } 
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

<style scoped>
    body {
        font-family: Arial, sans-serif;
        background-color: #f4f4f4;
        height: 2%;
        margin: 4%;
    }

    .login-container {
        display: flex;
        width: 200%;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        background-color: #fff;
        border-radius: 5%;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        padding: 15%;
    }

        .login-container input[type="text"],
        .login-container input[type="password"] {
            width: 100%;
            padding: 10px;
            margin-bottom: 2%;
            border: 1px solid #ccc;
            border-radius: 3px;
            margin: 2%;
            box-sizing: border-box;
        }

        .login-container input[type="submit"] {
            width: 100%;
            padding: 2%;
            background-color: #4CAF50;
            border: none;
            border-radius: 3px;
            color: white;
            cursor: pointer;
        }

            .login-container input[type="submit"]:hover {
                background-color: #45a049;
            }
    @media screen and (max-width: 1020px) {
        .login-container {
            margin: 20%;
            padding: 10%;
            max-width: 70%;
        }
    }
</style>
