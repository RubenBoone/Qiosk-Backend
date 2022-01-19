
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
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task PutUser(int id, User user);
        Task PostUser(User user);
        Task DeleteUser(int id);
        bool UserExists(int id);
    }
}
