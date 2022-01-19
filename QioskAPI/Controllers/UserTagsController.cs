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
    public class UserTagsController : ControllerBase
    {
        private IUserTagService _userTagService;

        public UserTagsController(IUserTagService userTagService)
        {
            _userTagService = userTagService;
        }

        // GET: api/UserTags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTag>>> GetUserTags()
        {
            var response = await _userTagService.GetUserTags();
            if (response == null)
                return BadRequest(new { message = "something went wrong in UserTagService" });

            return Ok(response);
        }

        // GET: api/UserTags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTag>> GetUserTag(int id)
        {
            var response = await _userTagService.GetUserTag(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        // PUT: api/UserTags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTag(int id, UserTag userTag)
        {
            if (id != userTag.UserTagID)
            {
                return BadRequest();
            }

            try
            {
                await _userTagService.PutUserTag(id, userTag);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTagExists(id))
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

        // POST: api/UserTags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserTag>> PostUserTag(UserTag userTag)
        {
            await _userTagService.PostUserTag(userTag);
            return CreatedAtAction("GetUserTag", new { id = userTag.UserTagID }, userTag);
        }

        // DELETE: api/UserTags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTag(int id)
        {
            var userTag = await _userTagService.GetUserTag(id);
            if (userTag == null)
            {
                return NotFound();
            }


            await _userTagService.DeleteUserTag(id);
            return NoContent();
        }

        private bool UserTagExists(int id)
        {
            return _userTagService.UserTagExists(id);
        }
    }
}
