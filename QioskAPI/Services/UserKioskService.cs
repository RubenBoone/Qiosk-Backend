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
    public class UserKioskService: IUserKioskService
    {
        private readonly QioskContext _context;
        public UserKioskService(QioskContext qioskContext)
        {
            _context = qioskContext;
        }
        public bool UserKioskExists(int id)
        {
            return _context.UserKiosks.Any(e => e.UserKioskID == id);
        }

        public async Task DeleteUserKiosk(int id)
        {
            var userKiosk = await _context.UserKiosks.FindAsync(id);
            _context.UserKiosks.Remove(userKiosk);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserKiosk>> GetUserKiosks()
        {
            return await _context.UserKiosks.ToListAsync();
        }

        public async Task<UserKiosk> GetUserKiosk(int id)
        {
            return await _context.UserKiosks.FindAsync(id);
        }

        public async Task<IEnumerable<UserKiosk>> GetSpecificUserKiosks(int userId)
        {
            return await _context.UserKiosks.Where(u => u.UserID == userId).ToListAsync();
        }

        public async Task PostUserKiosk(UserKiosk userKiosk)
        {
            _context.UserKiosks.Add(userKiosk);
            await _context.SaveChangesAsync();
        }

        public async Task PutUserKiosk(int id, UserKiosk userKiosk)
        {
            _context.Entry(userKiosk).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
