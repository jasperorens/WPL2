import { defineStore } from 'pinia';

export const useBookingStore = defineStore('payment', {
    state: () => ({
        bookings: [],
        bookedBy: [],
        //loading: false,
        //error: null,
    }),

    actions: {
        assignAllBookings() {
            try {
                fetch("https://localhost:7155/api/Booking/getAllBookings", {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                    },
                }).then(response =>{
                    return response.json()
                }).then(data=>{
                    this.bookings = JSON.parse(data)
                })


            } catch (error) {
                console.error(error)
            }
        },


        assignBooking() {
            try {
                fetch("https://localhost:7155/api/Booking/getBooking", {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                    },
                }).then(response =>{
                    this.bookings.response.json()
                    //return response.json() //niet return
                }).then(data=>{
                    this.bookedBy = JSON.parse(data)
                })


            } catch (error) {
                console.error(error)
            }
        },

        getters: {
            getBookings() {
                return this.bookings
            },
        }

    },
});
