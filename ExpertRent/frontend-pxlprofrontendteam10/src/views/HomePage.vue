<template>
  <div class="home" ref="page" id="home">
    <!-- foto van zwarte vrouw die in slide -->
    <img :src="workerImage"
         alt="worker"
         id="worker"
         class="worker">
    <!-- search balk -->
    <div class="search">
      <h1 class="welcome">Find your tutor!</h1>
      <!-- nog geen reden waarom 2 divs -->
      <div class="search-div">
        <!-- input field voor search -->
        <input class="white" type="text" placeholder="    Search" v-model="expertQuery" id="searchTermID" @keyup.enter="handleClick">
        <!-- icon voor search-->
        <span class="errspan"><font-awesome-icon
            class="icon"
            icon="fa-solid fa-magnifying-glass" /></span>

      </div>
    </div>
  </div>
  <!-- white area die experts splitst van banner -->
  <div class="white splitBanner"></div>
  <!-- div waarin alle experts wprden getoond-->
  <!--  ///////////////////////////////////////    -->
    <Cards/>
  <!--  ///////////////////////////////////////    -->

</template>

<script>
/* array met experts importeren */
import Cards from "@/components/Cards.vue";
import {useExpertStore} from "@/stores/expertStore";
//import ListingPage from "@/views/ListingPage.vue";

export default {
  name: 'HomePage',
  components: {Cards},
  emits: ['loginWerkt', 'search'],
  data(){
    
    return{
      Cards,
      workerImage: "",
      expertQuery: "",
      numberOfCards: {
        translateWidthToCardsNumber() {
          const minimumWidth = 320;
          let numberCards = 1
          let hpWidth = localStorage.getItem('HomePageWidth');
          while (hpWidth > minimumWidth) {
            numberCards += 1;
            hpWidth -= 320;
          }
          return numberCards;
        }
      },
      store: useExpertStore(),
    };
  },


  mounted() {
    /* Random number generator */
    const randomNumber = Math.floor(Math.random() * 4) + 1;

    /* Header changer on Load */
    if (randomNumber === 1) {
      this.workerImage = "/src/assets/buildDesign/worker1.png";
    } else if (randomNumber === 2) {
      this.workerImage = "/src/assets/buildDesign/worker2.png";
    } else if (randomNumber === 3) {
      this.workerImage = "/src/assets/buildDesign/worker3.png";
    } else if (randomNumber === 4) {
      this.workerImage = "/src/assets/buildDesign/worker4.png";
    }

    /* background Header changer on load */
    const workerImageBackground = document.querySelector('.home')
    if (randomNumber === 1) {
      workerImageBackground.style.backgroundImage = `url("/src/assets/buildDesign/background1.png")`;
    } else if (randomNumber === 2) {
      workerImageBackground.style.backgroundImage = `url("/src/assets/buildDesign/background2.png")`;
    } else if (randomNumber === 3) {
      workerImageBackground.style.backgroundImage = `url("/src/assets/buildDesign/background3.png")`;
    } else if (randomNumber === 4) {
      workerImageBackground.style.backgroundImage = `url("/src/assets/buildDesign/background4.png")`;
    }

    /* font and fontcolor of header changer on load */
    const workerImageFont = document.querySelector('.welcome')
    if (randomNumber === 1) {
      workerImageFont.style.color = '#b8d1c8';
      workerImageFont.style.fontFamily = 'Schoolbell, Arial,serif';
      workerImageFont.style.transform = 'skew(0deg, 3deg)';
      workerImageFont.style.fontSize = '3REM';
    }
    else if (randomNumber === 2) {
      workerImageFont.style.color = '#886839';
      workerImageFont.style.fontFamily = 'Impact, Arial,serif';
      workerImageFont.style.transform = 'skew(0deg, 0deg)';
      workerImageFont.style.fontSize = '3REM';
    }
    else if (randomNumber === 3) {
      workerImageFont.style.color = '#303030';
      workerImageFont.style.fontFamily = 'Times New Roman, Arial,serif';
      workerImageFont.style.transform = 'skew(0deg, 0deg)';
      workerImageFont.style.fontSize = '3REM';
    }
    else if (randomNumber === 4) {
      workerImageFont.style.color = '#f8dc3f';
      workerImageFont.style.fontFamily = 'Brush Script MT, Arial,serif';
      workerImageFont.style.transform = 'skew(0deg, -4deg)';
      workerImageFont.style.fontSize = '4REM';
    }

  },

  computed: {
  },
  created() {
    const width = window.innerWidth;
    localStorage.setItem('HomePageWidth', width);
    const query = this.$route.query.q;
    if (query) {
      document.getElementById("searchTermID").value = query;
    }
  },
  methods: {
    shuffle(array) {
      //if active = false, next number
      let currentIndex = array.length;
      let temporaryValue, randomIndex;
      while (0 !== currentIndex) {
        randomIndex = Math.floor(Math.random() * currentIndex);
        currentIndex -= 1;
        temporaryValue = array[currentIndex];
        array[currentIndex] = array[randomIndex];
        array[randomIndex] = temporaryValue;
      }
      return array;
    },
    handleClick() {
      this.$router.push({
        name: 'Listing',
        query: {q: this.expertQuery }, //this.expertQuery word vervangen door getElementbyId in de html met de V-model, dit werk als een hotreload
      });
      //query: { q: query },
      //this.$router.push({name: 'products.index', params: { id: 1 }})
      this.$emit("search", query);
      console.log("console log homepage", query);
    }
  },
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

/* style voor icoon bij search balk */
.errspan {
  float: right;
  position: relative;
  width: 30px;
  height: 30px;
  margin-right: 10px;
  margin-top: -32px;
  z-index: 2;
  color: black;
}

/* style voor banner (plaats met sliding worker woman) */
.home {
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
  height: 340px;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: row;
  gap: 250px;
}

/* style voor text in searchbalk */
.welcome {
  text-align: center;
  margin: 20px;
  /* this */
}
/* style voor search balk */
.search {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  opacity: 0;
  animation-name: slideInFromRight;
  animation-duration: 4s;
  animation-delay: 0.2s; /* Add a delay to the slide-in animation */
  animation-fill-mode: forwards; /* Ensure that the element stays visible after the animation ends */
}
/* style voor whitespace tussen banner en experts (we built a wall and made the experts great again) */
.white {
  width: 100%;
  height: 30px;
  background: white;
  display: flex;
  flex-direction: row;
  justify-content: center;
  border-radius: 25px;
}
/* it exists */
.search-div{
  width: 80%;
}
/* style voor whitespace tussen banner en experts (we built a wall and made the experts great again) */
.splitBanner {
  height: 20px;
  width: 100%;
}
/* test */

.icon {
  margin: 5px 20px;
  color: gray;
}

a {
  color: #42b983;
}

.worker{
  display: none;
}

cards {
  display: flex;
  flex-wrap: wrap;
}

/* starten van animatie */
.worker {
  opacity: 0;
  animation-name: slideInFromLeft;
  animation-duration: 4s;
  animation-delay: 0.2s; /* Add a delay to the slide-in animation */
  animation-fill-mode: forwards; /* Ensure that the element stays visible after the animation ends */
}

/* Define the keyframes for the slide-in animation */
@keyframes slideInFromLeft {
  from {
    opacity: 0;
    transform: translateX(-100%); /* Move the element from left to right */
  }
  to {
    opacity: 1;
    animation-fill-mode: forwards;
    transform: translateX(0); /* Move the element back to its original position */
  }
}

@keyframes slideInFromRight {
  from {
    opacity: 0;
    transform: translateX(100%); /* Move the element from left to right */
  }
  to {
    opacity: 1;
    animation-fill-mode: forwards;
    transform: translateX(0); /* Move the element back to its original position */
  }
}

/*/////////////////web mods///////////////////////*/
@media screen and (min-width: 880px) {

  .worker {
    height: 488px;
    margin-top: 12px;
    display: flex;
    opacity: 0;
  }

  /* style voor text in searchbalk */
  .welcome {
    font-size: 4rem;
  }

  .home {
    height: 500px;
  }
}

/*//////////////////////////the card////////////////////////////////////////*/

</style>
