<template>
<cards-listings></cards-listings>
</template>

<script>
import CardsListings from "@/components/CardsListings.vue";
import ListingPageSearch from "@/views/ListingPageSearch.vue";

export default {
  name: "ListingPageResults",
  components: {CardsListings, ListingPageSearch},
  data(){
    return{
      CardsListings,
    }
  }
}

</script>

<style scoped>

</style>


