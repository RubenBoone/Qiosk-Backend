using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { set {} get { return "**********"; } }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
