import { createApp } from 'vue';
import { createPinia } from 'pinia';
import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";
import { library } from '@fortawesome/fontawesome-svg-core';
import { faUserSecret, faHouse, faStreetView, faAddressCard, faShoppingCart, faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import App from './App.vue'
import router from './router'

const app = createApp(App);


import './assets/main.css'
library.add(faUserSecret, faHouse, faStreetView, faAddressCard, faShoppingCart, faMagnifyingGlass);

app.use(createPinia()) //Pinia
//alt + enter en klikken op component voor deze te installeren
app.component('font-awesome-icon', FontAwesomeIcon)
app.use(router)

app.mount('#app')
