<template>
  <div class="filterbalk">
    <!-- <div class="fixed-Position"> -->
    <div>
      <p class="theFont">Find your Expert</p>

      <div class="midfilter">
        <p class="categorieTitle">Categories</p>
        <div class="categories">
          <p @click="handleClickMenu" ref="STEM">STEM</p>
          <p @click="handleClickMenu" ref="Business">Business</p>
          <p @click="handleClickMenu" ref="Creative">Creative</p>
          <p @click="handleClickMenu" ref="Service">Service</p>
          <p @click="handleClickMenu" ref="Trade">Trade</p>
        </div>

      </div>
    </div>
    <div class="searchcontainer">
      <div class="search-div">
        <input class="white" type="text" placeholder="Search" id="searchTermID" @keyup.enter="handleClick">
        <button class="button" id="button" @click="handleClick">Go!</button>
      </div>
    </div>
    <!-- </div> -->
  </div>
</template>

<script>
import {useExpertStore} from "@/stores/expertStore";

export default {
  name: "ListingPageSearch",
  data() {
    return {
      store: useExpertStore(),
    };
  },
  methods: {
    handleClick() {
      const query = document.getElementById("searchTermID").value;
      this.$emit("search", query);
    },

    handleClickMenu(event) {
      const query = event.target.textContent;
      this.$emit("search", query);
    }
  },

  mounted() {
    const button = document.getElementById('button');
    const searchTerm = document.getElementById('searchTermID');
    button.addEventListener('click', () => {
      const query = searchTerm.value;
      // Do something with the search query, e.g. redirect to a search results page
      console.log(query);
    });
  } // <--- move this line up and remove the comma from here
}
</script>


<style scoped>
.filterbalk {
  height: 15%;
  /* indien width hier veranderd moet dit ook in Listingpage*/
  width: 100%;
  top: 120px;
  position: fixed;
  background-color: white;
  padding: 8% 0 0 0;
  margin-top: 0;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.midfilter {
  display: none;
}

.search-div {
  display: flex;
  flex-direction: row;
  justify-content: center;
  margin-bottom: 200px;
}

.theFont{
  font-family: Schoolbell,serif;
  text-align: center;
  font-size: 1.5rem;
  margin: 10px 10px;
  padding-bottom: 0;
}

.button {
  background-color: #2A93BC;
  color: white;
  border-radius: 10px;
  height: 30px;
  width: 50px;
  vertical-align: center;
  margin-top: -3px;
}

.searchcontainer {
  justify-content: space-around;
}

.white {
  width: 60%;
  margin-right: 5%;
  border-radius: 10px;
}

.categories p {
  padding-left: 20px;
}
.categories p:hover {
  color: #2A93BC;
}

.categorieTitle {
  background-color: #c4eeff;
  padding: 15px 10px;
  border-radius: 12px;
}

@media screen and (min-width: 880px) {

  .filterbalk {
    height: 100%;
    /* indien width hier veranderd moet dit ook in Listingpage*/
    width: 20%;
    top: 6%;
    position: fixed;
    background-color: white;
    padding: 50px 10px;
    margin-top: 0;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
  }

  .midfilter {
    padding-left: 20px;
    display: flex;
    width: 80%;
    flex-direction: column;
  }

  .theFont{
    font-family: Schoolbell,serif;
    text-align: center;
    font-size: 1.5rem;
    margin: 40px 10px;
    padding-bottom: 15%;
  }
}
</style>
