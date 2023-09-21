<template>

  <div class="main">

    <div class="upgrade">

      <label class="mainLabel"><b>Become an expert</b></label>

      <label for="profession"><b>Profession</b></label>
      <input type="text" id="profession" v-model="expert.profession">

      <label for="specialization"><b>Specialization</b></label>
      <input type="text" id="specialization" v-model="expert.specialization">

      <label for="category"><b>Category</b></label>
      <input type="text" id="category" list="category-options" v-model="expert.category">
      <datalist id="category-options">
        <option value="STEM"></option>
        <option value="Business"></option>
        <option value="Creative"></option>
        <option value="Service"></option>
        <option value="Trade"></option>
      </datalist>
      <label for="price"><b>$ Price/Hour</b></label>
      <input type="text" id="price" v-model="expert.price">
      <button class="register_Button" type="submit" @click.prevent="handleSubmit()">Upgrade Now</button>
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

const apiUrl = 'https://localhost:7155/api/Expert/ExpertJson';
export default {
  name: "UpgradePage",
  data() {
    return {
      expert: {
        id: localStorage.getItem("id"),
        profession: "",
        specialization: "",
        category: "Trade",
        price: 0,

      },
      errorMessages: [],
    }
  },

  methods: {
    handleSubmit() {
      console.log(this.expert)
      if (this.register() === true) {
        const response = fetch(apiUrl, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(this.expert)
      });
        localStorage.setItem('userIsExpert', '1');
        router.push({path:'/'});
        this.$emit('ReloadPage',1);
    }

    },
    register() {
      this.errorMessages = [];
      // check of alles date klopt
      let klopAlles = true
      //Lets voor naar JSON:


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

      if (this.checkCategory() === false) {
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

    checkProfession(){
      if ((this.expert.profession).length > 0)
      {
        return true;
      }
      else
      {
        this.errorMessages.push("Your profession cannot be left empty.");
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
        this.errorMessages.push("Your specialization cannot be left empty.");
        return false;
      }
    },
    checkCategory() {
      if ((this.expert.category).length > 0)
      {
        return true;
      }
      else
      {
        this.errorMessages.push("Your category cannot be left empty.");
        return false;
      }
    },
    checkPrice(){

      if (isNaN((this.expert.price)) || this.expert.price < 0  || this.expert.price > 1000)
      {
        console.log(this.expert.price)
        this.errorMessages.push("Please give a valid hourly rate (must be between 0 and 1000).");
        return false;
      }
      else
      {
        return true;
      }
    },

  },
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
  height: 85.3vh;
}

.upgrade {
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

.mainLabel{
  display: flex;
  align-self: center;
  margin: 10px;
  font-size: x-large;
  color: #2A93BC;
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
input[type=text]{
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

.register_Button {
  background-color: #2A93BC;
  display: flex;
  justify-self: center;
  align-self: center;
  justify-content: center;
  font-size: 20px;
}

@media screen and (min-width: 460px) {
    .upgrade {
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

}

@media screen and (min-width: 530px) {

  .upgrade {
    width: 60%;
  }

}

@media screen and (min-width: 630px) {

  .upgrade {
    width: 50%;
  }

}

@media screen and (min-width: 730px) {

  .upgrade {
    width: 40%;
  }

}

@media screen and (min-width: 880px) {
  .upgrade {
    width: 30%;
  }
}
</style>