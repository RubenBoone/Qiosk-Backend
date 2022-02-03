using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Models
{
    public class UserBooking
    {
        public int UserBookingID { get; set; }
        public int UserID { get; set; }
        public int BookingID { get; set; }
        public User User { get; set; }
        public Booking Booking { get; set; }
    }
}
