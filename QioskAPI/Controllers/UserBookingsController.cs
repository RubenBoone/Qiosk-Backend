using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QioskAPI.Data;
using QioskAPI.Models;

namespace QioskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBookingsController : ControllerBase
    {
        private readonly QioskContext _context;

        public UserBookingsController(QioskContext context)
        {
            _context = context;
        }

        // GET: api/UserBookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBooking>>> GetUserBookings()
        {
            return await _context.UserBookings.ToListAsync();
        }

        // GET: api/UserBookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserBooking>> GetUserBooking(int id)
        {
            var userBooking = await _context.UserBookings.FindAsync(id);

            if (userBooking == null)
            {
                return NotFound();
            }

            return userBooking;
        }

        // PUT: api/UserBookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserBooking(int id, UserBooking userBooking)
        {
            if (id != userBooking.UserBookingID)
            {
                return BadRequest();
            }

            _context.Entry(userBooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserBookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserBookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserBooking>> PostUserBooking(UserBooking userBooking)
        {
            _context.UserBookings.Add(userBooking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserBooking", new { id = userBooking.UserBookingID }, userBooking);
        }

        // DELETE: api/UserBookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserBooking(int id)
        {
            var userBooking = await _context.UserBookings.FindAsync(id);
            if (userBooking == null)
            {
                return NotFound();
            }

            _context.UserBookings.Remove(userBooking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserBookingExists(int id)
        {
            return _context.UserBookings.Any(e => e.UserBookingID == id);
        }
    }
}
