<template>
    <div class="containerCalendar">
        <div class="form">
            <label for="meetingDate">Meeting Date:</label>
            <input type="date" id="meetingDate" :min="getMinimumDate()" :disabled="isDayDisabled(selectedDay)" v-model="selectedDate" />

            <label v-if="selectedDate" for="meetingTime">Meeting Time:</label>
            <div v-if="selectedDate" class="timeBox">
                <p class="noMargin padding">From: </p>
                <select ref="meetingStart" id="meetingTime" v-model="selectedTime" @click.prevent="displayCorrectHours" class="padding selectTime">
                    <option v-for="time in bookedHours" :value="time">{{ time }}</option>
                </select>
                <p class="noMargin padding">Until: </p>
                <select id="meetingTime"  v-model="selectedTimeUntil" @click.prevent="removeSelectedHours" class="padding selectTime">
                    <option v-for="time in bookedHoursAfter" :value="time">{{ time }}</option>
                </select>
            </div>

          <!--<label for="manualTime">Manual Time Input:</label>
          <input
              type="text"
              id="manualTime"
              v-model="manualTime"
              @input="updateAvailableTimeSlots"
              placeholder="Enter a time to remove"
          />-->

            <button class="schedule" @click="scheduleMeeting">Schedule</button>
        </div>
    </div>
</template>

<script>
export default {
    name: "Calendars",
    props: {
        selectedDay: {
            type: String,
            default: "",
        },
        currentExpert: {
            type: Number,
            default: 0,
        },
    },
    data() {
        return {
          bookedDates: [],
          bookedHours: [],
          bookedHoursAfter: [],
          selectedDate: "", // Selected date for the meeting
          nextScreen: false,
          selectedTime: "", // Selected time for the meeting
          selectedTimeUntil: "",
          manualTime: "", // Manually inputted time to be removed
          events: [], // Events for the calendar
          startTimeCheck: [],
          endTimeCheck: [],
          expertBooked: [],
          timeSlots: [],
          bookedHoursAfterSplit: [],


        };
    },

  watch:  {
    selectedTime(value) {
      this.selectedTimeUntil = null;
    },

    selectedDate(newDate) {
      this.selectedTime = null;
      this.selectedTimeUntil = null;
    },
  },
    created() {
        this.getAllBookings();
    },
    computed: {
        idSeller() {
            return this.bookedDates.length > 0 ? this.bookedDates[0].idseller : "";
        },
      availableTimeSlots() {
        const timeSlots = [];
        const bookedTimes = this.getBookedTimesForSelectedExpert();
        for (let i = 0; i < 24; i++) {
          const hour = i.toString().padStart(2, "0");
          const time = `${hour}:00`;
          if (this.bookedHours.includes(time) == true) {
            timeSlots.push(time);
          }
        }
        return timeSlots;
      },
      availableTimeSlotsAfter() {
        const fromTime = this.selectedTime;

        const bookedTimes = this.getBookedTimesForSelectedExpert();
        let isStartFound = false;
        for (let i = 0; i < 24; i++) {
          const hour = i.toString().padStart(2, "0");
          const time = `${hour}:00`;
          if (this.bookedHours.includes(time) == true) {
            this.timeSlots.push(time);
          }
          if (time === fromTime) {
            isStartFound = true;
          }
        }
        return this.timeSlots;
      },
    },

    methods: {

      displayCorrectHours(){
        this.getBookedHours()
        console.log('date',this.selectedDate)
        console.log('expert id',this.currentExpert)
        console.log(this.bookedHours)
        console.log('hoursafter', this.bookedHoursAfter)
      },

      removeSelectedHours(){
        this.bookedHoursAfter = [];
        this.bookedHoursAfterSplit = [];
        let bookedsplit;
        for (let i = 0; i < this.bookedHours.length; i++) {
          bookedsplit = this.bookedHours[i].split(':');
          this.bookedHoursAfterSplit.push(parseInt(bookedsplit[0]));
        }
        let timesplit;
        timesplit = this.selectedTime.split(':')
        let selectedtimesplit = parseInt(timesplit[0])

        for (let i = 0; i < this.bookedHoursAfterSplit.length; i++) {
          if(this.bookedHoursAfterSplit[i] > selectedtimesplit){
            this.bookedHoursAfter.push(this.bookedHoursAfterSplit[i] + ':00')
          }
        }
      },

      getBookedTimesForSelectedExpert() {
            const selectedDate = this.selectedDate;
            const selectedExpert = this.currentExpert;
            const bookedTimes = [];
            for (const booking of this.bookedDates) {
                if (
                    booking.date === selectedDate &&
                    booking.idseller === selectedExpert
                ) {
                    const startTime = booking.starttime.split(":")[0];
                    console.log('jasper booking', booking.starttime.split(':')[0])
                    const endTime = booking.endtime.split(":")[0];
                    for (let i = Number(startTime); i <= Number(endTime); i++) {
                        const hour = i.toString().padStart(2, "0");
                        const time = `${hour}:00`;
                        bookedTimes.push(time);
                    }
                }
            }
            return bookedTimes;
        },

      async getAllBookings() {
        try {
          const response = await fetch("https://localhost:7155/api/Booking/getAllBookings", {
            method: "GET",
            headers: {
              Accept: "application/json",
            },
          });
          const data = await response.json();
          this.bookedDates = JSON.parse(data);
        } catch (error) {
          console.error(error);
        }
      },

      async getBookedHours() {
        try {
          const response = await fetch("https://localhost:7155/api/Booking/GetBookedHours?id=" + this.currentExpert + "&date=" + this.selectedDate, {
            method: "GET",
            headers: {
              Accept: "application/json",
            },
          });
          const data = await response.json();
          console.log('data van hours',data)
          this.bookedHours = (data);
          console.log('plz werk',this.bookedHours)
        } catch (error) {
          console.error(error);
        }
      },

      updateAvailableTimeSlots() {
        // Update available time slots when manual time input changes
        this.$forceUpdate();
      },

      updateWithBookings(){
          console.log('Wat zit in bookeddates?', this.bookedDates);
        for (let i = 0; i < this.bookedDates.length; i++) {
          if (this.bookedDates[i].idseller === this.currentExpert ){
            //datum

            this.expertBooked.push(this.bookedDates[i].idseller)
            this.startTimeCheck.push(this.bookedDates[i].starttime);
            this.endTimeCheck.push(this.bookedDates[i].endtime);
          }
           //replace 0 with where the expert is
        }

        console.log('endtime binnen', this.endTimeCheck)
          console.log('starttime binnen', this.startTimeCheck)
        console.log('expert', this.expertBooked)
          //let endTimeCheck =
          //let CompleTime =
          this.manualTime = this.startTimeCheck;
          this.updateAvailableTimeSlots();
      },

        getBookedTimesForSelectedDate() {
            const selectedDateBookings = this.bookedDates.filter(
                (booking) => booking.date === this.selectedDate && booking.idseller === this.currentExpert
            );
            const bookedTimes = [];
            for (const booking of selectedDateBookings) {
                const startTime = booking.starttime.split(":")[0];
                const endTime = booking.endtime.split(":")[0];
                for (let i = Number(startTime); i <= Number(endTime); i++) {
                    const hour = i.toString().padStart(2, "0");
                    const time = `${hour}:00`;
                    bookedTimes.push(time);
                }
            }
            return bookedTimes;
        },
        getMinimumDate() {
            const currentDate = new Date();
            return currentDate.toISOString().split("T")[0];
        },
        getDayOfWeek(date) {
            const days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
            return days[date.getDay()];
        },
        isDayDisabled(day) {
            return day === this.getDayOfWeek(new Date());
        },
        scheduleMeeting() {
            if (this.selectedDate && this.selectedTime && this.selectedTimeUntil) {
                const selectedDateTime = `${this.selectedDate}T${this.selectedTime}T${this.selectedTimeUntil}`;
                const isSlotBooked = this.events.some((event) => event.dateTime === selectedDateTime);
                if (isSlotBooked) {
                    alert("This time slot is already booked. Please select another time slot.");
                } else {
                    this.selectedDate = "";
                    this.selectedTime = "";
                    this.selectedTimeUntil = "";
                    this.$emit("selectedDateTimeChanged", selectedDateTime);
                    this.$emit("nextScreen", true);
                }
            } else {
                alert("Please select a date and time for the meeting.");
            }
        },
    },
};
</script>

<style scoped>
.containerCalendar {
    padding: 0;
}

.form {
    margin-bottom: 20px;
    display: flex;
    flex-direction: column;
    width: 100%;
}

.label.enabled {
  display: inline-block;
}

.schedule {
    background-color: #053f51;
    color: white;
    border-radius: 15px;
    border: none;
    padding: 8px 0;
}

.form > * {
    margin-bottom: 10px;
}

.timeBox {
    display: flex;
    flex-direction: column;
}

.noMargin {
    margin: 0;
}

.padding {
    padding: 0 15px 0 15px;
}

.selectTime {
    width: 70px;
}

@media screen and (min-width: 389px) {
.timeBox{
  flex-direction: row;
}
}
</style>
