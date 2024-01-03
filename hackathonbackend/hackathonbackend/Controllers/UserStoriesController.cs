using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hackathonbackend.Data;
using hackathonbackend.Models;
using AutoMapper;

namespace hackathonbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStoriesController : ControllerBase
    {
        private readonly HackathonDbContext _context;
        private readonly IMapper _mapper;

        public UserStoriesController(HackathonDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/UserStories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserStory>>> GetUserStories()
        {
            return await _context.UserStories.ToListAsync();

        }
        [HttpGet("getUserStory/{id}")]
        public async Task<ActionResult<IEnumerable<UserStoryDto>>> GetUserStoryByHackathonId(int id)
        { 
            var result = await _context.UserStories
                                        .Where( u => u.HackathonId == id)
                                        .ToListAsync();
            var newresult = _mapper.Map<IEnumerable<UserStoryDto>>(result);
            return Ok(newresult);

        }
        // GET: api/UserStories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserStory>> GetUserStory(int id)
        {
            var userStory = await _context.UserStories.FindAsync(id);

            if (userStory == null)
            {
                return NotFound();
            }

            return userStory;
        }

         // PUT: api/UserStories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserStory(int id, UserStory userStory)
        {
            if (id != userStory.UserStoryId)
            {
                return BadRequest();
            }

            _context.Entry(userStory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserStoryExists(id))
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

        // POST: api/UserStories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{hackathonId}")]
        public async Task<ActionResult<UserStory>> PostUserStory(int hackathonId,UserStory userStory)
        {
            userStory.HackathonId = hackathonId;
            _context.UserStories.Add(userStory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserStory", new { id = userStory.UserStoryId }, userStory);
        }

        // DELETE: api/UserStories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserStory(int id)
        {
            var userStory = await _context.UserStories.FindAsync(id);
            if (userStory == null)
            {
                return NotFound();
            }

            _context.UserStories.Remove(userStory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserStoryExists(int id)
        {
            return _context.UserStories.Any(e => e.UserStoryId == id);
        }
    }
}
