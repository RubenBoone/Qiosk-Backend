
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Models
{
    public class UserKiosk
    {
        public int UserKioskID { get; set; }
        public int UserID { get; set; }
        public int KioskID { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }
}
