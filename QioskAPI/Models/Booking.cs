﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public DateTime ArrivalTime  { get; set; }
        public DateTime DepatureTime { get; set; }
        public int NumberOfVisitors { get; set; }
    }
}