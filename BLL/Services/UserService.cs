using AutoMapper;
using BLL.DTO;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BLL.Services
{
    public class UserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(UnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ICollection<UserDTO>> GetAllAsync()
        {
            var results = await _unitOfWork.UserRepository.GetAllAsync();
            return results.Select(_mapper.Map<UserDTO>).ToList();
        }

        public async Task<UserDTO> GetOneAsync(Guid id)
        {
            var result = await _unitOfWork.UserRepository.GetOneAsync(id);
            return _mapper.Map<UserDTO>(result);
        }

        public async Task UpdateAsync(UserDTO model)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(model.Password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            var mappedRoles = model.Roles.Select(async id => await _unitOfWork.RoleRepository.GetOneAsync(id)).Select(t => t.Result).ToList();

            var originalUser = await _unitOfWork.UserRepository.GetOneAsync(model.Id);
            originalUser.UserName = model.UserName;
            originalUser.Password = Convert.ToHexString(hashBytes);
            originalUser.Roles.Clear();
            originalUser.Roles = mappedRoles;

            await _unitOfWork.UserRepository.UpdateAsync(originalUser);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            await _unitOfWork.UserRepository.RemoveAsync(id);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task CreateAsync(UserDTO model)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(model.Password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            var mappedRoles = model.Roles.Select(async id => await _unitOfWork.RoleRepository.GetOneAsync(id)).Select(t => t.Result).ToList();

            var mappedUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = model.UserName,
                Password = Convert.ToHexString(hashBytes),
                Roles = mappedRoles,
            };

            await _unitOfWork.UserRepository.CreateAsync(mappedUser);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task<string> AuthorizeAsync(string userName, string password)
        {
            var user = await _unitOfWork.UserRepository.GetByUsername(userName);

            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            var passwordHash = Convert.ToHexString(hashBytes);

            if (user == null || passwordHash != user.Password)
            {
                throw new Exception("Incorrect UserName or Password");
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
