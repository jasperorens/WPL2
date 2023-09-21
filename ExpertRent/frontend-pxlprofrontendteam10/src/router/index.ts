import { createRouter, createWebHistory } from 'vue-router'
//import ListingPage from '../views/ListingPage.vue';
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {      path: '/', name: 'Home', component: () => import('../views/HomePage.vue') },
    {      path: '/about', name: 'About', component: () => import('../views/AboutUsPage.vue') },
    {      path: '/login', name: 'Login', component: () => import('../views/LogInPage.vue') },
    {      path: '/listing', name: 'Listing', component: () => import('../views/ListingPage.vue')},
    {      path: '/cart', name: 'Cart', component: () => import('../views/CartPage.vue') },
    {      path: '/contact', name: 'Contact', component: () => import('../views/Contact.vue') },
    {      path: '/profile', name: 'Profile', component: () => import('../views/Profile.vue') },
    {      path: '/RegisterPage', name: 'RegisterPage', component: () => import('../views/RegisterPage.vue') },
    {      path: '/calendars', name: 'Calendars', component: () => import('../components/Calendars.vue') },
    {      path: '/upgradePage', name: 'UpgradePage', component: () => import('../views/UpgradePage.vue') },
    {      path: '/expertProfile', name: 'ExpertProfile', component: () => import('../views/ExpertProfile.vue') },
    {      path: '/viewProfile/:id', name: 'ViewProfile', component: () => import('../views/ViewProfile.vue') },
    {      path: '/cartPagePart2', name:'CartPagePart2', component: () => import('../views/CartPagePart2.vue') },
  ]
})

export default router
