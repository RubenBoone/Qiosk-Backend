using Microsoft.EntityFrameworkCore;
using QioskAPI.Data;
using QioskAPI.Interfaces;
using QioskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Services
{
    public class KioskService : IKioskService
    {
        private readonly QioskContext _context;
        public KioskService(QioskContext qioskContext)
        {
            _context = qioskContext;
        }
        public bool KioskExists(int id)
        {
            return _context.Kiosks.Any(e => e.KioskID == id);
        }

        public async Task DeleteKiosk(int id)
        {
            var kiosk = await _context.Kiosks.FindAsync(id);
            _context.Kiosks.Remove(kiosk);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Kiosk>> GetKiosks()
        {
            return await _context.Kiosks.ToListAsync();
        }
        public async Task<IEnumerable<Kiosk>> GetKiosksDash()
        {
            return await _context.Kiosks.OrderByDescending(k=>k.KioskID).Take(4).ToListAsync();
        }

        public async Task<Kiosk> GetKiosk(int id)
        {
            return await _context.Kiosks.FindAsync(id);
        }

        public async Task PostKiosk(Kiosk kiosk)
        {
            _context.Kiosks.Add(kiosk);
            await _context.SaveChangesAsync();
        }

        public async Task PutKiosk(int id, Kiosk kiosk)
        {
            _context.Entry(kiosk).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
