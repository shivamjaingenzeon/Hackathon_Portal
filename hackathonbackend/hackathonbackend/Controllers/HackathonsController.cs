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
    public class HackathonsController : ControllerBase
    {
        private readonly HackathonDbContext _context;
        private readonly IMapper _mapper;

        public HackathonsController(HackathonDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Hackathons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hackathon>>> GetHackathons()
        {
            return await _context.Hackathons.ToListAsync();
        }

        // GET: api/Hackathons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hackathon>> GetHackathon(int id)
        {
            var hackathon = await _context.Hackathons.FindAsync(id);

            if (hackathon == null)
            {
                return NotFound();
            }

            return hackathon;
        }
        [HttpGet("getUserStoriesByHackathonId/{id}")]
        public async Task<ActionResult<IEnumerable<Hackathon>>> GetUserStoryByHackathonId(int id) {
            var userStories = await _context
                                    .UserStories
                                    .Where(u => u.HackathonId == id)
                                    .ToListAsync();
            var userStoryDtos = _mapper.Map<IEnumerable<UserStoryDto>>(userStories);

            return Ok(userStoryDtos);
        }

        [HttpPost("assignUserStory/{teamId}/{userStoryId}")]
        public async Task<IActionResult> AssignUserStoryToTeam(int teamId, int userStoryId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team == null)
            {
                return NotFound($"Team with ID {teamId} not found.");
            }

            var userStory = await _context.UserStories.FindAsync(userStoryId);
            if (userStory == null)
            {
                return NotFound($"User story with ID {userStoryId} not found.");
            }

            // Assign the user story to the team
            team.UserStoryId = userStoryId;
            userStory.IsAssigned = true;
            _context.UserStories.Update(userStory);
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // PUT: api/Hackathons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHackathon(int id, Hackathon hackathon)
        {
            if (id != hackathon.HackathonId)
            {
                return BadRequest();
            }

            _context.Entry(hackathon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HackathonExists(id))
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

        // POST: api/Hackathons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{companyId}")]
        public async Task<ActionResult<Hackathon>> PostHackathon(int companyId, HackathonDto hackathon)
        {
            Console.WriteLine("in hackathon");
            hackathon.StartDate = hackathon.StartDate.ToUniversalTime();
            hackathon.EndDate = hackathon.EndDate.ToUniversalTime();
            
            var newhackathon = _mapper.Map<Hackathon>(hackathon);
            newhackathon.CompanyId = companyId;

            _context.Hackathons.Add(newhackathon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHackathon", new { id = newhackathon.HackathonId }, newhackathon);
        }

        // DELETE: api/Hackathons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHackathon(int id)
        {
            var hackathon = await _context.Hackathons.FindAsync(id);
            if (hackathon == null)
            {
                return NotFound();
            }

            _context.Hackathons.Remove(hackathon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HackathonExists(int id)
        {
            return _context.Hackathons.Any(e => e.HackathonId == id);
        }
    }
}
