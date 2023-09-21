<template>
    <!-- table voor shopping cart aanmaken -->
    <div class="main">
        <div class="ProcessContainer" ref="processRef" >
            <div class="Process">
                <div class="ProcessInner">
                    <img  alt="expertRent logo" src="src/assets/expertrent.png" class="logoHeader LogoDisplay" ref="expertRentLogo">
                    <p>{{message}}</p>
                    <button class="ProcessButton" @click="ProcessCloseX">{{answer}}</button>
                </div>
            </div>
        </div>



        <div class="shoppingCart" onload="changeValues()">
            <div class="cart">
                <h2 class="margin">Shopping Cart</h2>
                <table>
                    <thead>
                    <tr>
                        <th>Expert</th>
                        <th>Date</th>
                        <th>Time</th>
                        <th>Until</th>
                        <th>Price</th>
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
                    </tr>
                    </tbody>
                </table>
                <!-- totaal invullen in een div-->
                <div class="total">
                    Subtotal ({{ numberOfItems }} Items): €{{ total }}
                </div>

            </div>
            <div class="cart">
                <h2 class="margin">Payment Method</h2>
                <table class="paymentTable">
                    <thead>
                    <tr>
                        <td>Your method</td>
                        <td>Name on method</td>
                    </tr>
                    </thead>
                    <tbody>
                    <tr class="payments" :class="{ selected: selectedPaymentIndex === index }" v-for="(pMethod, index) in pMethods" :key="index">
                        <td>
                            <input type="radio" class="marginRight" @click="ClickPayment(index)" :checked="selectedPaymentIndex === index" ref="radioButton"/>
                            <img :src="'src/assets/Payment/' +  pMethod  + '.png'" class="paymentImage" :alt="pMethod"/>
                            <p>{{ pMethod }}</p>
                        </td>
                        <td> {{pName[index]}}</td>
                    </tr>
                    <tr><button class="payButton" @click="addPaymentMethod" ref="addPaymentRef">Add Payment method</button></tr>
                    </tbody>
                </table>

                <div class="addPayment">
                    <p>Method:</p>
                    <select ref="pMethods2">
                        <option value="bankContact">bankContact</option>
                        <option value="paypal">paypal</option>
                        <option value="sepa">sepa</option>
                    </select>
                    <p>Name on card:</p>
                    <input v-model="paymentName" ref="inputName" />
                    <p>Card number:</p>
                    <input v-model="paymentNum" ref="inputNumber" />
                    <button class="payButton marginButton" @click="addPayment">Submit</button>
                </div>

                <h2 class="margin">Add To Your Calendar</h2>
                <table class="paymentTable">
                    <thead>
                    <tr >
                        <th>Expert</th>
                        <th><p>Google Cal</p></th>
                        <th><p>iCal</p></th>
                        <th><p>Office</p></th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr v-for="(booking, index) in booked" :key="index">
                        <td class="expertNameTable">{{ booking[0] }}</td>
                        <td><img src="src/assets/Payment/googleCalendar.png" class="paymentImage" alt="googleCal" @click="addGoogle(booking)" /></td>
                        <td><img src="src/assets/Payment/ical.png" class="paymentImage" alt="iCall" @click="addApple(booking)" /></td>
                        <td><img src="src/assets/Payment/officecal.png" class="paymentImage" alt="officeCal" @click="addMicrosoft(booking)" /></td>
                    </tr>
                    </tbody>
                </table>

            </div>

            <div class="cart2">
                Subtotal ( {{numberOfItems}} Items):  €{{ total }}
                <!--nog geen code voor de checkout-->
                <button @click="processOrder" >Checkout</button>
                <div class="row">
                    <input type="checkbox" ref="savePaymentMethod" checked>
                    <label for="savePaymentMethod">Save Payment method</label>
                </div>

                <!--<button @click="getUser">Tester</button>-->
            </div>
        </div>
    </div>

</template>

<script>
import {useBookingStore} from "@/stores/bookingStore";
import {useExpertStore} from "@/stores/expertStore";
import router from "@/router";
//import router from "@/router";

export default {
    name: 'CartPagePart2',
    data() {
        return {
            bookingStore: useBookingStore(),
            booked: [],
            selectedPaymentIndex: null,
            bookings: [], //van store
            pMethods: [],
            pName: [],
            pNum: [],
            pFull: [],
            clicked: true,
            message: "",
            answer: "",
            complete: false,
            paymentName: "",
            paymentNum: "",
            userBooking: {
                idbuyer: "",
                idseller: "",
                date: "",
                starttime: "",
                endtime: "",
                paymentNum: "",
                amount: "",
                paymentMethod: "",
                paymentName: ""
            },
            availabledays: {
              monday: false,
              tuesday: false,
              wednesday: false,
              thursday: false,
              friday: false,
              saturday: false,
              sunday: false,
          }
            //pMethods: ['Paypal', 'BankContact', 'sepa'],
            //pMethods: [this.bookings[localStorage.getItem('id')][8]],

        };
    },
  getAvailabledays() {
    fetch('APIURL here')
        .then(res => res.json())
        .then(data => {
          this.availabledays = data[0];
        });
  },



    created() {
      const expertStore = useExpertStore();
      const experts = expertStore.experts;
      //const users = expertStore.users;
      const userId = this.$route.params.id;
      this.expert = experts.find((expert) => expert.id === parseInt(userId));
      this.user = expertStore.users.find((u) => u.id === parseInt(userId));
    },

    mounted() {
        this.booked = JSON.parse(localStorage.getItem('booked')) || [];
        //console.log('CartPage log', this.booked);
        useBookingStore().assignAllBookings();
        //useBookingStore().assignBooking();

        //this.pMethods = [useBookingStore().bookings[0].paymentMethod];
        //this.pName = [useBookingStore().bookings[0].idSeller];
        //console.log('use booking store', useBookingStore().bookings[0]);
        //console.log('use booking store further', useBookingStore().bookings[0].paymentMethod)
        //console.log('use booking store further', useBookingStore().bookings[0])

    },

    watch: {
        'bookingStore.bookings'(newBookings) {
            const currentUserId = parseInt(localStorage.getItem('id'));
            const filteredBookings = newBookings.filter(booking => booking.idbuyer === currentUserId);

            this.pMethods = [...new Set(filteredBookings
                .filter(booking => booking.paymentMethod !== '') // Filter out empty strings
                .map(booking => booking.paymentMethod))];

            this.pName = [...new Set(filteredBookings
                .filter(booking => booking.paymentName !== '')
                .map(booking => booking.paymentName))];

            this.pNum = [...new Set(filteredBookings
                .filter(booking => booking.paymentNum !== 0)
                .map(booking => booking.paymentNum))];

            this.pFull = [...new Set(filteredBookings.map(booking => booking.paymentName + booking.paymentMethod))];
        },
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
    },

    methods: {
        ProcessOpen() {
            this.$refs.processRef.style.display="flex"
        },

        ProcessCloseX() {
            if (this.complete === true){
                this.$refs.processRef.style.display="none"
                this.SendMail()
                router.push({path:'/'})
            }
            else {
                this.$refs.processRef.style.display="none"
            }
        },

        SendMail() {

        },



        processOrder() {
            if (this.selectedPaymentIndex === null || this.$refs.radioButton === null){
                this.message = "Please selected a payment method";
                this.answer = "Right!";
                this.ProcessOpen()

            }
            else {
                for(let i = 0; i < this.booked.length; i++){
                    let paymentMethod = this.pMethods[this.selectedPaymentIndex];
                    let paymentName = this.pName[this.selectedPaymentIndex];
                    let paymentNum = this.pNum[this.selectedPaymentIndex];

                    let seller = useExpertStore().users.find((expert) => expert.username === this.booked[i][0]); // find expert by username
                    let idseller = seller ? seller.id : null;

                    let date = this.booked[i][1].split('T')[0];
                    let starttime = this.booked[i][1].split('T')[1].slice(0, -3) + ':00';
                    let endtime = this.booked[i][1].split('T')[2];
                    let amount = this.updatePrice(this.booked[i]);
                    let idbuyer = localStorage.getItem('id');


                    //check if bankdata verzonden moet worden
                    if (this.$refs.savePaymentMethod && this.$refs.savePaymentMethod.checked) {
                        this.userBooking.idbuyer = idbuyer
                        this.userBooking.idseller = idseller
                        this.userBooking.date = date
                        this.userBooking.starttime = starttime
                        this.userBooking.endtime = endtime
                        this.userBooking.paymentNum = paymentNum
                        this.userBooking.amount = amount
                        this.userBooking.paymentMethod = paymentMethod
                        this.userBooking.paymentName = paymentName

                        fetch("https://localhost:7155/api/Booking/addBooking", {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json'
                            },
                            body: JSON.stringify(this.userBooking)
                        })
                    } else if (this.$refs.savePaymentMethod && this.$refs.savePaymentMethod.checked && this.pMethods.contains(this.userBooking.paymentMethod) && this.pName.contains(this.userBooking.paymentName)){
                        this.userBooking.idbuyer = idbuyer
                        this.userBooking.idseller = idseller
                        this.userBooking.date = date
                        this.userBooking.starttime = starttime
                        this.userBooking.endtime = endtime
                        this.userBooking.paymentNum = 0
                        this.userBooking.amount = amount
                        this.userBooking.paymentMethod = ""
                        this.userBooking.paymentName = ""
                    } else {
                        this.userBooking.idbuyer = idbuyer
                        this.userBooking.idseller = idseller
                        this.userBooking.date = date
                        this.userBooking.starttime = starttime
                        this.userBooking.endtime = endtime
                        this.userBooking.paymentNum = 0
                        this.userBooking.amount = amount
                        this.userBooking.paymentMethod = ""
                        this.userBooking.paymentName = ""

                        fetch("https://localhost:7155/api/Booking/addBooking", {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json'
                            },
                            body: JSON.stringify(this.userBooking)
                        })
                    }
                }

                this.complete = true;
                this.message = "Order Processed!";
                this.answer = "Thanks!";
                this.ProcessOpen()
                localStorage.removeItem('booked');
            }
        },

        addPayment(){
            if (this.$refs.inputName.value === ""){
                this.message = "please insert a name on the payment method";
                this.answer = "Oops!";
                this.ProcessOpen()
            }
            else if (this.$refs.inputNumber.value === ""){
                this.message = "please insert a card number";
                this.answer = "Okay!";
                this.ProcessOpen()
            }
            else {
                this.pMethods.push(this.$refs.pMethods2.value);
                //console.log('gaga',this.$refs.pMethods2)
                this.pName.push(this.paymentName);
                this.pNum.push(this.paymentNum);
                this.paymentName = '';
                this.paymentNum = '';
            }


        },

        addPaymentMethod() {
            const button = this.$refs.addPaymentRef;
            const addPayment = document.querySelector('.addPayment');
            if (this.clicked === true){
                button.textContent = "Cancel";
                addPayment.style.display = 'flex';
                this.clicked = false;
            }
            else {
                button.textContent = "Add Payment method";
                addPayment.style.display = 'none';
                this.clicked = true;
            }


        },

        getUser() {
            //const userBookingStore = useBookingStore();
            //const expertStore = useExpertStore();
            //const booking = userBookingStore.bookings[0];
            //const buyerId = booking.idbuyer; //moet kleine letter
            //const buyer = expertStore.users.find(user => user.id === buyerId)
            //console.log('buyer', buyer);
            //console.log('Buyer:', buyer.username);
            //const paymentName = booking.paymentName;
            //console.log('paymentname', paymentName);
            //activeId = useExpertStore().users[0].id;
            //console.log('localid',localId);
            //console.log('activeuser', activeId);
        },

        ClickPayment(index) {
            this.selectedPaymentIndex = index;
        },

        updatePrice(booking) {
            let currentPrice = booking[2];
            let startValue = booking[1].split('T')[2].split(':')[0];
            let secondValue = booking[1].split('T')[1].split(':')[0];

            let duration = startValue - secondValue;

            return duration * currentPrice;
        },

        addGoogle(booking) {
            /*// Retrieve the necessary details from the booking object
            const expert = booking[0];
            const date = booking[1].split('T')[0];
            const startTime = booking[1].split('T')[1].slice(0, -3) + ':00';
            const endTime = booking[1].split('T')[2];

            // Construct the event data to be added to the Google Calendar
            const eventData = {
                summary: `Appointment with ${expert}`,
                start: {
                    dateTime: `${date}T${startTime}`,
                },
                end: {
                    dateTime: `${date}T${endTime}`,
                },
            };

            // Use the Google Calendar API or a library to add the event to the calendar
            // Example: window.gapi.client.calendar.events.insert({ calendarId: 'primary', resource: eventData });

            // Handle the success or error response from the calendar API/library*/
        },

        addApple() {

        },

        addMicrosoft() {

        },
    }
}
</script>

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
    padding: 50px 0;
}

.shoppingCart {
    display: flex;
    flex-direction: column;
    height: 100%;
    width: 100%;
    margin: 0 20px;
}

.payButton {
    background-color: #2A93BC;
    display: flex;
    justify-self: center;
    align-self: center;
    justify-content: center;
    font-size: 15px;
    color: white;
    padding: 0 10px;
}

.marginButton {
    margin: 20px 0 40px 0;
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

.row {
    display: flex;
    flex-direction: row;
}

.marginRight {
    margin-right: 10px
}

.paymentImage {
    height: 30px;
    margin-right: 20px;
}

.paymentTable td:first-child {
    text-align: left;
    display: flex;
}

.paymentTable td:not(:first-child) {
    text-align: center;
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
    align-self: center;
}

.cart th {
    padding: 5px;
    text-align: left;
    font-size: smaller;
    background: #F0F0F0;
}

.cart td {
    padding: 5px;
    font-size: small;
}

.margin {
    margin-bottom: 5px;
}

.paymentTable {
    margin-bottom: 50px;
}

.addPayment {
    display: none;
    flex-direction: column;
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


.tableClass th:last-child{
    width: 20px;
}

.tableClass td:last-child {
    display: flex;
    justify-content: center;
}

.payments.selected {
    background-color: #efedaf;
    transition: background-color 0.5s;
}

.expertNameTable {
    width: 60%;
}

.ProcessContainer {
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
.Process {
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

.ProcessInner {
    display: flex;
    justify-content: center;
    flex-direction: column;
    margin-bottom: 10%;
    text-align: center;
}

.ProcessButton {
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
