using QioskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string email, string password);


    }
}
