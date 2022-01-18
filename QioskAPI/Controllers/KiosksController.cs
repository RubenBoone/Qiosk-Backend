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
    public class KiosksController : ControllerBase
    {
        private readonly QioskContext _context;

        public KiosksController(QioskContext context)
        {
            _context = context;
        }

        // GET: api/Kiosks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kiosk>>> GetKiosks()
        {
            return await _context.Kiosks.ToListAsync();
        }

        // GET: api/Kiosks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kiosk>> GetKiosk(int id)
        {
            var kiosk = await _context.Kiosks.FindAsync(id);

            if (kiosk == null)
            {
                return NotFound();
            }

            return kiosk;
        }

        // PUT: api/Kiosks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKiosk(int id, Kiosk kiosk)
        {
            if (id != kiosk.KioskID)
            {
                return BadRequest();
            }

            _context.Entry(kiosk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KioskExists(id))
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

        // POST: api/Kiosks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Kiosk>> PostKiosk(Kiosk kiosk)
        {
            _context.Kiosks.Add(kiosk);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKiosk", new { id = kiosk.KioskID }, kiosk);
        }

        // DELETE: api/Kiosks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKiosk(int id)
        {
            var kiosk = await _context.Kiosks.FindAsync(id);
            if (kiosk == null)
            {
                return NotFound();
            }

            _context.Kiosks.Remove(kiosk);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KioskExists(int id)
        {
            return _context.Kiosks.Any(e => e.KioskID == id);
        }
    }
}
