using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository
    {
        private readonly TestingDBContext _dbContext;

        public UserRepository(TestingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetOneAsync(Guid id)
        {
            return await _dbContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User> GetByUsername(string userName)
        {
            return await _dbContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(e => e.UserName == userName);
        }

        public async Task UpdateAsync(User model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
        }

        public async Task RemoveAsync(Guid id)
        {
            var model = await _dbContext.Users.FirstOrDefaultAsync(e => e.Id == id);

            _dbContext.Users.Remove(model);
        }

        public async Task CreateAsync(User model)
        {
            model.Id = Guid.NewGuid();
            _dbContext.Users.Add(model);
        }
    }
}
