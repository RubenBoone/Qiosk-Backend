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
        public int companyID { get; set; }
        public Company company { get; set; }
    }
}
