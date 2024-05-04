<template>
    <nav class="navbar">
        <ul>
            <li><router-link :to="{path: '/home', query: {userId: userId} }">Home</router-link></li>
            <li><router-link :to="{path: '/searchpage', query: {userId: userId} }">Search</router-link></li>
            <li><router-link :to="{path: '/'}">Logout</router-link></li>
        </ul>
    </nav>
</template>

<script>
    export default {
        props: ['userId'],
        methods: {
          
        }
    }
</script>

<style scoped>
    .navbar {
        display: flex;
        flex-direction: column;
        background-color: #0094ff;
        padding: 10px;
        margin: auto;
    }

        .navbar ul {
            list-style-type: none;
            margin: 0;
            padding: 0;
        }

            .navbar ul li {
                display: inline;
                margin-right: 20px;
            }

                .navbar ul li:last-child {
                    margin-right: 0;
                }

                .navbar ul li a {
                    color: white;
                    text-decoration: none;
                }

                    .navbar ul li a:hover {
                        text-decoration: underline;
                        color: #ddd;
                    }

        .navbar button {
            background-color: transparent;
            border: none;
            color: white;
            cursor: pointer;
        }

            .navbar button:hover {
                text-decoration: underline;
            }
</style>
