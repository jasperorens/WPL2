<template>
  <div class="pagebox">
    <div class="left">
      <ListingPageSearch @search="handleSearch"/>
    </div>

    <!--  -->
    <div class="right">
      <template v-if="searchQuery">
        <ListingPageResults :searchQuery="searchQuery" :search-query="searchQuery"/>
      </template>

      <template v-else>
        <ListingPageDefault/>
      </template>
    </div>

  </div>


</template>

<script>
/*import Experts from '../data/parts';*/
import CardsListings from "@/components/CardsListings.vue";
import ListingPageSearch from "@/views/ListingPageSearch.vue";
import ListingPageDefault from "@/views/ListingPageDefault.vue";
import ListingPageResults from "@/views/ListingPageResults.vue";
import {useExpertStore} from "@/stores/expertStore";

export default {
  name: 'ListingPage',
  components: {
    ListingPageResults,
    ListingPageDefault,
    ListingPageSearch,
    CardsListings,
  },

  mounted() {
    //console.log("test listingpage ",this.$route.query.q);
  },

  data(){
    return{
      CardsListings,
      ListingPageSearch,
      store: useExpertStore(),
      searchQueryLPS: "",
      lastSearchWasLPS: false,
    };
  },

  methods: {
    handleSearch(query) {
      this.searchQueryLPS = query;
      this.lastSearchWasLPS = true;
    }
  },

  computed: {
    searchQuery(){
      return !this.lastSearchWasLPS ? this.$route.query.q : this.searchQueryLPS;
    },

  }
};

</script>
<style scoped>
.pagebox {
  display: flex;
  flex-direction: column;
}

.left {
  display: flex;
  width: 100%;
}

.right {
  display: flex;
  width: 100%;
  margin-top: 25%;
}

@media screen and (min-width: 880px) {
  .pagebox {
    flex-direction: row;
  }

  .left {
    display: flex;
    width: 20%;
  }

  .right {
    display: flex;
    width: 80%;
    margin-top: 0;
  }
}
</style>
