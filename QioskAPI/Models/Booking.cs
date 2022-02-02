using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public DateTime BookingTime  { get; set; }
        public IEnumerable<UserBooking> UserBookings { get; set; }
        public int companyID { get; set; }
        public Company company { get; set; }
        public List<UserBooking> userBookings { get; set; }
        [NotMapped]
        public int numberOfVisitors { 
            get { 
                return userBookings!=null?userBookings.Count():0; 
            }
        }
    }
}
