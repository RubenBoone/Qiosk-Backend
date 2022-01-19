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
    public class UserBookingService:IUserBookingService
    {
        private readonly QioskContext _context;
        public UserBookingService(QioskContext qioskContext)
        {
            _context = qioskContext;
        }
        public bool UserBookingExists(int id)
        {
            return _context.UserBookings.Any(e => e.UserBookingID == id);
        }

        public async Task DeleteUserBooking(int id)
        {
            var userBooking = await _context.UserBookings.FindAsync(id);
            _context.UserBookings.Remove(userBooking);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserBooking>> GetUserBookings()
        {
            return await _context.UserBookings.ToListAsync();
        }

        public async Task<UserBooking> GetUserBooking(int id)
        {
            return await _context.UserBookings.FindAsync(id);
        }

        public async Task PostUserBooking(UserBooking userBooking)
        {
            _context.UserBookings.Add(userBooking);
            await _context.SaveChangesAsync();
        }

        public async Task PutUserBooking(int id, UserBooking userBooking)
        {
            _context.Entry(userBooking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
