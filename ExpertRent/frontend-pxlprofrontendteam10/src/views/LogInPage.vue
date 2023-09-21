<template>

  <div class="main">

    <div class="register">

      <label for="userName"><b>Username</b></label>
      <input type="text" id="userName" v-model="username">

      <label for="password"><b>Password</b></label>
      <input type="password" id="password" v-model="password">

      <button class="login_Button" type="submit" @click.prevent="login">Login</button>

      <router-link to="/RegisterPage" class="register_Button">
        <p>Register Account</p>
      </router-link>
    </div>
  </div>

</template>

<style scoped>
.main {
  background-image: url("../assets/LoginBackground.png");
  background-repeat: repeat;
  background-position: center;
  background-size: contain;
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 85.4vh;
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

.register_Button {

}

.login_Button{
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

<script>
import router from "@/router";
import {useExpertStore} from "@/stores/expertStore";

export default {
  name: 'LogInPage',

  emits: ['LoginWerkt', 'idIfExpert'],

  data() {
    return {
      username: "",
      password: "",
      rememberMe: false,
      store: useExpertStore()
    };
  },

  methods: {
    login() {
        fetch('https://localhost:7155/api/User/GetUserByName?username=' + this.username)
            .then(res => res.json())
            .then(data => {
              const password = data[0].password;
              const userID = data[0].id;
// perform login logic here using this.username and this.password
              if(this.password === password){
                this.$emit('loginWerkt',1)
                localStorage.setItem("user", this.username)
                localStorage.setItem("id", data[0].id)
                //alert(this.username)
                router.push({path:'/'})
                //alert(userID);
                this.$emit('idIfExpert', userID); // emit the idIfExpert event with the ide value
              }
              else{
                alert('Wrong password')
              }

            });
      /*localStorage.setItem("ExpertArray", this.experts)
      alert(localStorage.getItem("ExpertArray"))
      console.log("Checkthis:")
      console.log(localStorage.getItem("ExpertArray"))*/
    },


  },
  mounted() {
    //localStorage.setItem('adminRights', 'false');
    /*let user = localStorage.getItem("user")
    if(user !== null) {
      location.reload();
      localStorage.removeItem("user");
      localStorage.removeItem("id");
      localStorage.removeItem('userIsExpert')

    }*/
  },

};
</script>