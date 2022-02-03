using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Models
{
    public class Tag
    {
        public int TagID { get; set; }
        public string Code { get; set; }
        public ICollection<UserTag> UserTags { get; set; }
    }
}
