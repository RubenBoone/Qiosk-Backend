using QioskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetBookings();
        Task<Booking> GetBooking(int id);
        Task PutBooking(int id, Booking Booking);
        Task<IEnumerable<Booking>> GetUsersByBookingId(int id);
        Task PostBooking(Booking Booking);
        Task DeleteBooking(int id);
        bool BookingExists(int id);
    }
}
