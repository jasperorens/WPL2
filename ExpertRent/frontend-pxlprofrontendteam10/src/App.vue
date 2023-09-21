<script>
import {RouterView} from 'vue-router';
import {useExpertStore} from "@/stores/expertStore.js";
import { library } from '@fortawesome/fontawesome-svg-core';
import { faRocket } from '@fortawesome/free-solid-svg-icons';
library.add(faRocket);
/*import ViewProfile from "@/views/viewProfile.vue";*/
let loginNumber = 0;

export default {
    data(){
        return{
            store: useExpertStore(),
            loginNumber,
            experts:[],
            loggedin:[],
            randomNumbers:[],
            path:'/login',
            isAdmin: false,
          GotadminBadges: {
            admin: "false",
          },
          loadedDataAdminBadge: {
          },

        }
    },
    name: 'App',

    components: {
        RouterView,
    },

    /* props: {
      usernameIfExpert: {
        type: String,
        required: true,
      },
    }, */



    mounted() {

        this.alreadyLoggedIn();
        /* /////////Hamburger Menu///////////*/
        const menuBtn = document.querySelector('.hamburger');
        const hamburgerMenu = document.querySelector('.hamburgerMenu');
        let menuOpen = false;
        menuBtn.addEventListener('click', () => {
            if (!menuOpen) {
                menuBtn.classList.add('open');
                menuOpen = true;
                hamburgerMenu.style.display = 'flex';
            } else {
                menuBtn.classList.remove('open');
                menuOpen = false;
                hamburgerMenu.style.display = 'none';
            }
        });


        function toggleMenu() {
            if (!menuOpen) {
                menuBtn.classList.add('open');
                menuOpen = true;
                hamburgerMenu.style.display = 'flex';
            } else {
                menuBtn.classList.remove('open');
                menuOpen = false;
                hamburgerMenu.style.display = 'none';
            }
        }
        window.addEventListener('resize', () => {
            if (menuOpen && window.innerWidth >= 880) {
                toggleMenu();
            }
        });

    },
    beforeMount() {
        this.store.assignAllUsers();
        this.store.assignAllExperts();
        this.store.assignUserExperts();
        
        /* /////////No idea///////////*/
        this.$nextTick(() =>{
                console.log(this.$children)
                console.log(this.$refs)
                const childComponent =  this.$refs.routerview;
                console.log('usernameIfExpert');


            }
        );
    },




    /*computed: {
      logoUrl() {
        if (this.isAnExpert){
          return require('./assets/expertrent.png');
        } else {
          return require('./assets/expertrent_pro.png');
        }
      }
    }, */

    methods:{
      async getadminBadge(id) {
        try {
          await fetch("https://localhost:7155/api/Badge/getBadge?id="+id, {
            method: 'GET',
            headers: {
              'Accept': 'application/json',
            },
          }).then(response =>{
            return response.json()
          }).then(data=>{
            this.loadedDataAdminBadge = JSON.parse(data).slice(1, -1).split(',').map(pair => {
              const [, value] = pair.split(':');
              return value.trim().split('}')[0];
            });
            this.GotadminBadges.admin = this.loadedDataAdminBadge[1];
            console.log('GotAdminBadges',this.GotadminBadges.admin)
            localStorage.setItem('adminRights', this.GotadminBadges.admin)
          })
        } catch (error) {
          console.error(error)
        }

      },


        alreadyLoggedIn(){
            //alert(localStorage.getItem('userIsExpert'));

            let id = localStorage.getItem("id")
            if(id !== null) {
                const logintext = document.getElementById("Login");
                logintext.innerHTML = "Profile";
                loginNumber = 1;
                document.getElementById("upgradebutton").style.display = "flex";
                document.getElementById("upgradebuttonHM").style.display = "flex";
                this.handleUsernameIfExpert(id);
            }
        },

        closeWelcome() {
            this.$refs.welcomeBox.style.display="none"
        },
        reloadPage() {
            const logintext = document.getElementById("Login");
            logintext.innerHTML = "Login";
            document.getElementById("upgradebutton").style.display = "none";
            document.getElementById("upgradebuttonHM").style.display = "none";
            const routerLink = document.querySelector('#loginbutton');
            routerLink.setAttribute('to', '/login');
            this.path = '/login';


        },



        splitBadges(Array){
            return Array.split(',')
        },
        handleSuccessfulLogin(){
            this.$refs.welcomeBox.style.display = "flex";

            const logintext = document.getElementById("Login");
            logintext.innerHTML = "Profile";
            loginNumber = 1;

            const logintextHM = document.getElementById("LoginHM")
            logintextHM.innerHTML = "Profile";
            loginNumber = 1;
            document.getElementById("upgradebutton").style.display = "flex";
            document.getElementById("upgradebuttonHM").style.display = "flex";

            /*const routerLink = document.querySelector('#loginbutton');
            routerLink.setAttribute('to', '/profile');*/
            /*if(true)
            {
              this.path = '/ExpertProfile';
            }
            else
            {
              this.path = '/profile';
            }*/

        },
        /*experts() {
            return this.store.experts;
        },*/

        handleUsernameIfExpert(id) {
            //console.log("Id if expert:", id);

            let isAnExpert = false;
            let expertFound = false; // Flag variable to track if expert with matching username is found

            for (let i = 0; i < this.store.experts.length; i++) {
                if (this.store.experts[i].id === id) {
                    expertFound = true; // Set flag to true as expert is found
                    isAnExpert = true;

                    this.getadminBadge(id)
                  if(this.GotadminBadges.admin === "true")
                  {
                    this.isAdmin = true;
                    localStorage.setItem('adminRights', this.isAdmin);
                    console.log("latestAdmin = true", id);
                  }
                  else
                  {
                    console.log("latestAdmin = false", id);
                  }
                }
            }

            if (expertFound) { // Check if expert with matching id is found
                const routerLink = document.querySelector('#loginbutton');
                routerLink.setAttribute('to', '/ExpertProfile');
                this.path = '/ExpertProfile';
                console.log("this is an expert");
                document.getElementById("upgradebutton").style.display = "none";
                document.getElementById("upgradebuttonHM").style.display = "none";
                document.body.style.background = "#e3d9e7";
                this.$refs.expertRentLogo.src = 'src/assets/expertrent_pro.png';
                localStorage.setItem('userIsExpert', '1');

            } else {
                console.log("this is not an expert");
                this.isAdmin = false;
                localStorage.setItem('adminRights', false);
                console.log("AdminNoMore");
                const routerLink = document.querySelector('#loginbutton');
                routerLink.setAttribute('to', '/profile');
                this.path = '/profile';
                console.log("Deel3Works");
                document.body.style.background = "#B6D3D7";
                this.$refs.expertRentLogo.src = 'src/assets/expertrent.png';

            }
            if(localStorage.getItem('userIsExpert') === '1' && this.path !== '/ExpertProfile' )
            {

              this.getadminBadge(id)
              if(this.GotadminBadges.admin === "true")
              {
                this.isAdmin = true;
                localStorage.setItem('adminRights', this.isAdmin);
                console.log("latestAdmin = true");
              }
              else{
                localStorage.removeItem('adminRights');
              }
                const routerLink = document.querySelector('#loginbutton');
                routerLink.setAttribute('to', '/ExpertProfile');
                this.path = '/ExpertProfile';
                document.getElementById("upgradebutton").style.display = "none";
                document.getElementById("upgradebuttonHM").style.display = "none";
                document.body.style.background = "#e3d9e7";
                this.$refs.expertRentLogo.src = 'src/assets/expertrent_pro.png';

            }
        },

    },

}
// Define a function named 'loadHero' that loads expert data from a specified URL and displays it on the webpage.
/*function loadExperts(id) {
  // Set the URL to fetch data from.
  let url = 'http://localhost:3000/experts';

  // Use the 'fetch' function to make an HTTP request to the specified URL with the specified expert ID.
  fetch(url + id)
      // When the response is received, check the status code.
      .then((response) => {
        // If the status is 200 (OK), parse the response as JSON.
        if (response.status == 200) {
          return response.json();
        } else {
          // If the status is not 200, throw an error with the status code.
          throw `error with status ${response.status}`;
        }
      })
      // After parsing the JSON, process the data.
      .then((expert) => {
        // Create an array to hold the expert data.
        let data = [];
        // Add the data for the current expert to the array.
        data.push([expert.id, expert.naam, expert.voornaam, expert.ram, expert.ssd, expert.path, expert.omschrijving]);
      })
      // If an error occurs, catch it and display the error message on the webpage.
      .catch((error) => {
        output.appendChild(document.createTextNode(error));
      });
}*/
</script>

<template>
    <!-- npm run install, npm install --save vue router -->
    <!-- npm install --save @fortawesome/vue-fontawesome -->
    <!--<ViewProfile :isAdmin="isAdmin"></ViewProfile>-->
    <header>
        <router-link to="/">
            <img  alt="expertRent logo"
                  src="./assets/expertrent.png"
                  class="logoHeader"
                  ref="expertRentLogo">
        </router-link>
        <router-link to="/">
            <img alt="expertRent logo"
                 src="./assets/erlogo.png"
                 class="logoHeaderSmall">
        </router-link>
        <nav>
            <ul class="fullScreenNav">
                <li>
                    <router-link to="/" class="navButton">
                        <font-awesome-icon icon="fa-solid fa-house" size="2x" class="icon"/>
                        <p>Home</p>
                    </router-link>
                </li>

                <li>
                    <router-link to="/listing" class="navButton">
                        <font-awesome-icon icon="fa-solid fa-street-view" size="2x" class="icon" />
                        <p>Listings</p>
                    </router-link>
                </li>

                <li>
                    <router-link to="/about" class="navButton">
                        <font-awesome-icon icon="fa-solid fa-address-card" size="2x" class="icon" />
                        <p>About Us</p>
                    </router-link>
                </li>

                <li>
                    <router-link to="/cart" class="navButton">
                        <font-awesome-icon icon="fa-solid fa-cart-shopping" size="2x" class="icon" />
                        <p>Cart</p>
                    </router-link>
                </li>

                <li>
                    <router-link to="/upgradePage" class="navButton" id="upgradebutton">
                        <font-awesome-icon icon="rocket" size="2x" class="icon" />
                        <p>Upgrade</p>
                    </router-link>
                </li>

                <li>
                    <router-link id="loginbutton" v-bind:to="path">
                        <p id="Login" class="navLogIn">Log in</p>
                    </router-link>
                </li>
            </ul>

            <div  ref="menuBtn" class="hamburger">
                <span class="bar"></span>
                <span class="bar"></span>
                <span class="bar"></span>
            </div>
        </nav>
    </header>

    <div ref="hamburgerMenu" class="hamburgerMenu">
        <div class="divider"></div>

        <router-link to="/" class="navButtonHM">
            <font-awesome-icon icon="fa-solid fa-house" size="2x" class="icon"/>
            <p>Home</p>
        </router-link>

        <div class="divider"></div>

        <router-link to="/listing" class="navButtonHM"  >
            <font-awesome-icon icon="fa-solid fa-street-view" size="2x" class="icon" />
            <p>Listings</p>
        </router-link>

        <div class="divider"></div>

        <router-link to="/about" class="navButtonHM"  >
            <font-awesome-icon icon="fa-solid fa-address-card" size="2x" class="icon" />
            <p>About Us</p>
        </router-link>

        <div class="divider"></div>

        <router-link to="/cart" class="navButtonHM"  >
            <font-awesome-icon icon="fa-solid fa-cart-shopping" size="2x" class="icon" />
            <p>Cart</p>
        </router-link>

        <div class="divider"></div>

        <router-link id="loginbutton" v-bind:to="path" class="navButtonHM"  >
            <p id="LoginHM">Log In</p>
        </router-link>

        <div class="divider"></div>
        <router-link id="upgradebuttonHM" to="/upgradePage" class="navButtonHM"  >
            <font-awesome-icon icon="rocket" size="2x" class="icon" />
            <p id="UpgradeHM">Upgrade</p>
        </router-link>


    </div>

    <main id="main">
        <div class="loggedInContainer" ref="welcomeBox">
            <div class="loggedIn">
                <div class="loggedInInner">
                    <img  alt="expertRent logo" src="./assets/expertrent.png" class="logoHeader LogoDisplay" ref="expertRentLogo">
                    <p>Welcome Back!</p>
                    <button class="loggedInButton" @click="closeWelcome">Thanks!</button>
                </div>
            </div>
        </div>
        <RouterView @login-werkt="handleSuccessfulLogin" @ReloadPage="reloadPage" ref="routerview" @idIfExpert="handleUsernameIfExpert"/>
    </main>
</template>

<style>

@font-face {
    font-family: Schoolbell;
    src: local("schoolbell"),
    url(@/assets/fonts/Schoolbell-Regular.ttf) format("truetype");
}

body {
    background: #B6D3D7;
    background-attachment: fixed;
    padding: 0;
    margin: 0;
    box-sizing: border-box;
    height: 100%;
}

/* it says unused but it isn't */
#app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
}

header {
    background-color: white;
    margin: 30px 0 0 0;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    width: 100%;
    height: 80px;
    align-items: center;
    top: 0;
    position: sticky;
    z-index: 3;
}

nav ul {
    display: flex;
    flex-direction: row;
    align-self: center;
    margin-right: 35px;
    list-style: none;
}

.navButton{
    margin: 5px;
    font-size: 10px;
    width: 70px;
    padding: 0 25px;
    color: #7C7C7C;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-self: center;
    text-align: center;
}

.divider {
    width: 70%;
    height: 3px;
    margin-left: 15%;
    margin-right: 15%;
    background: #dedede;
    display: flex;
}

.navButtonHM {
    margin: 5px;
    font-size: 20px;
    gap: 20px;
    width: 150px;
    padding: 0 25px;
    color: #7C7C7C;
    display: flex;
    flex-direction: row;
    align-self: center;
    text-align: center;
    vertical-align: center;
}

.navButtonHM p {
    margin-top: 9px;
}

.navButton:hover {
    transition: all 500ms ease-in-out;
    color: orange;
}

.navButtonHM:hover {
    transition: all 500ms ease-in-out;
    color: orange;
}

.navLogIn {
    display: flex;
    margin: 5px 5px 5px 25px;
    font-size: 10px;
    padding: 0 30px;
    color: #F39150;
    border: #F39150 2px solid;
    border-radius: 15px;
    background-color: #FFE2AE;
    align-self: center;
    align-items: center;
    height: 40px;
}

.navLogIn:hover {
    color: darkred;
    border: darkred 2px solid;
    transition: all 500ms ease-in-out;
    background-color: orange;
}

.navLogIn, .navButton {
    text-decoration: none;
}

main {
    margin: 0 auto;
    width: 100%;
}

.logoHeader {
    display: none;
    padding: 25px 0;
    margin: auto 60px;
    width: 184px;
    height: 31px;
}
.LogoDisplay {
  display: flex;
}

.hamburgerMenu {
    background: rgba(255, 255, 255, 0.98);
    justify-content: start;
    flex-direction: column;
    position: fixed;
    display: none;
    width: 90%;
    height: 70%;
    z-index: 3;
    right: 5%;
    left: 5%;
    margin-top: 1%;
    border-radius: 15px;
}

.hamburgerMenu * {
    display: flex;
    justify-content: center;
}

.logoHeaderSmall {
    display: flex;
    height: 50px;
    width: 50px;
    margin: 28px 20px;
    position: absolute;
    left: 0;
    top: 0;
    padding: 5px;
}

a:link{
    text-decoration: none;
}

a:hover{
    text-decoration: none;
}

a:active{
    text-decoration: none;
}

a:visited{
    text-decoration: none;
}

#upgradebutton{
    display: none;
}
.error-message{
  border: #2A93BC 2px solid;
  border-radius: 15px;
  color: #2A93BC;
  display: flex;
  justify-content: center;
  padding: 10px 20px;
}
.error-messagecolor{

}
/* //////////////////////log in pop-up///////////////////////// */
.loggedInContainer {
    width: 100%;
    height: 100%;
    display: none;
    justify-content: center;
    position: absolute;
    align-self: center;
    align-content: center;
    z-index: 3;
    background-color: rgba(250, 250, 250, 0.81);
    backdrop-filter: blur(5px);
}
.loggedIn {
    height: 200px;
    width: 300px;
    display: flex;
    background: white;
    position: absolute;
    align-self: center;
    align-content: center;
    z-index: 4;
    border-radius: 20px;
    border: #2A93BC 3px solid;
    box-shadow: black 10px 10px 50px 1px;
}

.loggedInInner {
    display: flex;
    justify-content: center;
    flex-direction: column;
    margin-bottom: 10%;
    text-align: center;
}

.loggedInButton {
    width: 50%;
    margin: auto;
    border: none;
    border-radius: 10px;
    background-color: #2A93BC;
    color: white;
    padding: 10px 0;
}
/* //////////////////////hamburger///////////////////////// */

.bar {
    width: 40px;
    height: 4px;
    background-color: dimgray;
    margin: 0 auto;
    border-radius: 80px;
    -webkit-transition: all 0.3s ease-in-out;
}

.hamburger{
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    height: 40px;
    cursor: pointer;
    margin-right: 20px;

}

.fullScreenNav, li {
    display: none;
}

/* Add styles for the open menu state */
.open .bar:nth-child(1) {
    transform: translateY(13px) rotate(45deg);
}

.open .bar:nth-child(2) {
    opacity: 0;
}

.open .bar:nth-child(3) {
    transform: translateY(-13px) rotate(-45deg);
}
#upgradebuttonHM {
    display: none;
}
/*/////////////////tablet mods///////////////////////*/

@media screen and (max-width: 1200px) {

    .navButton {
        margin: 20px 0;
        padding: 0 5px;
    }

    header {
        height: 120px;
    }

    .navLogIn {
        margin: 15px 5px 5px 25px;
    }


    .hamburgerMenu {
        display: none;
    }
}


/*/////////////////mobil mods///////////////////////*/
@media screen and (min-width: 880px) {
    .logoHeader {
        display: flex;
    }

    .logoHeaderSmall {
        display: none;
        height: 5px;
        width: 5px;
    }

    .fullScreenNav li:nth-child(n+2) {
        display: flex;
    }

    .bar {
        display: none;
    }

    .fullScreenNav, li {
        display: flex;
        margin-top: 10px;
    }

    .hamburgerMenu {
        display: none;
    }

    .hamburger {
        display: none;
    }
}
</style>
