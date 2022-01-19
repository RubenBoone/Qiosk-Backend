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
    public class CreatePasswordsController : ControllerBase
    {
        private ICreatePasswordService _createPasswordService;

        public CreatePasswordsController(ICreatePasswordService createPasswordService)
        {
            _createPasswordService = createPasswordService;
        }

        // GET: api/CreatePasswords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreatePassword>>> GetCreatePasswords()
        {
            var response = await _createPasswordService.GetCreatePasswords();
            if (response == null)
                return BadRequest(new { message = "something went wrong in CreatePasswordService" });

            return Ok(response);
        }

        // GET: api/CreatePasswords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CreatePassword>> GetCreatePassword(int id)
        {
            var response = await _createPasswordService.GetCreatePassword(id);
            if (response == null)
                return NotFound();

            return Ok(response);
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

            try
            {
                await _createPasswordService.PutCreatePassword(id, createPassword);
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
            await _createPasswordService.PostCreatePassword(createPassword);
            return CreatedAtAction("GetCreatePassword", new { id = createPassword.CreatePasswordID }, createPassword);
        }

        // DELETE: api/CreatePasswords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreatePassword(int id)
        {
            var createPassword = await _createPasswordService.GetCreatePassword(id);
            if (createPassword == null)
            {
                return NotFound();
            }


            await _createPasswordService.DeleteCreatePassword(id);
            return NoContent();
        }

        private bool CreatePasswordExists(int id)
        {
            return _createPasswordService.CreatePasswordExists(id);
        }
    }
}
