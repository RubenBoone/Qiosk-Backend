using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QioskAPI.Data;
using QioskAPI.Interfaces;
using QioskAPI.Models;

namespace QioskAPI.Services
{
    public class UserTagService :IUserTagService
    {
        private readonly QioskContext _context;
        public UserTagService(QioskContext qioskContext)
        {
            _context = qioskContext;
        }
        public bool UserTagExists(int id)
        {
            return _context.UserTags.Any(e => e.UserTagID == id);
        }

        public async Task DeleteUserTag(int id)
        {
            var userTag = await _context.UserTags.FindAsync(id);
            _context.UserTags.Remove(userTag);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserTag>> GetUserTags()
        {
            return await _context.UserTags.Include(u => u.User).Include(t => t.Tag).ToListAsync();
        }

        public async Task<UserTag> GetUserTag(int id)
        {
            return await _context.UserTags.FindAsync(id);
        }

        public async Task PostUserTag(UserTag userTag)
        {
            _context.UserTags.Add(userTag);
            await _context.SaveChangesAsync();
        }

        public async Task PutUserTag(int id, UserTag userTag)
        {
            _context.Entry(userTag).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
