<template>
  <div class="cards-container">
    <div class="card-container" v-for="expert in shuffle(experts).slice(0, 12)" :key="expert.id">
      <div class="card">
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
      },
    };
  },
  computed: {
    imgPath() {
      //console.log(`src/data/images/${this.store.experts[0].image}`, import.meta.url);
      return new URL(this.store.experts[0].image, import.meta.url);
    },
    experts() {
      let experts = this.store.getUserExpert;
      return experts.map(expert =>{
        expert.image= new URL(`../data/images/${expert.image}`, import.meta.url)
        return expert
      })
    },
    groupedItems() {
      let groups = [];
      for (let i = 0; i < this.ExpertJson.expertInfo.length; i += 5) {
        groups.push(this.ExpertJson.expertInfo.slice(i, i + 5));
      }
      return groups;
    }
  },
  methods: {
    fetchString() {
      fetch('https://localhost:7155/api/User/GetUserByName')
          .then(response => response.json())
          .then(data => {
            console.log(data);
          })
          .catch(error => {
            console.error(error);
          });
    },
    shuffle(array) {
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
    doFilter(e) {
      console.log(e.id);
    },
    routeToViewProfile(id){
      this.$router.push({name: 'ViewProfile', params: {id}})
    }
  },
}
</script>
<style scoped>
.cards-container {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}
.card-container {
  margin: 20px 25px;
}
.card {
  margin: 1px 20px;
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
.section > div {
  display: flex;
  flex-direction: column;
}
/*/////////////////mobil mods///////////////////////*/
@media screen and (min-width: 880px) {
  .section > div {
    display: flex;
    flex-direction: row;
  }
  .card-container {
    margin: 50px 20px;
  }
}
</style>
