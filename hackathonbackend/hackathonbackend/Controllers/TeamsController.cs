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
    public class TeamsController : ControllerBase
    {
        private readonly HackathonDbContext _context;
        private readonly IMapper _mapper;

        public TeamsController(HackathonDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return await _context.Teams.ToListAsync();
        }


        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        [HttpGet("getTeamsByHackathonId/{hackathonId}")]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeamsByHackathonId(int hackathonId)
        {
            var teams = await _context.Teams.Where(t => t.HackathonId == hackathonId).ToListAsync();

            if (teams == null || teams.Count == 0)
            {
                return NotFound();
            }
            Console.WriteLine(teams);
            var teamDtos = _mapper.Map<IEnumerable<TeamDto>>(teams);
            return Ok(teamDtos);
        }

        [HttpGet("getAssignedUserStory/{teamId}")]
        public async Task<ActionResult<IEnumerable<UserStoryDto>>> getAssignedUserStory(int teamId) {
            var result = await _context
                                .UserStories
                                .Where(u => u.TeamId == teamId).ToListAsync();
            return Ok(result);
        }

        [HttpGet("getTeamMembers/{id}")]
        public async Task<ActionResult<IEnumerable<Member>>> GetTeamMembersByTeamId(int id)
        {
            IEnumerable<Member> members = await _context.Members.
                Where(e => e.TeamId == id)
                .ToListAsync();
            

            if (members == null)
            {
                return NotFound();
            }

            return Ok(members);
        }



        // PUT: api/Teams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
