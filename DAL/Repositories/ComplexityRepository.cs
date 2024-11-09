using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ComplexityRepository
    {
        private readonly TestingDBContext _dbContext;

        public ComplexityRepository(TestingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Complexity>> GetAllAsync()
        {
            return await _dbContext.Complexities.ToListAsync();
        }

        public async Task<Complexity> GetOneAsync(Guid id)
        {
            return await _dbContext.Complexities.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Complexity model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
        }

        public async Task RemoveAsync(Guid id)
        {
            var model = await _dbContext.Complexities.FirstOrDefaultAsync(e => e.Id == id);

            _dbContext.Complexities.Remove(model);
        }

        public async Task CreateAsync(Complexity model)
        {
            model.Id = Guid.NewGuid();
            _dbContext.Complexities.Add(model);
        }
    }
}
