<template>
  <div class="lsection">
    <!-- div gemaakt om rijen te splitsen -->
    <div>
      <div class="lcard" v-for="expert in shuffle(experts)" :key="expert.id">
          <p v-text="expert.firstname + ' ' + expert.name"/>
        <img :src="'src/data/images/pf_' + expert.id + '.png'" class="lpictureCard" alt="picture">
          <p v-text="expert.profession"></p>
          <p v-text="expert.specialization"></p>
        <input type="button" value="Rent X knowledge" @click="routeToViewProfile(expert.id)" class="buttonProf"/>
        </div>
    </div>
  </div>
</template>
<script>
/*const images = require.context('../../data/images', true, /\.png$/);*/
import {useExpertStore} from "@/stores/ExpertStore";

export default {
  name: "ListingCard",
  data() {
    return {
      store: useExpertStore(),
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
      let experts;
      for (let i = 0; i < this.store.experts.length; i++) {
        if(this.store.experts[i].active = 1)
        {
          experts.push(this.store.experts[i])
        }
      }

      return experts.map(expert => {
        expert.image = new URL(`../data/images/${expert.image}`, import.meta.url)
        return expert
      })
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
.lcard {
  margin: 50px 20px;
  padding: 10px;
  background-color: white;
  display: flex;
  width: 200px;
  height: 310px;
  flex-direction: column;
  border-radius: 30px;
}

.lcard p:first-child {
  text-align: right;
  font-weight: bold;
  margin: 15px 0 10px 0;
}

.lcard p:last-child {
  background-color: #2A93BC;
  color: white;
  border-radius: 10px;
  padding: 10px;
  text-align: center;
}

.lcard p:last-child:hover, a {
  background-color: #09637F;
  box-sizing: content-box;
  border-top: #053F51 5px solid;
  border-left: #053F51 5px solid;
  padding: 8px 8px 5px 5px;
  color: #CECECE;
}


.lcard img:nth-child(2) {
  border: #7C7C7C 1px solid;
  border-radius: 10px;
  align-self: center;
}

/* dit zijn alle tekst velden zoals profession en specialiteit */
.lcard p:nth-child(n+2):nth-child(-n+4) {
  margin: 8px 0 0 0;
}

.lpictureCard {
  height: 130px;
  width: 130px;
}

.lsection {
  display: flex;
  width: 70%;
  align-items: center;
  justify-content: space-between;
  flex-direction: column;
}

.section > div{
  display: flex;
  flex-direction: column;
}

.midfilter >p{
  text-align: center;
  font-weight: bolder;
  color: grey;
}
.midfilter > div > p{
  text-align: center;
  font-weight: bold ;
  border-top: 2px lightgray solid ;
  padding-top: 12px;
  color: grey;
}

.midfilter > div > p:hover{
  transition: all 500ms ease-in-out;
  color: black;
}

.filterbalk > div> p{
  text-align: center;
  font-size: 2rem;
  margin: 20px;
  color: gray;
  border-radius: 10px;
}

@media screen and (min-width: 880px) {
  .lsection {
    display: flex;
    width: 60%;
    align-items: center;
    justify-content: space-between;
    flex-direction: row;
  }

  .section > div{
    display: flex;
    flex-direction: column;
  }
  .filterbalk > div> p{
    font-family: Schoolbell,serif;
    text-align: center;
    font-size: 3rem;
    margin: 10px;
  }

  .midfilter >p{
    text-align: center;
    font-weight: bolder;
  }
  .midfilter > div > p{
    text-align: center;
    font-weight: bold ;
  }
}


</style>

