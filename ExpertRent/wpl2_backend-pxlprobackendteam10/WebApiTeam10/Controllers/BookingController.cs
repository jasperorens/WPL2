using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json.Nodes;
using ClassLibTeam06.Data;
using ClassLibTeam10.Business;
using ClassLibTeam10.Business.Entities;
using ClassLibTeam10.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApiTeam10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        [HttpPost] // Gebruik post
        [Route("addBooking")] // api/booking/addbooking word opgeroepen
        public void addBooking([FromBody] JsonObject jsonObject) // FromBody word de Json gestoken
        {
            try
            {
                Booking booking = JsonConvert.DeserializeObject<Booking>(jsonObject.ToString()); //het gene wat binnen komt word omgezet naar booking
                BookingData data = new BookingData(); //new instance
                Console.Write(jsonObject.ToString());
                EmailData emailData = new EmailData();
                emailData.SendMail(booking);
                emailData.SendMailSeller(booking);
                data.AddBooking(booking);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet]
        [Route("getBooking")]
        public IActionResult GetBooking(string id)
        {
            BookingData bookingData = new BookingData(); 
            return Ok(bookingData.GetPaymentData(id));
        }

        [HttpGet]
        [Route("getAllBookings")]
        public IActionResult GetAllBookings()
        {
            var bookings = Bookings.GetBookings();
            if (bookings.Succeeded)
            {
                var usersDataTable = bookings.DataTable;
                string userJSON = JsonConvert.SerializeObject(usersDataTable);
                return Ok(userJSON);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("GetBookedDates")]
        public IActionResult GetBookedDates(string id)
        {
            BookingData bookingData = new BookingData();
            return Ok(bookingData.GetBookedDates(id));
        }
        
        [HttpGet]
        [Route("GetBookedHours")]
        public IActionResult GetBookedDates(string id, string date)
        {
            BookingData bookingData = new BookingData();
            return Ok(bookingData.GetBookedHours(id, date));
        }
    }
}
