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
    public class UserKiosksController : ControllerBase
    {
        private readonly QioskContext _context;

        public UserKiosksController(QioskContext context)
        {
            _context = context;
        }

        // GET: api/UserKiosks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserKiosk>>> GetUserKiosks()
        {
            return await _context.UserKiosks.ToListAsync();
        }

        // GET: api/UserKiosks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserKiosk>> GetUserKiosk(int id)
        {
            var userKiosk = await _context.UserKiosks.FindAsync(id);

            if (userKiosk == null)
            {
                return NotFound();
            }

            return userKiosk;
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

            _context.Entry(userKiosk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
            _context.UserKiosks.Add(userKiosk);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserKiosk", new { id = userKiosk.UserKioskID }, userKiosk);
        }

        // DELETE: api/UserKiosks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserKiosk(int id)
        {
            var userKiosk = await _context.UserKiosks.FindAsync(id);
            if (userKiosk == null)
            {
                return NotFound();
            }

            _context.UserKiosks.Remove(userKiosk);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserKioskExists(int id)
        {
            return _context.UserKiosks.Any(e => e.UserKioskID == id);
        }
    }
}
