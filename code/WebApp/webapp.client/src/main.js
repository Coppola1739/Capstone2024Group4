import './assets/main.css';

import { createApp } from 'vue';
import App from './App.vue';
import { createRouter, createWebHistory } from 'vue-router';
import SourcePage from './components/SourcePage.vue';
import HomePage from './components/HelloWorld.vue';

const routes = [
    {
        path: '/source/:id',
        name: 'SourcePage',
        component: SourcePage,
        props: true
    },
    {
        path: '/',
        name: 'HomePage',
        component: HomePage
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
