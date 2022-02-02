using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QioskAPI.Models;
using QioskAPI.Interfaces;


namespace QioskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserKiosksController : ControllerBase
    {
        private IUserKioskService _userKioskService;

        public UserKiosksController(IUserKioskService userKioskService)
        {
            _userKioskService = userKioskService;
        }

        // GET: api/UserKiosks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserKiosk>>> GetUserKiosks()
        {
            var response = await _userKioskService.GetUserKiosks();
            if (response == null)
                return BadRequest(new { message = "something went wrong in UserKioskService" });

            return Ok(response);
        }

        // GET: api/UserKiosks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserKiosk>> GetUserKiosk(int id)
        {
            var response = await _userKioskService.GetUserKiosk(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("specific/{userID}")]
        public async Task<ActionResult<IEnumerable<UserKiosk>>> GetSpecificUserKiosks(int userID)
        {
            var response = await _userKioskService.GetSpecificUserKiosks(userID);
            if (response == null)
                return BadRequest(new { message = "something went wrong in UserKioskService" });

            return Ok(response);
        }

        // PUT: api/UserKiosks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserKiosk(int id, UserKiosk userKiosk)
        {
            if (id != userKiosk.UserKioskID)
            {
                return BadRequest();
            }

            try
            {
                await _userKioskService.PutUserKiosk(id, userKiosk);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserKioskExists(id))
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

        // POST: api/UserKiosks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserKiosk>> PostUserKiosk(UserKiosk userKiosk)
        {
            await _userKioskService.PostUserKiosk(userKiosk);
            return CreatedAtAction("GetUserKiosk", new { id = userKiosk.UserKioskID }, userKiosk);
        }

        // DELETE: api/UserKiosks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserKiosk(int id)
        {
            var userKiosk = await _userKioskService.GetUserKiosk(id);
            if (userKiosk == null)
            {
                return NotFound();
            }


            await _userKioskService.DeleteUserKiosk(id);
            return NoContent();
        }

        private bool UserKioskExists(int id)
        {
            return _userKioskService.UserKioskExists(id);
        }
    }
}
