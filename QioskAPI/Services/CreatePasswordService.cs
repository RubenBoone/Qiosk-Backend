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
    public class CreatePasswordService :ICreatePasswordService
    {
        private readonly QioskContext _context;
        public CreatePasswordService(QioskContext qioskContext)
        {
            _context = qioskContext;
        }
        public bool CreatePasswordExists(int id)
        {
            return _context.CreatePasswords.Any(e => e.CreatePasswordID == id);
        }

        public async Task DeleteCreatePassword(int id)
        {
            var createPassword = await _context.CreatePasswords.FindAsync(id);
            _context.CreatePasswords.Remove(createPassword);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CreatePassword>> GetCreatePasswords()
        {
            return await _context.CreatePasswords.ToListAsync();
        }

        public async Task<CreatePassword> GetCreatePassword(int id)
        {
            return await _context.CreatePasswords.FindAsync(id);
        }

        public async Task PostCreatePassword(CreatePassword createPassword)
        {
            _context.CreatePasswords.Add(createPassword);
            await _context.SaveChangesAsync();
        }

        public async Task PutCreatePassword(int id, CreatePassword createPassword)
        {
            _context.Entry(createPassword).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
