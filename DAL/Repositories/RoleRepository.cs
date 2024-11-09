using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RoleRepository
    {
        private readonly TestingDBContext _dbContext;

        public RoleRepository(TestingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Role>> GetAllAsync()
        {
            return await _dbContext.Roles.ToListAsync();
        }

        public async Task<Role> GetOneAsync(Guid id)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Role model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
        }

        public async Task RemoveAsync(Guid id)
        {
            var model = await _dbContext.Roles.FirstOrDefaultAsync(e => e.Id == id);

            _dbContext.Roles.Remove(model);
        }

        public async Task CreateAsync(Role model)
        {
            model.Id = Guid.NewGuid();
            _dbContext.Roles.Add(model);
        }
    }
}
