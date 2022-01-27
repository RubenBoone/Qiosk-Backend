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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private  ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            IEnumerable<Tag> response;
            var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            if (isAdmin)
            {

                response= await _tagService.GetTags();
            }
            else
            {
                return Unauthorized();
            }
            if (response == null)
                return BadRequest(new { message = "something went wrong in TagService" });

            return Ok(response);
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var response = await _tagService.GetTag(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        // PUT: api/Tags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(int id, Tag tag)
        {
            if (id != tag.TagID)
            {
                return BadRequest();
            }

            try
            {
                await _tagService.PutTag(id, tag);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
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

        // POST: api/Tags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            await _tagService.PostTag(tag);
            return CreatedAtAction("GetTag", new { id = tag.TagID }, tag);
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _tagService.GetTag(id);
            if (tag == null)
            {
                return NotFound();
            }

            var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            if (isAdmin)
            {

                await _tagService.DeleteTag(id);
            }
            else
            {
                return Unauthorized();
            }
            await _tagService.DeleteTag(id);
            return NoContent();
        }

        private bool TagExists(int id)
        {
            return _tagService.TagExists(id);
        }
    }
}

