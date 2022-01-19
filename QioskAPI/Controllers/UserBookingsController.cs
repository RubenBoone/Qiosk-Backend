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


namespace QioskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBookingsController : ControllerBase
    {
        private  IUserBookingService _userBookingService;

        public UserBookingsController(IUserBookingService userBookingService)
        {
            _userBookingService = userBookingService;
        }

        // GET: api/UserBookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBooking>>> GetUserBookings()
        {
            var response = await _userBookingService.GetUserBookings();
            if (response == null)
                return BadRequest(new { message = "something went wrong in UBService" });

            return Ok(response);
        }

        // GET: api/UserBookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserBooking>> GetUserBooking(int id)
        {
            var response = await _userBookingService.GetUserBooking(id);
            if (response == null)
                return NotFound();

            return Ok(response);
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

            try
            {
                await _userBookingService.PutUserBooking(id, userBooking);
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
            await _userBookingService.PostUserBooking(userBooking);
            return CreatedAtAction("GetUserBooking", new { id = userBooking.UserBookingID }, userBooking);
        }

        // DELETE: api/UserBookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserBooking(int id)
        {
            var userBooking = await _userBookingService.GetUserBooking(id);
            if (userBooking == null)
            {
                return NotFound();
            }

            await _userBookingService.DeleteUserBooking(id);
            return NoContent();
        }

        private bool UserBookingExists(int id)
        {
            return _userBookingService.UserBookingExists(id);
        }
    }
}
