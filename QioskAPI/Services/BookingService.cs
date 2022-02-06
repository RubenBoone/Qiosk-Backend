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
            return await _context.Bookings.Include(c=>c.Company).Include(u=>u.UserBookings).ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetBookingsDash()
        {
            return await _context.Bookings.Where(b=>b.BookingTime.Date>=DateTime.Now.Date).OrderBy(b=>b.BookingTime).Include(b=>b.Company).Include(b=>b.UserBookings).Take(4).ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetUsersByBookingId(int id)
        {
            return await _context.Bookings.Where(b => b.BookingID == id).Include(ub => ub.UserBookings).ThenInclude(u => u.User).ToListAsync();
        }

        public async Task<Booking> GetBooking(int id)
        {
            return await _context.Bookings.Include(b=>b.Company).FirstOrDefaultAsync(b=>b.BookingID == id);
        }
        public async Task<IEnumerable<int>> GetTimeSlots(string d)
        {

            var dX = DateTime.Parse(d);
            var slots = new List<int>();
            var bookings = await _context.Bookings.Where(b => b.BookingTime.Date== dX.Date).ToListAsync();
       

            switch (bookings.Count)
            {
                case 0:
                    slots.Add(1);
                    slots.Add(9);
                    return slots;
                case 1:
                    if (bookings[0].BookingTime.Hour>=7 && bookings[0].BookingTime.Hour <= 10) slots.Add(1);
                    else slots.Add(9);
                    return slots;
                default:
                    return slots;
            }
        }

        public async Task PostBooking(Booking booking)
        {
            var c = await _context.Companies.FindAsync(booking.CompanyID);
            if (c != null)
            { booking.Company = c; }
            if (booking.CompanyID == 0)
            {
                var company = await _context.Companies.FirstOrDefaultAsync(u => booking.Company.Name == u.Name);
                if (company != null)
                {
                    booking.CompanyID = company.CompanyID;
                    booking.Company = company;
                }
            }
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
