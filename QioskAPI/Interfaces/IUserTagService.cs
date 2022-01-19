using QioskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Interfaces
{
    public interface IUserTagService
    {
        Task<IEnumerable<UserTag>> GetUserTags();
        Task<UserTag> GetUserTag(int id);
        Task PutUserTag(int id, UserTag UserTag);
        Task PostUserTag(UserTag UserTag);
        Task DeleteUserTag(int id);
        bool UserTagExists(int id);
    }
}
