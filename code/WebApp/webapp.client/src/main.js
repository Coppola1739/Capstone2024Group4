import './assets/main.css';

import { createApp } from 'vue';
import App from './App.vue';
import { createRouter, createWebHistory } from 'vue-router';
import SourcePage from './components/SourcePage.vue';
import HomePage from './components/HelloWorld.vue';
import LoginPage from './components/LoginPage.vue';
import SignupPage from './components/SignupPage.vue';

const routes = [
    {
        path: '/source/:id',
        name: 'SourcePage',
        component: SourcePage,
        props: true
    },
    {
        path: '/home',
        name: 'HomePage',
        component: HomePage
    },
    {
        path: '/',
        name: 'LoginPage',
        component: LoginPage
    },
    {
        path: '/signup',
        name: 'SignupPage',
        component: SignupPage
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, from, next) => {
    console.log('Navigating to:', to.fullPath);
    next();
});

createApp(App).use(router).mount('#app');
