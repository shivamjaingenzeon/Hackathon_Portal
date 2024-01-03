using AutoMapper;
using hackathonbackend.Data;
using hackathonbackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace hackathonbackend.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly HackathonDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountRepository(HackathonDbContext context, IMapper mapper,IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
       /* public Task<ResponseDto<object>> LoginAsync(LoginDto loginDto)
        {
            var result = _context.AuthenticationUsers.FindAsync(loginDto.Username);

            
        }*/
        public async Task<ResponseDto<T>> LoginAsync<T>(LoginDto loginDto) where T : class
        {
            Console.WriteLine(loginDto.Username);
            Console.WriteLine(loginDto.Password);
            var user = await _context.AuthenticationUsers
                    .FirstOrDefaultAsync(u => u.Username == loginDto.Username);
            Console.WriteLine(user.PasswordHash);
           if (user.PasswordHash != loginDto.Password)
            {
                user = null;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginDto.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]));
            Console.WriteLine($"{authSigninKey}");

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:ValidIssuer"],
                audience: _configuration["JwtSettings:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );


            if (user == null)
            {
                Console.WriteLine("User Null");
                return new ResponseDto<T>
                {
                    Success = false,
                    Message = "Invalid credentials"
                };
            }

            if (user.IsCompany)
            {
                var company = await _context.Companies.FindAsync(user.Id);

                if (company == null)
                {
                    return new ResponseDto<T>
                    {
                        Success = false,
                        Message = "Company not found"
                    };
                }

                var companyResponseDto = new CompanyLoginResponseDto
                {
                    CompanyId = company.Id,
                    CompanyName = company.Name,
                    Contact = company.Contact,
                    Location = company.Location,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                // Add other properties as needed
            };
                var response = new ResponseDto<T>
                {
                    IsCompany = true,
                    Success = true,
                    Message = "Company login successful",
                    Data = companyResponseDto as T
                };

                return response;
            }
            else
            {
                Console.WriteLine(user.Id);
                var team = await _context.Teams.Where( u => u.AuthenticationUserId == user.Id).FirstOrDefaultAsync();
                Console.WriteLine("in loign " + team);
                if (team == null)
                {
                    return new ResponseDto<T>
                    {
                        Success = false,
                        Message = "Team not found"
                    };
                }

                var teamResponseDto = new TeamLoginResponseDto
                {
                    TeamId = team.Id,
                    TeamName = team.Name,
                    NumberOfMembers = team.NumberOfMembers,
                    College = team.College,
                    Contact = team.Contact,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                    // Add other properties as needed
                };

                var response = new ResponseDto<T>
                {
                    Success = true,
                    Message = "Team login successful",
                    Data = teamResponseDto as T
                };

                return response;
            }
        }


        public Task<IdentityResult> SignUpAsync(TeamSignInDto singInDto)
        {
            throw new NotImplementedException();
        }
    }
}
