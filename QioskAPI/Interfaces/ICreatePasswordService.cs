using QioskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Interfaces
{
    public interface ICreatePasswordService
    {
        Task<IEnumerable<CreatePassword>> GetCreatePasswords();
        Task<CreatePassword> GetCreatePassword(int id);
        Task PutCreatePassword(int id, CreatePassword CreatePassword);
        Task PostCreatePassword(CreatePassword CreatePassword);
        Task DeleteCreatePassword(int id);
        bool CreatePasswordExists(int id);
    }
}
