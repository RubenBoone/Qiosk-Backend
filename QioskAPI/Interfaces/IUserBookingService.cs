using QioskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Interfaces
{
    public interface IUserBookingService
    {
        Task<IEnumerable<UserBooking>> GetUserBookings();
        Task<UserBooking> GetUserBooking(int id);
        Task PutUserBooking(int id, UserBooking UserBooking);
        Task PostUserBooking(UserBooking UserBooking);
        Task DeleteUserBooking(int id);
        bool UserBookingExists(int id);
    }
}
