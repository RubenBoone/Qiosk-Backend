using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public DateTime BookingTime  { get; set; }
        public IEnumerable<UserBooking> UserBookings { get; set; }
    }
}
