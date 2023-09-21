<template>
  <!-- table voor shopping cart aanmaken -->
    <div class="main">
        <div class="hazeContainer" ref="hazeRef" >
            <div class="haze">
                <div class="hazeInner">
                    <img  alt="expertRent logo" src="src/assets/expertrent.png" class="logoHeader LogoDisplay" ref="expertRentLogo">
                    <p>{{hazeText}}</p>
                    <button class="hazeButton" @click="HazeClose">{{hazeText2}}</button>
                </div>
            </div>
        </div>



        <div class="shoppingCart">
            <div class="cart">
                <h2 class="margin">Shopping Cart</h2>
                <p @click="deleteAll()" class="deleteAll">Clear all items</p>
                <table class="tableClass">
                    <thead>
                    <tr>
                        <th>Expert</th>
                        <th>Date</th>
                        <th>Time</th>
                        <th>Until</th>
                        <th>Price</th>
                        <th>Delete</th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr v-for="(booking, index) in booked" :key="index">
                        <td>{{ booking[0] }}</td>
                        <td>{{ booking[1].split('T')[0] }}</td>
                        <td>{{ booking[1].split('T')[1].slice(0, -3) + ':00' }}</td>
                        <td>{{ booking[1].split('T')[2] }}</td>
                        <!-- <td>{{ booking[2] }}</td> -->
                        <td>{{ updatePrice(booking) }}</td>

                        <td><img alt="x" src="src/assets/delete.png" class="deleteButton" @click="deleteThis(booking[0], booking[1], booking[2])" /></td>
                    </tr>
                    </tbody>
                </table>
                <!-- totaal invullen in een div-->
                <div class="total">
                    Subtotal ({{ numberOfItems }} Items): €{{ total }}
                </div>

            </div>
            <div class="cart2">
                Subtotal ( {{numberOfItems}} Items):  €{{ total }}
                <!--nog geen code voor de checkout-->
                <button @click="next" >Next</button>
            </div>
        </div>
    </div>

</template>

<script>

export default {
    name: 'CartPage',
    data() {
        return {
            booked: [],
            hazeText: "",
            hazeText2: "",
            clicked: false,
        }
    },
    methods: {
        HazeClose(){
            this.$refs.hazeRef.style.display ="none";
            if (this.clicked === true){
                this.clicked = false;
                this.OpenLogin();
            }
        },
        HazeOpen(){
          this.$refs.hazeRef.style.display = "flex";
        },
        OpenLogin(){
            this.$router.push({ name: 'Login' });
        },
        next() {
            if (!localStorage.getItem("user"))
            {
                this.hazeText = "Please log in";
                this.hazeText2 = "Ok";
                this.HazeOpen();
                this.clicked = true;
                window.scrollTo(0, 0);
            }
            else
            {
                if(!localStorage.getItem('booked')){
                    this.hazeText = "Nothing in the basket";
                    this.hazeText2 = "Oopsies!";
                    this.HazeOpen();
                    window.scrollTo(0, 0);
                }
                else {
                    this.$router.push({ name: 'CartPagePart2' });
                }
            }
        },

        updatePrice(booking) {
            let currentPrice = booking[2];
            let startValue = booking[1].split('T')[2].split(':')[0];
            let secondValue = booking[1].split('T')[1].split(':')[0];

            let duration = startValue - secondValue;

            return duration * currentPrice;
        },


        deleteAll() {
            localStorage.removeItem('booked');
            this.booked = [];
            const updatedBookedJSON = JSON.stringify(this.booked);
            localStorage.setItem('booked', updatedBookedJSON);
        },

        deleteThis(name, date, time, price) {
            const bookedJSON = localStorage.getItem('booked');
            const booked = JSON.parse(bookedJSON) || [];

            const indexToDelete = booked.findIndex((booking) => {
                return booking[0] === name && booking[1] === date && booking[2] === time && booking[3] === price;
            });

            if (indexToDelete >= 0) {
                booked.splice(indexToDelete, 1);
            }

            const updatedBookedJSON = JSON.stringify(booked);
            localStorage.setItem('booked', updatedBookedJSON);
            this.booked = booked;
        },
    },
    mounted() {
        this.booked = JSON.parse(localStorage.getItem('booked')) || [];
    },

    computed: {
        numberOfItems() {
            return this.booked.length;
        },

        total() {
            let total = 0;
            this.booked.forEach((booking) => {
                total += this.updatePrice(booking);
            });
            return total.toFixed(2);
        },
    },
    filters: {
        currency(value) {
            return '$' + value.toFixed(2);
        }
    }
}
</script>

<!-- css -->
<style scoped>



.main{
    width: 100%;
    display: flex;
    align-content: center;
    justify-content: center;
    background-image: url("../../src/assets/backgroundShopping.png");
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    min-height: 750px;
  padding-bottom: 50px;
}

.shoppingCart {
    display: flex;
    flex-direction: column;
    height: 100%;
    width: 100%;
    margin: 0 20px;
}

.cart {
    margin-top: 5%;
    display: flex;
    flex-direction: column;
    width: 80%;
    border: 1px solid #ccc;
    background-color: #f9f9f9;
    height: auto;
    padding: 40px 40px 40px 40px;
    border-radius: 10px;
}

.cart2 {
    margin-top: 50px;
    display: flex;
    flex-direction: column;
    width: 80%;
    border: 1px solid #ccc;
    padding: 40px 40px 40px 40px;
    background-color: #f9f9f9;
    height: auto;
    border-radius: 10px;
}

.cart h2 {
    font-size: 24px;
    margin-top: 0;
}

.cart table {
    width: 100%;
    border-collapse: collapse;
    background-color: white;
    align-self: center;
}

.cart th {
    background-color: #eee;
    padding: 10px;
    text-align: left;
    border: 1px solid #ccc;
}

.cart td {
    padding: 10px;
    border: 1px solid #ccc;
}

.deleteButton {
    height: 15px;
    width: 15px;
    background-color: white;
    cursor: pointer;
}

.deleteAll {
    margin: 0;
    padding: 0 0 30px 0;
    display: flex;
    width: 100%;
    cursor: pointer;
}

.deleteAll:hover {
    color: orange;
}

.margin {
    margin-bottom: 5px;
}


.cart .total {
    text-align: right;
    font-size: 20px;
    font-weight: bold;
    margin-top: 10px;
}

.cart2 button {
    background-color: #2A93BC;
    color: white;
    border: none;
    padding: 10px 20px;
    text-align: center;
    text-decoration: none;
    font-size: 16px;
    margin-top: 10px;
    cursor: pointer;
}

.cart button:hover {
    background-color: #09637F;
    box-sizing: content-box;
    border-top: #053F51 5px solid;
    border-left: #053F51 5px solid;
    padding: 8px 8px 5px 5px;
    color: #CECECE;
}

.tableClass th:last-child{
    width: 20px;
}

.tableClass td:last-child {
    display: flex;
    justify-content: center;
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

@media screen and (min-width: 880px) {
    .shoppingCart {
        flex-direction: row;
        height: 80%;
    }

    .cart {
        width: 50%;
        height: 100%;
        margin-top: 2%;
        margin-right: 20px;
    }

    .cart2 {
        width: 20%;
        height: 30%;
        margin-top: 5%;
        margin-right: 20px;
    }
}
</style>

