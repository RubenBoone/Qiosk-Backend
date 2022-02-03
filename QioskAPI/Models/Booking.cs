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
        public int CompanyID { get; set; }
        public Company Company { get; set; }
        public ICollection<UserBooking> UserBookings { get; set; }
        [NotMapped]
        public int numberOfVisitors { 
            get { 
                return UserBookings!=null?UserBookings.Count():0; 
            }
        }
    }
}
