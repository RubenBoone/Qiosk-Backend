using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QioskAPI.Interfaces;
using QioskAPI.Models;

namespace QioskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KiosksController : ControllerBase
    {
        private IKioskService _kioskService;

        public KiosksController(IKioskService kioskService)
        {
            _kioskService = kioskService;
        }

        // GET: api/Kiosks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kiosk>>> GetKiosks()
        {
            var response = await _kioskService.GetKiosks();
            if (response == null)
                return BadRequest(new { message = "something went wrong in KioskService" });

            return Ok(response);
        }

        // GET: api/Kiosks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kiosk>> GetKiosk(int id)
        {
            var response = await _kioskService.GetKiosk(id);
            if (response == null)
                return NotFound();

            return Ok(response);
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

            try
            {
                await _kioskService.PutKiosk(id, kiosk);
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
            await _kioskService.PostKiosk(kiosk);
            return CreatedAtAction("GetKiosk", new { id = kiosk.KioskID }, kiosk);
        }

        // DELETE: api/Kiosks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKiosk(int id)
        {
            var kiosk = await _kioskService.GetKiosk(id);
            if (kiosk == null)
            {
                return NotFound();
            }


            await _kioskService.DeleteKiosk(id);
            return NoContent();
        }

        private bool KioskExists(int id)
        {
            return _kioskService.KioskExists(id);
        }
    }
}
