using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QioskAPI.Data;
using QioskAPI.Models;
using QioskAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace QioskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: api/Bookings
        //[Authorize]//ad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            IEnumerable<Booking> response;
           // var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            //if (isAdmin)
            //{
                response = await _bookingService.GetBookings();
           // }
            //else
            //{
              //  return Unauthorized();
            //}
            if (response == null)
                return BadRequest(new { message = "something went wrong in BookingService" });

            return Ok(response);
        }
        // GET: api/Bookings
        [Authorize]//ad
        [HttpGet("bookingsDash")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsDash()
        {
            IEnumerable<Booking> response;
            var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            if (isAdmin)
            {
                response = await _bookingService.GetBookingsDash();
            }
            else
            {
                return Unauthorized();
            }
            if (response == null)
                return BadRequest(new { message = "something went wrong in BookingService" });

            return Ok(response);
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var response = await _bookingService.GetBooking(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetUsersByBookingId(int id)
        {
            IEnumerable<Booking> response;
            var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            if (isAdmin)
            {
                response = await _bookingService.GetUsersByBookingId(id);
            }
            else
            {
                return Unauthorized();
            }
            if (response == null)
                return BadRequest(new { message = "something went wrong in BookingService" });

            return Ok(response);
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]//ad
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.BookingID)
            {
                return BadRequest();
            }

            try
            {
                var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
                if (isAdmin)
                {

                    await _bookingService.PutBooking(id,booking);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            await _bookingService.PostBooking(booking);
            return CreatedAtAction("GetBooking", new { id = booking.BookingID }, booking);
        }

        // DELETE: api/Bookings/5
        [Authorize]//ad
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _bookingService.GetBooking(id);
            if (booking == null)
            {
                return NotFound();
            }
            var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            if (isAdmin)
            {
                await _bookingService.DeleteBooking(id);
            }
            else
            {
                return Unauthorized();
            }
            return NoContent();

        }

        private bool BookingExists(int id)
        {
            return _bookingService.BookingExists(id);
        }
    }
}
