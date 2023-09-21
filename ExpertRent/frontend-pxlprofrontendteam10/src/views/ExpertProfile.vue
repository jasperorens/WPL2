<template>

  <div class="main">
    <!-- div voor profilefoto -->
    <div id="image_upload" class="profilePicture" :style="{ 'background-image': `url(${imageData})` }" @click="selectFile">
    <span v-if="!imageData" class="placeholder">
      Choose an Image
    </span>
      <input  class="file-input" ref="fileInput" type="file" @input="onSelectFile">
    </div>
    <!-- div voor profile page (heeft zelfde layout als register form), gegevens in input fields worden gefetched
        en dan getoond in de input fields -->
    <div class="register">

      <!-- username input -->
      <label for="userName"><b>Username</b></label>
      <input  v-if="settingsTrue === true"  class="readonly" type="text" id="userName" v-model="user.username" readonly="readonly">
      <label v-else class="marginDown">{{user.username}}</label>

      <!-- name input -->
      <label for="lastName"><b>Name</b></label>
      <input v-if="settingsTrue === true" type="text" id="lastName" v-model="user.name">
      <label v-else class="marginDown">{{user.name}}</label>
      <!-- first name input -->
      <label for="firstName"><b>First name</b></label>
      <input v-if="settingsTrue === true" type="text" id="firstName" v-model="user.firstname">
      <label v-else class="marginDown">{{user.firstname}}</label>
      <!-- job input -->
      <label for="profession"><b>Profession</b></label>
      <input v-if="settingsTrue === true" type="text" id="profession" v-model="expert.profession">
      <label v-else class="marginDown">{{expert.profession}}</label>
      <!-- speciality input -->
      <label for="specialization"><b>Specialization</b></label>
      <input v-if="settingsTrue === true" type="text" id="specialization" v-model="expert.specialization">
      <label v-else class="marginDown">{{expert.specialization}}</label>
      <!-- category input -->
      <label for="category"><b>Category</b></label>
      <input v-if="settingsTrue === true" type="text" id="category" list="category-options" v-model="expert.category">
      <label v-else class="marginDown">{{expert.category}}</label>
      <datalist id="category-options">
        <option value="STEM"></option>
        <option value="Business"></option>
        <option value="Creative"></option>
        <option value="Service"></option>
        <option value="Trade"></option>
      </datalist>


      <!-- Price input -->
      <label for="price"><b>$ Price/Hour</b></label>
      <input v-if="settingsTrue === true"  type="text" id="price" v-model="expert.price">
      <label v-else class="marginDown">{{expert.price}}</label>


      <!-- street and number input, staan in 1 div om naast elkaar te tonen -->
      <div class="streetandnumber">
        <div class="street_div">
          <label for="street"><b>Street</b></label>
          <input v-if="settingsTrue === true" type="text" id="street" v-model="user.street">
          <label v-else class="marginDown">{{user.street}}</label>
        </div>
        <div class="number_div">
          <label for="streetNumber"><b>Number</b></label>
          <input v-if="settingsTrue === true" type="text" id="streetNumber" v-model="user.streetnr">
          <label v-else class="marginDown">{{user.streetnr}}</label>
        </div>
      </div>

      <!-- iput voor country deel staat in commentaar voor als we in toekomst nog nodig hebben -->
      <div class="country_info">
        <div class="country_div">

          <label for="country-input"><b>Country</b></label>
          <input v-if="settingsTrue === true" type="text" id="country-input" list="country-options" v-model="user.country" @change="selectCountry(user.country)">
          <label v-else class="marginDown">{{user.country}}</label>
          <datalist id="country-options">
            <option v-for="(country, index) in countries" :value="country.name" :key="index">{{ country.name }}</option>
          </datalist>
        </div>
        <!-- input voor postcode -->
        <div class="postcode_div">
          <label for="postCode"><b>Postal Code</b></label>
          <input v-if="settingsTrue === true" type="text" id="postCode" v-model="user.postalcode">
          <label v-else class="marginDown">{{user.postalcode}}</label>
        </div>
      </div>
      <!-- input voor province -->
      <label for="province"><b>Province/State</b></label>
      <input v-if="settingsTrue === true" type="text" id="province" v-model="user.province">
      <label v-else class="marginDown">{{user.province}}</label>

      <!-- input voor telefoon en code telefoon -->
      <div class="telephone">
        <div class="phone_number_div">
          <label for="phoneNumber"><b>Telephone</b></label>
          <input v-if="settingsTrue === true" type="text" id="phoneNumber" v-model="user.telephone" @focus="updatePhoneNumber">
          <label v-else class="marginDown">{{user.telephone}}</label>
        </div>

      </div>
      <!-- input voor email -->
      <label for="email" ><b>Email</b></label>
      <input v-if="settingsTrue === true" class="readonly" type="text" id="email" v-model="user.email" readonly="readonly">
      <label v-else class="marginDown">{{user.email}}</label>
      <!-- settings button -->
      <button class="settings_Button" type="submit" @click.prevent="setting">Settings</button>
      <button class="settings_Button" type="submit" @click.prevent="Days">Available Days</button>
      <button class="settings_Button" type="button" @click.prevent="logout">Logout</button>
      <div id="Days">
        <label for="mon">Monday</label>
        <input id="mon" type="checkbox" value="Monday" v-model="availabledays.monday">

        <label for="tue">Tuesday</label>
        <input id="tue" type="checkbox" value="Tuesday" v-model="availabledays.tuesday">

        <label for="wed">Wednesday</label>
        <input id="wed" type="checkbox" value="Wednesday" v-model="availabledays.wednesday">

        <label for="thu">Thursday</label>
        <input id="thu" type="checkbox" value="Thursday" v-model="availabledays.thursday">

        <label for="fri">Friday</label>
        <input id="fri" type="checkbox" value="Friday" v-model="availabledays.friday">

        <label for="sat">Saturday</label>
        <input id="sat" type="checkbox" value="Saturday" v-model="availabledays.saturday">

        <label for="sun">Sunday</label>
        <input id="sun" type="checkbox" value="Sunday" v-model="availabledays.sunday">

        <button class="settings_Button" type="submit" @click.prevent="submitDays">Save Days</button>
      </div>

      <!-- input voor passwoord -->
      <div id="settings">

        <div class="currentPassword">
          <label for="password"><b>Current password</b></label>
          <input type="password" id="password" v-model="currentPassword">
        </div>
        <!-- send code button -->
        <button class="settings_Button" type="submit" @click.prevent="sendEmail">Send Code</button>
        <!--  -->
        <div class="emailCode">
          <label for="emailCode"><b>Password Code</b></label>
          <input type="text" id="emailCode" v-model="inputcode">
        </div>

        <div class="newPassword">
          <label for="newPassword"><b>New Password</b></label>
          <input type="password" id="newPassword" v-model="newPassword">
        </div>

        <div class="newPassword">
          <label for="confirmNewPassword"><b>Confirm New Password</b></label>
          <input type="password" id="confirmNewPassword" v-model="newPasswordCheck">
        </div>

        <button class="settings_Button" type="submit" @click.prevent="passwordcheck">Change password</button>

        <button class="settings_Button" type="submit" @click.prevent="handleSubmit">Save profile changes</button>
        <div class="error-message" v-if="errorMessages.length > 0" :key="errorMessages.join('-')">
          <ul class="error-messagecolor">
            <li  v-for="errorMessage in errorMessages" :key="errorMessage">{{ errorMessage }}</li>
          </ul>
        </div>

      </div>

    </div>

  </div>

</template>

<script>
/* imports van pinia store, deze doen nu niets (30/03/2023) */
import {useExpertStore} from "../stores/ExpertStore";
import {storeToRefs} from 'pinia'
/* import van router */
import router from "../router";

const apiUrl = 'https://localhost:7155/api/User/editUserInfo';
const emailUrl = 'https://localhost:7155/api/Email/ReceiveEmailCode';
const expertUrl = 'https://localhost:7155/api/Expert/editExpertInfo';
const apiUrlForPass = 'https://localhost:7155/api/User/ChangePassword';
const apiDaysURL = 'https://localhost:7155/api/Unavailable/UpdateDays';

export default {
name: "ExpertProfile",

  /*   setup(){
    const store = useExpertStore()
    const {getUsers} = storeToRefs(store)
    store.getAllUsers()
    return console.log({store, getUsers})
  },*/

  data() {
    return {
      settingsTrue: false,
      newPassword: "",
      newPasswordCheck: "",
      currentPassword: "",
      inputcode: "",
      user: {
        password: "",
        email: "",
        username: "",
        name: "",
        firstname: "",
        street: "",
        streetnr: "",
        postalcode: "",
        province: "",
        countryCode: "",
        telephone: "",
        country: "",
      },
      expert: {
        profession: "",
        specialization: "",
        category: "",
        price: "",
        id: "",
        active: 0,
      },
      passchange:{
        newpass: "",
        emailcode: "",
        email: "",
      },
      Codemail:{
        email:"",
      },
        imageData: null,
      //Noodzakelijk voor de countryAPI, landenlijst en telefooncode.
      countries: [],
      telefooncode: '',
      availabledays: {
        expertId: 0,
        monday: false,
        tuesday:false,
        wednesday: false,
        thursday: false,
        friday: false,
        saturday: false,
        sunday: false,

      },
      errorMessages: [],
    }
  },

  mounted() {

    // Landen binnenhalen uit API
    fetch("https://api.countrystatecity.in/v1/countries", {
      method: 'GET',
      headers: {
        'X-CSCAPI-KEY': 'b1V5NHl0UEI5SkR0YjcxOXdWbHF4aVBIN01BeDdxZzY5d0RKcFlnTg=='
      }
    })
        .then(response => response.json())
        .then(result => {
          this.countries = result;
        })
        .catch(error => console.log('error', error));
  },

  methods: {
    setting() {
      this.settingsTrue = true;
      const settingsDiv = document.getElementById('settings');
      if (settingsDiv.style.display === "flex") {
        settingsDiv.style.display = "none";
      } else {
        settingsDiv.style.display = "flex";
      }
    },
    Days() {
      const DaysDiv = document.getElementById('Days');
      if (DaysDiv.style.display === "flex") {
        DaysDiv.style.display = "none";
      } else {
        DaysDiv.style.display = "flex";
      }
    },
    generateRandomNumber() {
      return Math.floor(Math.random() * 900000) + 100000;
    },
    sendEmail(){
      this.Codemail.email = this.user.email;
      if(this.user.password === this.currentPassword){
        this.sendEmailCode()
      }
      else{
        alert('Password does not match current')
      }

    },
    async sendEmailCode() {
      alert('Email code has been sent')
      fetch(emailUrl, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(this.Codemail)
      })

    },

    passwordcheck(){
      var length = this.newPassword.length
      if(length > 8){
        if(this.newPassword.toString() == this.newPasswordCheck.toString()){
          this.user.password = this.newPassword;
          this.handePasswordSubmitWithoutCheck()
        }
        else {
          alert('Passwords are not the same')
        }

      } else{
        alert('Password need more than 8 characters')
      }
    },

    async handePasswordSubmitWithoutCheck() {
      this.passchange.newpass = this.newPassword;
      this.passchange.emailcode = this.inputcode;
      this.passchange.email = this.user.email;
      this.newPassword = "";
      this.newPasswordCheck = "";
      this.inputcode = "";
      this.currentPassword = "";
      this.setting();

      console.log(JSON.stringify(this.passchange))
      await fetch(apiUrlForPass, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(this.passchange)
      })

    },
    logout(){

      let user = localStorage.getItem("user")
      if(user !== null) {
        localStorage.removeItem("user");
        localStorage.removeItem("id");
        localStorage.removeItem('userIsExpert');
        localStorage.removeItem('adminRights');

      }
      router.push({path:'/'});
      this.$emit('ReloadPage',1)
    },
    registerNoPass() {
      this.errorMessages = [];
      // check of alles date klopt
      let klopAlles = true
      //Lets voor naar JSON:
      //CheckUsername
      if (this.checkUserName() === false) {
        klopAlles = false;
      }

      //CheckName
      if (this.checkName() === false) {
        klopAlles = false;
      }
      //CheckFirstName
      if (this.checkFirstName() === false) {
        klopAlles = false;
      }
      //CheckStreetNumber
      if (this.checkAdress() === false) {
        klopAlles = false;
      }
      //Check PhoneNumber
      if (this.checkPhone() === false) {
        klopAlles = false;
      }
      //Check Email
      if (this.checkEmail() === false) {
        klopAlles = false;
      }
      //CheckProfession
      if (this.checkProfession() === false) {
        klopAlles = false;
      }
      //CheckSpecialization
      if (this.checkSpecialization() === false) {
        klopAlles = false;
      }

      //Check Price
      if (this.checkPrice() === false) {
        klopAlles = false;
      }

      if(klopAlles === true)
      {
        return true;
      }
      else{
        return false;
      }
    },
    register() {
      this.errorMessages = [];
      // check of alles date klopt
      let klopAlles = true
      //Lets voor naar JSON:
      if (this.checkUserName() === false) {
        klopAlles = false;
      }
      //CheckName
      if (this.checkName() === false) {
        klopAlles = false;
      }
      //CheckFirstName
      if (this.checkFirstName() === false) {
        klopAlles = false;
      }
      //CheckStreetNumber
      if (this.checkAdress() === false) {
        klopAlles = false;
      }
      //Check PhoneNumber
      if (this.checkPhone() === false) {
        klopAlles = false;
      }
      // check paswoord
      if (this.checkPassword() === false) {
        klopAlles = false;
      }
      //Check Email
      if (this.checkEmail() === false) {
        klopAlles = false;
      }

      //CheckProfession
      if (this.checkProfession() === false) {
        klopAlles = false;
      }
      //CheckSpecialization
      if (this.checkSpecialization() === false) {
        klopAlles = false;
      }

      //Check Price
      if (this.checkPrice() === false) {
        klopAlles = false;
      }

      if(klopAlles === true)
      {
        return true;
      }
      else{
        return false;
      }
    },
    checkUserName(){
      if ((/\s/.test(this.user.username)) === false && (this.user.username).length > 0)
      {

        return true;
      } else {
        this.errorMessages.push("Your username cannot be whitespaces");
        return false;
      }

    },
    checkName(){
      if ((this.user.name).length > 0)
      {
        return true;
      }
      else
      {
        this.errorMessages.push("Your name cannot contain whitespaces");
        return false;

      }
    },
    checkFirstName(){
      let FirstNameCheck = true
      if ((this.user.firstname).length > 0)
      {

      }
      else
      {
        this.errorMessages.push("Your first name cannot contain whitespaces");
        FirstNameCheck = false;
      }
      //CheckStreet
      if ((this.user.street).length > 0)
      {

      }
      else
      {
        this.errorMessages.push("Your street cannot be whitespaces");
        FirstNameCheck = false;
      }
      return FirstNameCheck;
    },
    checkAdress(){
      let AdressCheck = true;
      // CheckPostalCode
      //console.log(this.user.streetnr)
      if ((parseInt(this.user.streetNr)) < 0)
      {
        this.errorMessages.push("Street number is mandatory.");
        AdressCheck = false;
      }
      if (!/^\d+$/.test(this.user.postal) && (this.user.postal) > 0)
      {
        this.errorMessages.push("Your postal code can only contain numbers");
        AdressCheck = false;
      }
      return AdressCheck;
    },
    checkPhone(){

      if (/^\+[0-9]+(-[0-9]+)*$/.test(this.user.telephone))
      {

        return true;
      } else {
        this.errorMessages.push("Your phone number can only contain numbers and must start with +.");
        return  false;
      }
    },
    checkEmail(){
      if (this.user.email.includes('@') === true && (this.user.email).length >= 3)
      {


      } else {
        this.errorMessages.push("Email must contain '@' and at least 3 characters.");
        return false;
      }
    },
    checkPassword(){
      let PasswordCheck = true;
      const hasUpperCase = /[A-Z]/.test(this.user.password);
      const hasLowerCase = /[a-z]/.test(this.user.password);
      const hasNumber = /\d/.test(this.user.password);
      if (this.user.passwordCheck === this.user.password) {

        if (this.user.password.length > 8) {

          if ((/\s/.test(this.user.password)) === false) {
            if (hasUpperCase) {

              if (hasLowerCase) {

                if (hasNumber) {

                  if (this.user.password !== this.user.username) {
                    /*alert("your passwords are oke")*/

                  } else {
                    this.errorMessages.push("Your passwords can't be the same as your username")
                    PasswordCheck = false;
                  }

                } else {
                  this.errorMessages.push("Your passwords must contain a number")
                  PasswordCheck = false;
                }

              } else {
                this.errorMessages.push("Your passwords need at least 1 lowercase letter")
                PasswordCheck = false;
              }
            } else {
              this.errorMessages.push("Your passwords need at least 1 uppercase letter")
              PasswordCheck = false;
            }
          } else {
            this.errorMessages.push("Your passwords can't have whitespaces")
            PasswordCheck = false;
          }

        } else {
          this.errorMessages.push("Your passwords needs to have more than 8 characters")
          PasswordCheck = false;
        }

      } else {
        this.errorMessages.push("Your passwords do no match")
        PasswordCheck = false;
      }
      return PasswordCheck;
    },
    checkProfession(){
      if ((this.expert.profession).length > 0)
      {
        return true;
      }
      else
      {
        this.errorMessages.push("Your profession cannot be left empty");
        return false;
      }
    },
    checkSpecialization() {
      if ((this.expert.specialization).length > 0)
      {
        return true;
      }
      else
      {
        this.errorMessages.push("Your specialization cannot be left empty");
        return false;
      }
    },
    checkPrice(){

      if (isNaN((this.expert.price)) || this.expert.price < 0)
      {
        this.errorMessages.push("Please give a valid hourly rate (must be 0 or higher).");
        return false;
      }
      else
      {
        return true;
      }
    },

    async handleSubmit() {
      this.settingsTrue = false;
      if(this.registerNoPass() === true){
        if(this.expert.active === true){
          this.expert.active = 1;
        }
        else{
          this.expert.active = 0;
        }
        this.expert.price = parseInt(this.expert.price)
        console.log(JSON.stringify(this.user))
        console.log(JSON.stringify(this.expert));

      await fetch(apiUrl, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(this.user)
      })
        await fetch(expertUrl, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(this.expert)
        })
      }
    },
    async submitDays(){
      this.availabledays.expertId = this.expert.id;
      await fetch(apiDaysURL, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(this.availabledays)
      })
    },

    getBackgroundUrl()
    {
      let element = document.getElementById("image_upload");
      let style = window.getComputedStyle(element);
      let backgroundImage = style.getPropertyValue("background-image");
      let commalocation = backgroundImage.indexOf(",") + 1;
      let finalLink = backgroundImage.slice(commalocation, -2);
      this.user.image = finalLink;
    },
    //Indien land gekozen, haal meer info binnen van dat land door de ISO2 code toe te voegen aan de default-URL.
    selectCountry(countryname) {
      let selectedCountry = this.countries.find(country => country.name === countryname);
      let selectedISO = selectedCountry.iso2;
      let countryURL = "https://api.countrystatecity.in/v1/countries/" + selectedISO;
      this.fetchCountryInfo(countryURL);
    },
    //Info-binnenhaler
    fetchCountryInfo(countryURL) {
      fetch(countryURL, {
        method: 'GET',
        headers: {
          'X-CSCAPI-KEY': 'b1V5NHl0UEI5SkR0YjcxOXdWbHF4aVBIN01BeDdxZzY5d0RKcFlnTg=='
        }
      })
          .then(response => response.json())
          .then(result => {
            this.telefooncode = result.phonecode;
            if (!this.telefooncode.startsWith("+")) {
              this.telefooncode = "+" + this.telefooncode;
            }
            console.log(this.telefooncode);
          })
          .catch(error => console.log('error', error));
    },
    //Indien telefoonnummervakje leeg, plaats telefooncode wanneer user klikt op het vakje.
    updatePhoneNumber() {
      if (!this.user.telephone) {
        this.user.telephone = `${this.telefooncode}`;
      }
    },
    /* udated mailto van chatgpt staat momenteel in commentaar omdat we nog geen email van website hebben */
    sendMail() {
      const nodemailer = require('nodemailer');

// configureren van email verzender dingus
      let transporter = nodemailer.createTransport({
        service: 'gmail',
        auth: {
          user: 'expertrent1@gmail.com',
          pass: 'aliaseruja1'
        }
      });

// alle nodige data voor de email invullen

      let mailOptions = {
        from: 'expertrent1@gmail.com', // sender address
        to: this.user.email, // list of receivers
        subject: 'Hello', // Subject line
        text: Math.floor(Math.random() * 900000) + 100000, // plain text body
        html: '<b>Hello world!</b>' // html body
      };

// mail versturen en errors opvangen
      transporter.sendMail(mailOptions, (error, info) => {
        if (error) {
          return console.log(error);
        }
        console.log('Message sent: %s', info.messageId);
      });
      /* huidige mailto, wordt nog vervangen */

      /*var randomNumber = Math.floor(Math.random() * 900000) + 100000; // generates a random 6-digit number*/
      /*var email = 'mailto:"' + this.user.email + '"?subject=Password change&body=' + randomNumber;
      window.location.href = email;*/
    },

    changePassword() {
      if(this.newPassword === this.newPasswordCheck && this.currentPassword === this.user.password) {
        fetch('https://localhost:7155/api/User/ChangePassword?name=' + this.user.username, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(this.newPassword)
        })

      } else {
        console.log("incorrect password")
      }
    },


    getUser() {
      fetch('https://localhost:7155/api/User/GetUserByName?username=' + localStorage.getItem("user"))
          .then(res => res.json())
          .then(data => {
            console.log(data[0]);
            this.user = data[0];
          });

      if(localStorage.getItem("id") !== null)
      {
        fetch('https://localhost:7155/api/Expert/GetExpertById?id=' + localStorage.getItem("id"))
            .then(res => res.json())
            .then(data => {
              console.log('data log bool', data[0].active);

              this.expert = data[0];

                if('https://expertrent.blob.core.windows.net/images/' + this.expert.id + ".jpg'" !== null) {
                    this.imageData = 'https://expertrent.blob.core.windows.net/images/' + this.expert.id + ".jpg";
                }
            });
      }

    },
    selectFile () {
      this.$refs.fileInput.click()

    },
      onSelectFile () {
          const input = this.$refs.fileInput
          const files = input.files
          if (files && files[0]) {
              const reader = new FileReader
              reader.onload = e => {
                  const imageByte =  reader.result;
                  fetch('https://localhost:7155/api/Image/SendImage?userid=' + this.expert.id, {
                      method: 'POST',
                      headers: {
                          'Content-Type': 'application/json'
                      },
                      body: JSON.stringify({ image: imageByte })
                  })
                  this.imageData = e.target.result
              }
              reader.readAsDataURL(files[0])
              this.$emit('input', files[0])


          }
      },
  },

  beforeMount() {
    this.getUser()
  }
}
</script>

<style scoped>
.main {
  background-image: url("../assets/ProfileBackground.png");
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 100%;
}

.readonly{
  background: lightgray;
}
.profilePicture {
  display: block;
  width: 150px;
  height: 150px;
  cursor: pointer;
  background-size: cover;
  background-position: center center;
  z-index: 2;
  margin-top: 2%;
  margin-bottom: -30px;
  border-radius: 20px;
  box-shadow: 2px 2px 3px black;

}
.placeholder {
  background: #F0F0F0;
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  color: #333;
  font-size: 18px;
  font-family: Helvetica;
  border-radius: 20px;
  box-shadow: 2px 2px 3px black;

}
.placeholder:hover {
  background: #E0E0E0;
}
.file-input {
  display: none;
}

#settings{
  display: none;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  align-self: center;
  justify-self: center;
  padding: 20px;
}
#Days{
  display: none;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  align-self: center;
  justify-self: center;
  padding: 20px;
}
.marginDown {
  margin-top: 10px;
  margin-bottom: 20px;
}
.register {
  background-color: white;
  display: flex;
  align-items: flex-start;
  justify-content: center;
  flex-direction: column;
  margin-bottom: 2%;
  padding: 20px;
  padding-top: 50px;
  border-radius: 25px;
  width: 80%;
  box-shadow: 2px 2px 5px black;
}

form {
  background-color: #fff;
  border-radius: 5px;
  padding: 50px;
  width: 100%;
  margin: 50px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
}
h2 {
  text-align: center;
}
input[type=text],
input[type=password] {
  width: 100%;
  padding: 12px 20px;
  margin: 8px 0;
  display: inline-block;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
}
button {
  background-color: #4caf50;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  width: 80%;
}
button:hover {
  background-color: #45a049;
}



input {
  display: flex;
  justify-self: center;
  align-self: center;
  width: 100%;
}

.streetandnumber {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  width: 100%;
}

.street_div {
  display: flex;
  flex-direction: column;
  width: 100%;
}

.number_div {
  display: flex;
  flex-direction: column;
  width: 100%;
}

.country_info {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  width: 100%;
}

.country_div {
  display: flex;
  flex-direction: column;
  width: 100%;
}

.postcode_div {
  display: flex;
  flex-direction: column;
  width: 100%;
}

.telephone {
  display: flex;
  width: 100%;
}

.phone_number_div {
  display: flex;
  flex-direction: column;
  width: 100%;
}

.settings_Button {
  background-color: #2A93BC;
    border-radius: 10px;
  color: white;
  display: flex;
  justify-self: center;
  align-self: center;
  justify-content: center;
  font-size: 20px;
  width: 200px;
}

.currentPassword{
  display: flex;
  flex-direction: column;
  width: 100%;
}

@media screen and (min-width: 460px) {

  .register {
    background-color: white;
    display: flex;
    align-items: flex-start;
    justify-content: center;
    flex-direction: column;
    margin-bottom: 2%;
    padding: 20px;
    padding-top: 50px;
    border-radius: 25px;
    width: 80%;
    box-shadow: 2px 2px 5px black;
  }
  .profilePicture {
    display: block;
    width: 200px;
    height: 200px;
    cursor: pointer;
    background-size: cover;
    background-position: center center;
    z-index: 2;
    margin-top: 2%;
    margin-bottom: -30px;
    border-radius: 20px;
    box-shadow: 2px 2px 3px black;

  }

  input {
    display: flex;
    justify-self: center;
    align-self: center;
    width: 100%;
  }

  .streetandnumber {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    width: 100%;
  }

  .street_div {
    display: flex;
    flex-direction: column;
    width: 70%;
  }

  .number_div {
    display: flex;
    flex-direction: column;
    width: 25%;
  }

  .country_info {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    width: 100%;
  }

  .country_div {
    display: flex;
    flex-direction: column;
    width: 60%;
  }

  .postcode_div {
    display: flex;
    flex-direction: column;
    width: 35%;
  }

  .telephone {
    display: flex;
    width: 100%;
  }

  .phone_number_div {
    display: flex;
    flex-direction: column;
    width: 100%;
  }

  .newPassword{
    display: flex;
    flex-direction: column;
    width: 100%;
  }

}

@media screen and (min-width: 530px) {

  .register {
    width: 70%;
  }

}

@media screen and (min-width: 630px) {

  .register {
    width: 60%;
  }

}

@media screen and (min-width: 730px) {

  .register {
    width: 50%;
  }

}

@media screen and (min-width: 880px) {

  .register {
    width: 40%;
  }

}


</style>
