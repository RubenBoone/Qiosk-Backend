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
    public class UserBookingsController : ControllerBase
    {
        private  IUserBookingService _userBookingService;

        public UserBookingsController(IUserBookingService userBookingService)
        {
            _userBookingService = userBookingService;
        }

        // GET: api/UserBookings
        [Authorize]//ad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBooking>>> GetUserBookings()
        {
            IEnumerable<UserBooking> response;
            var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            if (isAdmin)
            {

                response= await _userBookingService.GetUserBookings();
            }
            else
            {
                return Unauthorized();
            }
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
        [Authorize]//ad
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserBooking(int id, UserBooking userBooking)
        {
            if (id != userBooking.UserBookingID)
            {
                return BadRequest();
            }

            try
            {
                var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
                if (isAdmin)
                {

                    await _userBookingService.PutUserBooking(id,userBooking);
                }
                else
                {
                    return Unauthorized();
                }
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
        [Authorize]//ad
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserBooking(int id)
        {
            var userBooking = await _userBookingService.GetUserBooking(id);
            if (userBooking == null)
            {
                return NotFound();
            }
            var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            if (isAdmin)
            {

                await _userBookingService.DeleteUserBooking(id);
            }
            else
            {
                return Unauthorized();
            }
            return NoContent();
        }

        private bool UserBookingExists(int id)
        {
            return _userBookingService.UserBookingExists(id);
        }
    }
}
