<template>
  <button ref="removeButtonID" class="settings_Button" type="submit" @click="removeUser">Remove User</button>


  <div class="hazeContainer" ref="hazeRef">
    <div class="haze">
      <div class="hazeInner">
        <img  alt="expertRent logo" src="/src/assets/expertrent.png" class="logoHeader LogoDisplay" ref="expertRentLogo">
        <p>{{hazeText}}</p>
        <button class="hazeButton" @click="HazeClose">{{hazeText2}}</button>
      </div>
    </div>
  </div>


  <div class="container">
    <div class="info">
      <div class="topInfo">
        <img :src="'https://expertrent.blob.core.windows.net/images/' + user.id + '.jpg'" class="profilePicture" alt="picture">

        <div class="topInfoText margin">
          <div class="userNameDiv">
            <p class="userName">{{user.username}}</p>
          </div>

          <p>name: <span> {{ user.firstname }} {{ user.name }}</span></p>
          <p>category: <span> {{ expert.category}}</span></p>
          <p>profession: <span> {{ expert.profession}}</span></p>
          <p>specialization: <span> {{ expert.specialization}}</span></p>
        </div>
      </div>

      <div class="row" >
        <div class="topInfoText rowContainer">
          <p>province: <span> {{ user.province}}</span></p>
          <p>country: <span> {{ user.country}}</span></p>
            <div class="unavailableClass">
<!--                <p><span style="font-weight: bolder">weekdays:</span></p>
                <ul v-for="(days, key) in this.daysforDisplay" :key=key>
                    <li v-text="days"/>
                </ul>-->
            </div>
        </div>

        <div class="rowContainerCalendar">
          <div class="rowContainerCalendarTop">
            <div class="boxAvailable">
              <template v-if="expert.nowAvailable === false">
                <p>inActive</p>
                <img src="/src/assets/Badges/inActive.png" class="availableLogo" alt="unavailable" title="Unvailable">
              </template>

              <template v-else >
                <p>Active</p>
                <img src="/src/assets/Badges/nowAvailable.png" class="availableLogo" alt="available" title="Available">
              </template>
            </div>

            <div class="moneyBox">
              <p>price/hour: </p>
              <p class="money"><span class="small">€</span> {{ expert.price}}</p>
            </div>
          </div>


          <div class="calendarB">
            <Calendars @selectedDateTimeChanged="updateSelectedDateTime" :selectedDay="selectedDay" :currentExpert="this.expert.id"/>
          </div>

        </div>

        <div class="rowContainer2 addition">
          <p class="lightText">Badges</p>







            <div class="badgeClass">
                <div class="box" ref="admin" style="display:flex" title="Admin badge">
                    <img src="/src/assets/Badges/admin.png" class="badges" alt="adminbadge" >
                </div>

                <div class="box" ref="goGetter" style="display:flex" title="GoGetter badge">
                    <img src="/src/assets/Badges/goGetter.png" class="badges" alt="goGetter" >
                </div>

                <div class="box" ref="humanSearchEngine" style="display:flex" title="Human search Engine badge">
                    <img src="/src/assets/Badges/humanSearchEngine.png" class="badges" alt="humanSearchEngine" >
                </div>

                <div class="box" ref="MVP" style="display:flex" title="MVP badge">
                    <img src="/src/assets/Badges/MVP.png" class="badges" alt="MVP" >
                </div>

                <div class="box" ref="onARoll" style="display:flex" title="On a Roll badge">
                    <img src="/src/assets/Badges/onARoll.png" class="badges" alt="onARoll" >
                </div>

                <div class="box" ref="roockie" style="display:flex" title="Rookie badge">
                    <img src="/src/assets/Badges/roockie.png" class="badges" alt="roockie" >
                </div>

                <div class="box" ref="verified" style="display:flex" title="Verified badge">
                    <img src="/src/assets/Badges/verified.png" class="badges" alt="verified" >
                </div>

                <div class="box" ref="veteran" style="display:flex" title="Veteran badge">
                    <img src="/src/assets/Badges/veteran.png" class="badges" alt="veteran" >
                </div>

                <div class="box" ref="workaholic" style="display:flex" title="Workaholic badge">
                    <img src="/src/assets/Badges/workaholic.png" class="badges" alt="workaholic" >
                </div>

                <div class="box" ref="workaholic1" style="display:flex" title="Workaholic lvl2 badge">
                    <img src="/src/assets/Badges/workaholic1.png" class="badges" alt="workaholic1" >
                </div>


              <!--<img src="/src/assets/Badges/MVP.png">
            <div v-for="(value, key) in GotBadges" :key=key v-if="value">
              <div class="box">-->
                  <!--<div style="width: 20px; height: 20px; background-color: red"></div> -->
                  <!--<img src="/src/assets/Badges/MVP.png" class="badges">
                  <img :src="'/src/assets/Badges/' + value + '.png'" class="badges" alt="" :title=" value + 'badge'">
              </div>
            </div>-->
          </div>
        </div>
      </div>
    </div>
    <!--
    available: [
    ],
    -->
  </div>

</template>

<script>
import Cards from "@/components/Cards.vue";
import ListingCard from "@/components/ListingCard.vue";
import {useExpertStore} from "@/stores/expertStore";
import {useBookingStore} from "@/stores/bookingStore";
import Calendars from "@/components/Calendars.vue";
/*const apiUrl = 'https://localhost:7155/api/User/editUserInfo';*/
export default {
  name: "ViewProfile",
  components: {Calendars, Cards},
  emits: ['loginWerkt', 'idIfExpert'],

    data() {
        return {
            selectedDay: "",
            expert: "",
            user: "",
            Cards,
            Calendars,
            ListingCard,
            store: useExpertStore(),
            isAdmin: false,
            Booked: {
                idBuyer: "",
                idSeller: "",
                Date: "",
                StartTime: "",
                EndTime: ""
            },
            GotBadges: {
                userid: "",
                admin: "false",
                goGetter: "false",
                humanSearchEngine: "false",
                MVP: "false",
                onARoll: "false",
                roockie: "false",
                verified: "false",
                veteran: "false",
                workaholic: "false",
                workaholic1: "false",
            },
            loadedDataBadge: {

            },
            hazeText: "",
            hazeText2: "",
            clickedBooked: false,
            availabledays: {
              expertId: 0,
              monday: false,
              tuesday:false,
              wednesday: false,
              thursday: false,
              friday: false,
              saturday: false,
              sunday: false,
          }
        };
    },

    created() {
    const store = useExpertStore();
    const experts = store.experts;
    const users = store.users;
    const id = this.$route.params.id;

    this.expert = experts.find((expert)=> expert.id === parseInt(id));
    this.user = store.users.find((u)=> u.id === parseInt(id));
    //console.log(this.expert)
    //console.log(this.user)
      this.getBadge();

    /*const bookingStore = useBookingStore();
    const allBookings = bookingStore.assignAllBookings().then(()=> {
      const bookedDates = JSON.stringify(allBookings);
      console.log('booked dates', bookedDates);
    }).catch(error => {
        console.error(error);
      });*/



  },

  computed: {
      numberofBadges() {
        return this.GotBadges.length;
      },
    badgesNr() {
      return this.expert.Gotbadges.length;
      /*let badgesNr = this.expert.badges;
      return badgesNr.map(badge => {
        //expert.image = new URL(`../data/images/${expert.image}`, import.meta.url)
        return badge
      })*/
    },
  },

    mounted() {
      //console.log(this.expert.id);
    this.isAdmin = localStorage.getItem('adminRights');
    console.log("Are you still an admin", this.isAdmin);
    this.$nextTick(() => {
      if (this.isAdmin === 'true') {
        this.$refs.removeButtonID.style.display = "flex";
        console.log("Button is visible");
      } else {
        this.$refs.removeButtonID.style.display = "none";
        console.log("Button is hidden");
      }
    });
    this.getAvailability();


  },

    methods:{
      async getBadge() {
          try {
              await fetch("https://localhost:7155/api/Badge/getBadge?id="+this.expert.id, {
                  method: 'GET',
                  headers: {
                      'Accept': 'application/json',
                  },
              }).then(response =>{
                  return response.json()
              }).then(data=>{
                  this.loadedDataBadge = JSON.parse(data).slice(1, -1).split(',').map(pair => {
                      const [, value] = pair.split(':');
                      return value.trim().split('}')[0];
                  });
                  this.GotBadges.userid = this.loadedDataBadge[0];
                  this.GotBadges.admin = this.loadedDataBadge[1];
                  this.GotBadges.goGetter = this.loadedDataBadge[2];
                  this.GotBadges.humanSearchEngine = this.loadedDataBadge[3];
                  this.GotBadges.MVP = this.loadedDataBadge[4];
                  this.GotBadges.onARoll = this.loadedDataBadge[5];
                  this.GotBadges.roockie = this.loadedDataBadge[6];
                  this.GotBadges.verified = this.loadedDataBadge[7];
                  this.GotBadges.veteran = this.loadedDataBadge[8];
                  this.GotBadges.workaholic = this.loadedDataBadge[9];
                  this.GotBadges.workaholic1 = this.loadedDataBadge[10];
                  console.log('GotBadges',this.GotBadges)
              })
          } catch (error) {
              console.error(error)
          }
          this.setBadges();
      },



    HazeClose(){
      this.$refs.hazeRef.style.display ="none";
      if (this.clicked === true){
        this.clicked = false;
        this.OpenLogin();
      }
    },
    HazeOpen(){
      this.$refs.hazeRef.style.display = "flex";
      window.scrollTo(0, 0);
    },
    AlertBooking(){
      this.hazeText = "Booked successfully!";
      this.hazeText2 = "Thanks!";
      this.HazeOpen();
      this.clicked = true;

    },

    removeUser(){
      const index = this.store.experts.findIndex(obj => obj.username === this.expert.username);
      console.log("index:")
      console.log(index);
      console.log("Startactive:")
      console.log(this.store.experts[index].active);
      this.store.experts[index].active = 0;
      this.expert.active = 0;
      console.log("Expert has been deactivated, bye!");
      this.handleSubmit();
    },
    updateSelectedDateTime(dateTime) {


      // do whatever you want with the dateTime data
      //console.log('profile', dateTime);
      //console.log('expert', this.expert.username);

      /*localStorage.setItem('UsernameBooking', this.expert.username);
      localStorage.setItem('TimeBooking', dateTime);
      localStorage.setItem('PriceBooking', this.expert.price);*/

      let book = [this.user.username, dateTime, this.expert.price];
      /*alert(book);*/


// Retrieve the booked array from localStorage, or create an empty array if it doesn't exist
      let booked = JSON.parse(localStorage.getItem('booked') || '[]');

// Add the book array to the booked array

      const dateAndTime = dateTime.split('T');

      const datePart = dateAndTime[0];
      const startTimePart = dateAndTime[1];
      /*alert(datePart);
      alert(startTimePart);*/

      let dayOfWeek = new Date(datePart);
      /*alert(dayOfWeek)*/
      dayOfWeek = dayOfWeek.getDay();
     /* alert(dayOfWeek)*/


/*
        let dateForCheck = new Date(dateTime);
        let dayOfWeek =  dateForCheck.getDay();
        alert(dateTime);
      alert(dateForCheck);
        alert("Bookeddate", dayOfWeek);
        console.log('dayOfWeek', dayOfWeek);*/

        switch (dayOfWeek){
          case 0:
            if(this.availabledays.sunday === true){
              this.AlertBooking();
              booked.push(book);

            }
            else{

              this.hazeText = "Warning";
              this.hazeText2 = "Sorry, this user is unavailable on this day. Please select another date.";
              this.HazeOpen();
              this.clicked = true;
            }
            break;

          case 1:
            if(this.availabledays.monday === true){
              this.AlertBooking();
              booked.push(book);

            }
            else{

              this.hazeText = "Warning";
              this.hazeText2 = "Sorry, this user is unavailable on this day. Please select another date.";
              this.HazeOpen();
              this.clicked = true;
            }
            break;

          case 2:
            if(this.availabledays.tuesday === true){
              this.AlertBooking();
              booked.push(book);
            }
            else{

              this.hazeText = "Warning";
              this.hazeText2 = "Sorry, this user is unavailable on this day. Please select another date.";
              this.HazeOpen();
              this.clicked = true;
            }
            break;

          case 3:
            if(this.availabledays.wednesday === true){
              this.AlertBooking();
              booked.push(book);

            }
            else{

              this.hazeText = "Warning";
              this.hazeText2 = "Sorry, this user is unavailable on this day. Please select another date.";
              this.HazeOpen();
              this.clicked = true;
            }
            break;

          case 4:
            if(this.availabledays.thursday === true){
              this.AlertBooking();
              booked.push(book);

            }
            else{

              this.hazeText = "Warning";
              this.hazeText2 = "Sorry, this user is unavailable on this day. Please select another date.";
              this.HazeOpen();
              this.clicked = true;
            }
            break;

          case 5:
            if(this.availabledays.friday === true){
              this.AlertBooking();
              booked.push(book);

            }
            else{

              this.hazeText = "Warning";
              this.hazeText2 = "Sorry, this user is unavailable on this day. Please select another date.";
              this.HazeOpen();
              this.clicked = true;
            }
            break;

          case 6:
            if(this.availabledays.saturday === true){
              this.AlertBooking();
              booked.push(book);
            }
            else{

              this.hazeText = "Warning";
              this.hazeText2 = "Sorry, this user is unavailable on this day. Please select another date.";
              this.HazeOpen();
              this.clicked = true;
            }
            break;
          
        }



// Store the updated booked array in localStorage
      localStorage.setItem('booked', JSON.stringify(booked));

// Clear the book array
      book = [];
    },
    //split if
    async handleSubmit() {
      console.log(this.expert);
      await fetch('https://localhost:7155/api/Expert/editExpertActive', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(this.expert)})
      console.log("Look master, I've done it, I've done it! I smashed, and grounded, and pounded this rotten expert into fleshy pink pulp! What good is your expertise now?! Try stirring up trouble in this sorry state! MUHAHAHAHA! ")

    },
    getAvailability() {
      fetch('https://localhost:7155/api/Unavailable/GetDaysByID?id=' + this.user.id)
          .then(res => res.json())
          .then(data => {
            this.availabledays = data;
            console.log('dit is de availability :)',this.availabledays);
            });
  },
        setBadges(){
            console.log('start Set Badges');
            ///////////////////////////////////////
            if(this.GotBadges.MVP === "false") {
                this.$refs.MVP.style.display = "none";
            }
            else {
                this.$refs.MVP.style.display = "flex";
            }
            ///////////////////////////////////////
            if (this.GotBadges.admin === "false") {
                this.$refs.admin.style.display = "none";
            }
            else {
                this.$refs.admin.style.display = "flex";
            }
            ///////////////////////////////////////
            if (this.GotBadges.humanSearchEngine === "false") {
                this.$refs.humanSearchEngine.style.display = "none";
            }
            else {
                this.$refs.humanSearchEngine.style.display = "flex";
            }
            ///////////////////////////////////////
            if (this.GotBadges.MVP === "false") {
                this.$refs.MVP.style.display = "none";
            }
            else {
                this.$refs.MVP.style.display = "flex";
            }
            ///////////////////////////////////////
            if (this.GotBadges.onARoll === "false") {
                this.$refs.onARoll.style.display = "none";
            }
            else {
                this.$refs.onARoll.style.display = "flex";
            }
            ///////////////////////////////////////
            if (this.GotBadges.roockie === "false") {
                this.$refs.roockie.style.display = "none";
            }
            else {
                this.$refs.roockie.style.display = "flex";
            }
            ///////////////////////////////////////
            if (this.GotBadges.verified === "false") {
                this.$refs.verified.style.display = "none";
            }
            else {
                this.$refs.verified.style.display = "flex";
            }
            ///////////////////////////////////////
            if (this.GotBadges.veteran === "false") {
                this.$refs.veteran.style.display = "none";
            }
            else {
                this.$refs.veteran.style.display = "flex";
            }
            ///////////////////////////////////////
            if (this.GotBadges.goGetter === "false") {
                this.$refs.goGetter.style.display = "none";
            }
            else {
                this.$refs.goGetter.style.display = "flex";
            }
            ///////////////////////////////////////
            if (this.GotBadges.workaholic === "false") {
                this.$refs.workaholic.style.display = "none";
            }
            else {
                this.$refs.workaholic.style.display = "flex";
            }
            ///////////////////////////////////////
            if (this.GotBadges.workaholic1 === "false") {
                this.$refs.workaholic1.style.display = "none";
            }
            else {
                this.$refs.workaholic1.style.display = "flex";
            }
        },
},

}
</script>

<style scoped>
.container {
  display: flex;
  flex-direction: column;
  background-color: #889ea1;
  border-radius: 15px;
  margin: 30px 20px;
  padding: 15px 20px;
}

.info {
  display: flex;
  flex-direction: column;
  width: 96%;
}

.profilePicture {
  display: block;
  width: 100%;
  height: auto;
  cursor: pointer;
  background-size: cover;
  background-position: center center;
  z-index: 2;
  margin-top: -20px;
  margin-bottom: 10px;
  border-radius: 10px;
  box-shadow: 2px 2px 3px black;
}

.topInfo {
  display: flex;
  flex-direction: column;
  border-radius: 10px;
    background: #c4d2d4;
    padding: 10px;
}

.margin {
  margin: 10px 0;
}

.topInfoText {
  display: flex;
  flex-direction: column;
}

.unavailableClass {
    border-radius: 10px;
    background: #7489b8;
    padding-left: 20px;
    margin-bottom: 15px;
}

.userName {
  font-size: 1.8rem;
  margin: 0;
  color: white;
}

.userNameDiv {
  display: flex;
  flex-direction: row;
  align-items: center;
  border: 2px solid #053f51;
  background-color: #7489b8;
  border-radius: 10px;
  padding: 5px 10px;
}

.availableLogo {
  height: 20px;
  margin-right: 5px;
}

.badges {
  display: flex;
  height: 60px;
  width: 60px;
  flex-wrap: wrap;
}

.lightText {
  color: white;
  display: flex;
  justify-content: center;
  width: 100%;
  margin-top: 0;
  padding-top: 10px;
  background: linear-gradient(0deg, rgba(133,139,142,0) 0%, rgba(77,80,83,0.7718487053024334) 100%);
}

.box {
  border: 2px solid #343136;
  border-radius: 10px;
  margin: 5px;
  width: 70px;
  height: 70px;
  justify-content: center;
  align-items: center;
  background: linear-gradient(0deg, rgba(81,89,95,1) 0%, rgba(75,77,78,1) 100%);
  transition: all 500ms ease-in-out;
}

.box:hover {
  transform: translateY(-5px);
  background: linear-gradient(0deg, rgba(161,169,175,1) 0%, rgba(133,139,142,1) 100%);
}

.badgeClass {
  display: flex;
  flex-wrap: wrap;
  justify-content: start;
  border-radius: 8px;
  width: 100%;
  height: auto;
  margin: 10px;
  flex-direction: row;
  padding: 10px;
  background-color: #605b63;
}

.boxAvailable {
  padding: 0 5px 5px 5px;
  border-radius: 10px;
  width: 100%;
}

.row {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  width: 100%;
  margin-top: 10px;
}
.rowContainer {
    background: #c4d2d4;
    margin-top: 20px;
  padding: 0 10px;
  width: 95%;
  border-radius: 15px;
}
.rowContainer2 {
  background-color: #343136;
  margin-top: 20px;
  padding: 0 10px;
  width: 100%;
  border-radius: 15px;
}
.moneyBox {
  display: flex;
  flex-direction: column;
  padding: 0;
  margin: 0;
  width: 100%;
}
.money {
  font-size: 1.5rem;
  font-weight: bolder;
  color: darkgreen;
  background-color: darkseagreen;
  border: darkgreen 1px solid;
  padding: 5px;
  border-radius: 10px;
  margin: 0;
}
.small {
  font-size: small;
}
.calendarB {
  border-radius: 10px;
  background-color: #7489b8;
  padding: 10px;
}
.rowContainerCalendar {
    background: #c4d2d4;
    margin-top: 20px;
  padding: 20px;
  width: 90%;
  border-radius: 15px;
}

.rowContainerCalendarTop {
  display: flex;
  flex-direction: row;
  padding: 10px;
  width: 100%;
  justify-content: space-evenly;
}

.rowContainer2 {
  background-color: #343136;
  margin-top: 20px;
  padding: 0 20px;
  width: 90%;
  border-radius: 15px;
}

.addition {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.hazeContainer {
  top: 0;
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
.haze {
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

.hazeInner {
  display: flex;
  justify-content: center;
  flex-direction: column;
  margin-bottom: 10%;
  text-align: center;
}

.hazeButton {
  width: 50%;
  margin: auto;
  border: none;
  border-radius: 10px;
  background-color: #2A93BC;
  color: white;
  padding: 10px 0;
}
@media only screen and (min-width: 768px) {
  .row{
    flex-direction: unset;
  }
  .container {
    margin: 60px auto;
    max-width: 960px;
    padding: 15px;
  }

  .profilePicture {
    width: 200px;
    height: 200px;
    margin-top: 0;
    margin-bottom: 10px;
  }

  .info {
    margin-left: 20px;
  }

  .topInfo {
    margin-bottom: 20px;
    flex-direction: unset;
  }

  .margin {
    margin: 0 20px;
  }

  .moneyBox {
    width: 40%;
  }

  .rowContainer {
    width: 30%;
    margin: 20px 10px;
  }

  .rowContainerCalendar {
    width: 40%;
    margin: 20px 10px;
  }

  .rowContainer2 {
    width: 30%;
    margin: 20px 10px;
  }
}


</style>
