using hackathonbackend.Data;
using hackathonbackend.Models;
using hackathonbackend.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;


namespace hackathonbackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly HackathonDbContext _dbContext;

        public AccountController(IAccountRepository accountRepository, IConfiguration configuration, HackathonDbContext dbContext)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] TeamSignInDto signInDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid signup request");
            }

            var team = new Team
            {
                Name = signInDto.TeamName,
                NumberOfMembers = signInDto.NumberOfMembers,
                College = signInDto.College,
                Contact = signInDto.Contact,
                CompanyId = 1,
                HackathonId =1
                // Set other team properties as needed
            };

            var authenticationUser = new AuthenticationUser
            {
                Username = signInDto.Username,
                PasswordHash = signInDto.Password,
                IsCompany = false
            };

            team.AuthenticationUser = authenticationUser;

            _dbContext.Teams.Add(team);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Team registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            Console.WriteLine("IN Login Controller");
            var teamLoginResponse = await _accountRepository.LoginAsync<TeamLoginResponseDto>(loginDto);
            if (teamLoginResponse.Data != null && teamLoginResponse.Data.TeamName != null)
            {
               /* var teamResponseDto = new TeamLoginResponseDto
                {
                    TeamId = teamLoginResponse.Data.TeamId,
                    TeamName = teamLoginResponse.Data.TeamName,
                    NumberOfMembers = teamLoginResponse.Data.NumberOfMembers,
                    College = teamLoginResponse.Data.College,
                    Contact = teamLoginResponse.Data.Contact
                };

                var response = new ResponseDto<TeamLoginResponseDto>
                {
                    Success = true,
                    Message = "Team login successful",
                    Data = teamResponseDto
                };
*/
                return Ok(teamLoginResponse);
            }

            var companyLoginResponse = await _accountRepository.LoginAsync<CompanyLoginResponseDto>(loginDto);
            if (companyLoginResponse.Data != null)
            {
                /*var companyResponseDto = new CompanyLoginResponseDto
                {
                    CompanyId = companyLoginResponse.Data.CompanyId,
                    CompanyName = companyLoginResponse.Data.CompanyName,
                    Contact = companyLoginResponse.Data.Contact,
                    Location = companyLoginResponse.Data.Location
                };

                var response = new ResponseDto<CompanyLoginResponseDto>
                {
                    IsCompany = true,
                    Success = true,
                    Message = "Company login successful",
                    Data = companyResponseDto
                };*/

                return Ok(companyLoginResponse);
            }

            return Unauthorized();
        }

    }
}


    /*// Helper method to get the user by username
    private async Task<AuthenticationUser> GetUserByUsername(string username)
        {
            return await _dbContext.AuthenticationUsers.FirstOrDefaultAsync(u => u.Username == username);
        }
    }*/

