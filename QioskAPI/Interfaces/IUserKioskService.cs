using QioskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Interfaces
{
    public interface IUserKioskService
    {
        Task<IEnumerable<UserKiosk>> GetUserKiosks();
        Task<UserKiosk> GetUserKiosk(int id);
        Task PutUserKiosk(int id, UserKiosk UserKiosk);
        Task PostUserKiosk(UserKiosk UserKiosk);
        Task DeleteUserKiosk(int id);
        bool UserKioskExists(int id);
    }
}
