using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Models
{
    public class CreatePassword
    {
        public int CreatePasswordID { get; set; }
        public int UserID { get; set; }
        public string Link { get; set; }
        public bool IsUsed { get; set; }

    }
}
