<template>
  <!--  <div v-for="(expert, index) in experts" :key="index">-->
  <!-- div gemaakt om rijen te splitsen -->
  <div class="section">
    <div>
      <div class="card" v-for="expert in shuffle(experts).slice(0, numberOfCards.translateWidthToCardsNumber())"
           :key="expert.id">
        <p v-text="expert.firstname + ' ' + expert.name"/>
        <img :src="'https://expertrent.blob.core.windows.net/images/' + expert.id + '.jpg'" class="pictureCard" alt="picture">
        <p v-text="expert.profession"></p>
        <p v-text="expert.specialization"></p>
        <input type="button" value="Rent knowledge" @click="routeToViewProfile(expert.id)" class="buttonProf"/>
      </div>
    </div>
  </div>
</template>


<script>
import {mapStores} from "pinia";
import {useExpertStore} from "@/stores/expertStore";
//import homePage from "@/views/HomePage.vue";
export default {
  name: "Cards",
  data() {
    return {
      pinia: mapStores(),
      store: useExpertStore(),
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
      path: "pf_2.png"
    };
  },
  computed: {
    imgPath() {
      //console.log(`src/data/images/${this.store.experts[0].image}`, import.meta.url);
      return new URL(this.store.experts[0].image, import.meta.url);
      /*  return new URL(`../data/images/${this.store.experts[0].image}`, import.meta.url); */
      /*       return new URL(`../data/images/pf_2.png`); */
      /*return new URL("../data/images/pf_2.png");*/
    },
    experts() {
      //console.log(experts)
      return this.store.getUserExpert;
    },
    /* groupedItems() {
    let groups = [];
    for (let i = 0; i < this.ExpertJson.expertInfo.length; i += 5) {
      groups.push(this.ExpertJson.expertInfo.slice(i, i + 5));
    }
    return groups;
    } */
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
    routeToViewProfile(id){
      this.$router.push({name: 'ViewProfile', params: {id}})
    }
  },
}
</script>
<style scoped>
.card {
  margin: 50px 20px;
  padding: 10px;
  background-color: white;
  display: flex;
  width: 200px;
  height: 310px;
  flex-direction: column;
  border-radius: 30px;
  box-shadow: 2px 3px 4px rgba(0, 0, 0, 0.5);
}
.card p:first-child {
  text-align: right;
  font-weight: bold;
  margin: 15px 0 10px 0;
}
.buttonProf {
  background-color: #2A93BC;
  color: white;
  border-radius: 10px;
  padding: 10px;
  text-align: center;
  margin: 25px 0 0 0;
}
.buttonProf:hover {
  background-color: #09637F;
  box-sizing: content-box;
  border-top: #053F51 5px solid;
  border-left: #053F51 5px solid;
  padding: 8px 8px 5px 5px;
  color: #CECECE;
}
.card img:nth-child(2) {
  border: #7C7C7C 1px solid;
  border-radius: 10px;
  align-self: center;
}
/* dit zijn alle tekst velden zoals profession en specialiteit */
.card p:nth-child(n+2):nth-child(-n+4) {
  margin: 8px 0 0 0;
}
.pictureCard {
  
  height: 130px;
  width: 130px;
}



.section {
  display: flex;
  height: auto;
  align-items: center;
  justify-content: center;
  flex-direction: row;
  flex-wrap: wrap;
}
.section > div {
  display: flex;
  flex-direction: column;
}
/*/////////////////mobil mods///////////////////////*/
@media screen and (min-width: 880px) {
  .section {
    display: flex;
    flex-direction: column;
    justify-content: center;
  }
  .section > div {
    display: flex;
    flex-direction: row;
  }
}
</style>