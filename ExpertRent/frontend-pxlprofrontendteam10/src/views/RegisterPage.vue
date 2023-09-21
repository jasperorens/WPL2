<template>

  <div class="main">

    <div class="register">

      <label for="userName"><b>Username</b></label>
      <input type="text" id="userName" v-model="user.username">


      <label for="lastName"><b>Name</b></label>
      <input type="text" id="lastName" v-model="user.name">

      <label for="firstName"><b>First name</b></label>
      <input type="text" id="firstName" v-model="user.firstname">

      <div class="streetandnumber">
        <div class="street_div">
          <label for="street"><b>Street</b></label>
          <input type="text" id="street" v-model="user.street">
        </div>
        <div class="number_div">
          <label for="streetNumber"><b>Number</b></label>
          <input type="text" id="streetNumber" v-model="user.streetNr">
        </div>
      </div>


      <div class="country_info">
        <div class="country_div">

          <label for="country-input"><b>Country</b></label>
          <input type="text" id="country-input" list="country-options" v-model="user.country" @change="selectCountry(user.country)">
          <datalist id="country-options">
            <option v-for="(country, index) in countries" :value="country.name" :key="index">{{ country.name }}</option>
          </datalist>
        </div>
        <div class="postcode_div">
          <label for="postCode"><b>Postal Code</b></label>
          <input type="text" id="postCode" v-model="user.postalCode">
        </div>
      </div>

      <label for="province"><b>Province/State</b></label>
      <input type="text" id="province" v-model="user.province">


      <div class="telephone">
        <div class="phone_number_div">
          <label for="phoneNumber"><b>Telephone</b></label>
          <input type="text" id="phoneNumber" v-model="user.telephone" @focus="updatePhoneNumber">
        </div>

      </div>

      <label for="email"><b>Email</b></label>
      <input type="text" id="email" v-model="user.email">

      <label for="password"><b>Password</b></label>
      <input type="password" id="password" v-model="user.password">
      <label for="passwordcheck"><b>Verify Password</b></label>
      <input type="password" id="passwordcheck" v-model="user.passwordCheck">


      <button class="register_Button" type="submit" @click.prevent="handleSubmit()">Register</button>
      <div class="error-message" v-if="errorMessages.length > 0" :key="errorMessages.join('-')">
        <ul class="error-messagecolor">
          <li v-for="errorMessage in errorMessages" :key="errorMessage">{{ errorMessage }}</li>
        </ul>
      </div>
    </div>

  </div>

</template>

<script>
    import router from "../router";
    import {useExpertStore} from "@/stores/expertStore";

    const apiUrl = 'https://localhost:7155/api/User/UserJson';

    /// variable voor de post

    let email;
    let password;
    let username;
    let name;
    let firstName;
    let telephone;
    let country;
    let street;
    let streetNr;
    let postalCode;
    let stateProvince;


export default {
  name: "RegisterPage",

  data() {
    return {
      store: useExpertStore(),
      user: {
        password: "",
        passwordCheck: "",
        email: "",
        username: "",
        name: "",
        firstname: "",
        street: "",
        streetNr: "",
        postalCode: "",
        province: "",
        telephone: "",
        country: "",
      },
      //Noodzakelijk voor de countryAPI, landenlijst en telefooncode.
      countries: [],
      telefooncode: '',
      //Errorstorage
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
//Delete telefoonnummer indien wijziging land. Weghalen indien groep niet voor deze feature is.
  watch: {
    telefooncode(newCode) {
      this.user.telephone = '';
    }
  },
  methods: {
    handleSubmit() {
      if(this.register() === true) {
        console.log(this.user)

        console.log("Geen probleem met de checks bro")
      fetch(apiUrl, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(this.user)
      })
        router.push({path: '/login'})
        this.$emit('ReloadPage',1);

      }
    },



    acount(){
      this.register()
    },
    //Indien land gekozen, haal meer info binnen van dat land door de ISO2 code toe te voegen aan de default-URL.
    selectCountry(countryname) {
      let selectedCountry = this.countries.find(country => country.name === countryname);
      let selectedISO = selectedCountry.iso2;
      let countryURL = "https://api.countrystatecity.in/v1/countries/" + selectedISO;
      this.fetchCountryInfo(countryURL);
    },
    logout(){
      router.push({path:'/Login'})
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
            console.log(result);
            console.log(result.phonecode);
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
    register() {
      this.errorMessages = [];
      // check of alles date klopt
      let klopAlles = true
      //Lets voor naar JSON:
      //CheckUsername
      if(this.checkUserName() === false)
      {
        klopAlles = false;
        console.log("UsernameProbleem");
      }
      //Country en StateProvince
      stateProvince = this.user.province;
      country = this.user.country;
      //CheckName
      if(this.checkName() === false)
      {
        klopAlles = false;
        console.log("NameProbleem");
      }
      //CheckFirstName
      if(this.checkFirstName() === false)
      {
        klopAlles = false;
        console.log("FirstnameProbleem");
      }
      //CheckStreetNumber
      if(this.checkAdress() === false)
      {
        klopAlles = false;
        console.log("AdressProbleem");
      }
      //Check PhoneNumber
      if(this.checkPhone() === false)
      {
        klopAlles = false;
        console.log("PhoneProbleem");
      }
      // check paswoord
      if(this.checkPassword() === false)
      {
        klopAlles = false;
        console.log("PWProbleem");
      }

      //Check Email
      if(this.checkEmail() === false)
      {
        klopAlles = false;
        console.log("MailProbleem");
      }


//Submit if everything is OK.
      if(klopAlles === true){
        return true;
      }
      else{
        return false;
      }
    },
    checkUserName(){
      if ((/\s/.test(this.user.username)) === false && (this.user.username).length > 0)
      {
        username = this.user.username;
        return true;
      } else {
        this.errorMessages.push("Your username cannot contain whitespaces.");
        return false;
      }

    },
    checkName(){
      if ((this.user.name).length > 0)
      {
        name = this.user.name;
        return true;
      }
      else
      {
        this.errorMessages.push("Your name cannot be left empty.");
        return false;

      }
    },
    checkFirstName(){
      let FirstNameCheck = true
      if ((this.user.firstname).length > 0)
      {
        firstName = this.user.firstname;

      }
      else
      {
        this.errorMessages.push("Your first name cannot be empty.");
        FirstNameCheck = false;
      }
      //CheckStreet
      if ((this.user.street).length > 0)
      {
        street = this.user.street;
      }
      else
      {
        this.errorMessages.push("Your street cannot be empty.");
        FirstNameCheck = false;
      }
      return FirstNameCheck;
    },
    checkAdress(){
      let AdressCheck = true;
      if ((this.user.streetNr).length > 0)
      {
        streetNr = this.user.streetNr;
      }
      else
      {
        this.errorMessages.push("Street number is mandatory.");
        AdressCheck = false;
      }
      // CheckPostalCode
      if ((this.user.postalCode).length > 0)
      {
        postalCode = this.user.postalCode;
      }
      else
      {
        this.errorMessages.push("Your postal code can only contain numbers");
        AdressCheck = false;
      }
      return AdressCheck;
    },
    checkPhone(){

      if (/^\+[0-9]+(-[0-9]+)*$/.test(this.user.telephone))
      {
        telephone = this.user.telephone;
        return true;
      } else {
        this.errorMessages.push("Your phone number can only contain numbers");
        return  false;
      }
    },
    checkEmail(){
      if (this.user.email.includes('@') === true && (this.user.email).length >= 3)
      {
        /*alert("Email is good :)")*/
        email = this.user.email;
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
      /*alert(PasswordCheck);*/
      if (this.user.passwordCheck === this.user.password) {

        if (this.user.password.length > 8) {

          if ((/\s/.test(this.user.password)) === false) {
            if (hasUpperCase) {

              if (hasLowerCase) {

                if (hasNumber) {

                  if (this.user.password !== this.user.username) {
                    /*alert("your passwords are oke")*/
                    password = this.user.password;
                  } else {
                    this.errorMessages.push("Your passwords can't be the same as your username.")
                    PasswordCheck = false;

                  }

                } else {
                  this.errorMessages.push("Your passwords must contain a number.")
                  PasswordCheck = false;

                }

              } else {
                this.errorMessages.push("Your passwords need at least 1 lowercase letter.")
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
      /*this.errorMessages.push(PasswordCheck)*/;
      return PasswordCheck;


    }
  },
  computed:{
    id() {
      var length = this.store.getUsers.length + 1
      console.log(this.store.getUsers())
      return length
    },
  }
}

</script>

<style scoped>
.main {
  background-image: url("../assets/Register_Background.jpg");
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
  display: flex;
  justify-content: center;

  align-items: center;
  width: 100%;
}

.register {
  background-color: white;
  display: flex;
  align-items: flex-start;
  justify-content: center;
  flex-direction: column;
  margin-top: 2%;
  margin-bottom: 2%;
  padding: 20px;
  border-radius: 25px;
  width: 70%;
  box-shadow: 2px 2px 5px black;
}

form {
  background-color: #fff;
  border-radius: 5px;
  padding: 20px;
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

.code_div {
  display: flex;
  flex-direction: column;
  width: 100%;
}

.register_Button {
  background-color: #2A93BC;
  display: flex;
  justify-self: center;
  align-self: center;
  justify-content: center;
  font-size: 20px;
}

@media screen and (min-width: 460px) {

  .register {
    background-color: white;
    display: flex;
    align-items: flex-start;
    justify-content: center;
    flex-direction: column;
    margin-top: 2%;
    margin-bottom: 2%;
    padding: 20px;
    border-radius: 25px;
    width: 70%;
    box-shadow: 2px 2px 5px black;
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

}

@media screen and (min-width: 530px) {

  .register {
    width: 60%;
  }

}

@media screen and (min-width: 630px) {

  .register {
    width: 50%;
  }

}

@media screen and (min-width: 730px) {

  .register {
    width: 40%;
  }

}

@media screen and (min-width: 880px) {

  .register {
    width: 30%;
  }

}


</style>
