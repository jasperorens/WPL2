<template>
  <div class="main">
    <div class="block">
      <h2 ref="resultShow" id="resultShow">
        test
      </h2>
    </div>

    <div class="boxResults">
      <div class="card" v-for="expert in shuffle(foundExperts)" :key="expert.id">
        <p v-text="expert.firstname + ' ' + expert.name"/>
        <img :src="'https://expertrent.blob.core.windows.net/images/' + expert.id + '.jpg'" class="pictureCard" alt="picture">
        <p class="left" v-text="expert.profession"></p>
        <p class="left" v-text="expert.specialization"></p>
        <input type="button" value="Rent knowledge" @click="routeToViewProfile(expert.id)" class="buttonProf"/>
      </div>
    </div>
  </div>
</template>

<script>
import { useExpertStore } from "@/stores/ExpertStore";

export default {
  name: "ListingPageResults",
  data() {
    return {
      experts: [],
      store: useExpertStore(),
      numberOfCards: 1,
      foundExperts: [],
    };
  },
  computed: {
    test() {
      //return this.$route.params.q;
    },
  },
  props: {
    searchQuery: {
      type: String,
      required: true,
    },
  },
  methods: {
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
    openProfile(event) {
      // Implement openProfile method
    },

    updateFoundExperts() {
 
      this.foundExperts = this.store.userExperts.filter(
          expert =>
              expert.name.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
              expert.firstname.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
              expert.profession.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
              expert.specialization.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
              expert.category.toLowerCase().includes(this.searchQuery.toLowerCase())
      );

      if (this.foundExperts.length > 0) {
        this.$refs.resultShow.innerHTML = `Experts found for \"<span class="result-font">${this.searchQuery}</span>\"`;
      } else {
        this.$refs.resultShow.innerHTML = `No experts found for \"<span class="result-font">${this.searchQuery}</span>\"`;
      }
      console.log(this.foundExperts)
    },

    routeToViewProfile(id){
      this.$router.push({name: 'ViewProfile', params: {id}})
    }
  },




  mounted() {
    this.updateFoundExperts();
    //console.log('test:', this.$route.params.q);
    
  },

  beforeUpdate() {
    this.updateFoundExperts();
  },
}
</script>

<style scoped>
.main {
  display: flex;
  flex-direction: column;
  justify-content: center;
  text-align: center;
  width: 100%;
  margin: 15px;
}

.card {
  margin: 50px 20px;
  padding: 10px;
  background-color: white;
  display: flex;
  width: 200px;
  height: 310px;
  flex-direction: column;
  border-radius: 30px;
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

.boxResults {
  display: flex;
  flex-direction: row;
  height: auto;
  flex-wrap: wrap;
  justify-content: center;
}

.left {
  text-align: left;
}

/* dit zijn alle tekst velden zoals profession en specialiteit */
.card p:nth-child(n+2):nth-child(-n+4) {
  margin: 8px 0 0 0;
}

.pictureCard {
  height: 130px;
  width: 130px;
}





.block {
  display: flex;
  height: 100%;
  width: 95%;
  background-color: white;
  justify-content: center;
  margin: 60px auto;
  border-radius: 20px;
  color: dimgrey;
}

.result-font {
  color: dodgerblue;
}
/*/////////////////web mods///////////////////////*/
@media screen and (min-width: 100px) {
  .block {
    margin-top: 100px;
  }
}

/*/////////////////web mods///////////////////////*/
@media screen and (min-width: 200px) {
  .block {
    margin-top: 60px;
  }
}

/*/////////////////web mods///////////////////////*/
@media screen and (min-width: 450px) {
  .block {
    margin-top: 20px;
  }
}


/*/////////////////web mods///////////////////////*/
@media screen and (min-width: 880px) {
  .block {
    margin-top: 20px;
  }
}



</style>
