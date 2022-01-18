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
    public class CreatePasswordsController : ControllerBase
    {
        private readonly QioskContext _context;

        public CreatePasswordsController(QioskContext context)
        {
            _context = context;
        }

        // GET: api/CreatePasswords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreatePassword>>> GetCreatePasswords()
        {
            return await _context.CreatePasswords.ToListAsync();
        }

        // GET: api/CreatePasswords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CreatePassword>> GetCreatePassword(int id)
        {
            var createPassword = await _context.CreatePasswords.FindAsync(id);

            if (createPassword == null)
            {
                return NotFound();
            }

            return createPassword;
        }

        // PUT: api/CreatePasswords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreatePassword(int id, CreatePassword createPassword)
        {
            if (id != createPassword.CreatePasswordID)
            {
                return BadRequest();
            }

            _context.Entry(createPassword).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreatePasswordExists(id))
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

        // POST: api/CreatePasswords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreatePassword>> PostCreatePassword(CreatePassword createPassword)
        {
            _context.CreatePasswords.Add(createPassword);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCreatePassword", new { id = createPassword.CreatePasswordID }, createPassword);
        }

        // DELETE: api/CreatePasswords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreatePassword(int id)
        {
            var createPassword = await _context.CreatePasswords.FindAsync(id);
            if (createPassword == null)
            {
                return NotFound();
            }

            _context.CreatePasswords.Remove(createPassword);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CreatePasswordExists(int id)
        {
            return _context.CreatePasswords.Any(e => e.CreatePasswordID == id);
        }
    }
}
