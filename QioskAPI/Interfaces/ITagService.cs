using QioskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetTags();
        Task<Tag> GetTag(int id);
        Task PutTag(int id, Tag Tag);
        Task PostTag(Tag Tag);
        Task DeleteTag(int id);
        bool TagExists(int id);
    }
}
