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
    public class BookingService:IBookingService
    {
        private readonly QioskContext _context;
        public BookingService(QioskContext qioskContext)
        {
            _context = qioskContext;
        }
        public bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingID == id);
        }

        public async Task DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookings()
        {
            return await _context.Bookings.Include(b=>b.company).Include(b=>b.userBookings).ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetBookingsDash()
        {
            return await _context.Bookings.OrderByDescending(b=>b.BookingTime).Include(b=>b.company).Include(b=>b.userBookings).Take(10).ToListAsync();
        }

        public async Task<Booking> GetBooking(int id)
        {
            return await _context.Bookings.Include(b=>b.company).FirstOrDefaultAsync(b=>b.BookingID == id);
        }

        public async Task PostBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task PutBooking(int id, Booking booking)
        {
            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
