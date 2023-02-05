import Vue, { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import './style.css'
import App from './App.vue'

import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

import Home from './pages/Home/Home.vue';
import TrainingAddDetails from './pages/TrainingAddDetails/TrainingAddDetails.vue';

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)

var routes = [
    { path: '/', component: Home },
    { path: '/training-add-details/:id', component: TrainingAddDetails }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
  })

createApp(App).use(router).mount('#app')
