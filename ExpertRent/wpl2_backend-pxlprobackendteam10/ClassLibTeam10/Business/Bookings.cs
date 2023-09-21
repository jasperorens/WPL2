using ClassLibTeam10.Data;
using ClassLibTeam10.Data.Framework;

namespace ClassLibTeam10.Business
{
    public class Bookings
    {
        public static SelectResult GetBookings()
        {
            BookingData bookingData = new BookingData();
            SelectResult result = bookingData.Select();
            return result;
        }
    }
}