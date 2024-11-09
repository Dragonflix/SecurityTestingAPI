using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SecurityTestingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly TestingDBContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public HomeController(TestingDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration; 
        }

        [HttpGet("Authorize")]
        public async Task<ActionResult<string>> Authorize(string userName, string password)
        {
            var user = _context.Users.Include(u => u.Roles).FirstOrDefault(u => u.UserName == userName);

            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            var passwordHash = Convert.ToHexString(hashBytes);


            if (user == null || passwordHash != user.Password)
            {
                return BadRequest(userName);  
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
