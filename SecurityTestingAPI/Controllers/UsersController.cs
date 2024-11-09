using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityTestingAPI;
using SecurityTestingAPI.DTO;
using SecurityTestingAPI.Models;

namespace SecurityTestingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TestingDBContext _context;
        private readonly IMapper _mapper;

        public UsersController(TestingDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return await _context.Users.Include(u => u.Roles).Select(u => _mapper.Map<UserDTO>(u)).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return _mapper.Map<UserDTO>(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserDTO user)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(user.Password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            if (id != user.Id)
            {
                return BadRequest();
            }

            var originalUser = _context.Users.Include(u => u.Roles).First(u => u.Id == id);
            originalUser.UserName = user.UserName;
            originalUser.Password = Convert.ToHexString(hashBytes);
            originalUser.Roles.Clear();
            originalUser.Roles = user.Roles.Select(id => _context.Roles.First(r => r.Id == id)).ToList();

            _context.Users.Update(originalUser);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO user)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(user.Password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            var mappedUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = user.UserName,
                Password = Convert.ToHexString(hashBytes),
                Roles = user.Roles.Select(id => _context.Roles.First(r => r.Id == id)).ToList(),
            };
            _context.Users.Add(mappedUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
