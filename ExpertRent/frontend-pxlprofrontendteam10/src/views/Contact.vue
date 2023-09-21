<template xmlns="http://www.w3.org/1999/html">
  <div class="contact-main">
    <div class="heightClass">
      <div class="div-contact">
        <form id="contact-form">
          <label for="name">Name:</label>
          <input type="text" id="name" name="name" required>

          <label for="subject">Subject</label>
          <input type="text" id="subject" name="subject" required>

          <label for="email">Email:</label>
          <input type="email" id="email" name="email" required>

          <label for="message">Message:</label>
          <textarea id="message" name="message" required></textarea>

          <button class="button" type="submit" @click.prevent="sendEmail">Send</button>
        </form>
      </div>

    </div>

    <div>
    </div>

  </div>

  <!-- <img src="src/assets/backgroundcontact.jpg"
  alt="background"> -->

</template>

<script>
export default {
  methods: {
    sendEmail() {
      var name = document.getElementById('name').value;
      var email = document.getElementById('email').value;
      var message = document.getElementById('message').value;

      var subject = 'New message from ' + name;
      var body = 'From: ' + email + '\n\n' + message;

      var mailtoLink = 'mailto:sennegeurts2@gmail.com?subject=' + encodeURIComponent(subject) + '&body=' + encodeURIComponent(body);

      window.open(mailtoLink);
    }
  }
}
</script>

<style scoped>
.heightClass {
  padding: 50px;
}
.button{
  background-color: #2A93BC;
  border-radius: 15px;
}
.foto{
  display: none;
}
label
{
  display: inline-block;
  margin-bottom: 0.5rem;
}
input[type="text"],input[type="email"],textarea
{ display: block;
  width: 90%;
  padding: 0.375rem 0.75rem;
  font-size: 1rem;
  line-height: 1.5;
  color: #495057;
  background-color: #fff;
  border: 1px solid #ced4da;
  border-radius: 0.25rem;
  transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
}
button[type="submit"]
{
  display: block;
  margin-top: 1rem;
}
@media screen and (min-width: 500px)
{
  .contact-main
  {
    display: flex;
    flex-direction: row;
    align-content: center;
    justify-content: center;
    width: 100%;
    background-image: url("../../src/assets/backgroundcontact2.png");
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
  }
  .contact-main > div
  { width: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
  }
  .foto{
    display: flex;
    height: 80%;
  }
  .div-contact
  { width: 50%;
    height: auto;
    display: flex;
    align-items: center;
    justify-content: center;
    background-color: whitesmoke;
    padding: 30px;
    border-radius: 15px;
  }
  form{
    display: flex;
    flex-direction: column;
    width: 100%;
    height: 100%;
    margin: 0;
    /*box-shadow: 5px -5px 20px black;*/
  }
}</style>


<!--<template>
  <div class="about">
    <h1>Dit is de pagina om contact met ons op te nemen</h1>
  </div>
</template>

<style>
@media (min-width: 1024px) {
  .about {
    min-height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
    width: 100%;
  }
}
</style>-->
