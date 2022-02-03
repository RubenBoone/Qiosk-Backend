using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Models
{
    public class Kiosk
    {
        public int KioskID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Coordinate { get; set; }
        public ICollection<UserKiosk> UserKiosks { get; set; }
    }
}
