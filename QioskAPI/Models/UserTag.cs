using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Models
{
    public class UserTag
    {
        public int UserTagID { get; set; }
        public int UserID { get; set; }
        public int TagID { get; set; }
    }
}
