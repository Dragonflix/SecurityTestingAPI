using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TestTaskRepository
    {
        private readonly TestingDBContext _dbContext;

        public TestTaskRepository(TestingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<TestTask>> GetAllAsync()
        {
            return await _dbContext.TestTasks.ToListAsync();
        }

        public async Task<TestTask> GetOneAsync(Guid id)
        {
            return await _dbContext.TestTasks.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(TestTask model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
        }

        public async Task RemoveAsync(Guid id)
        {
            var model = await _dbContext.TestTasks.FirstOrDefaultAsync(e => e.Id == id);

            _dbContext.TestTasks.Remove(model);
        }

        public async Task CreateAsync(TestTask model)
        {
            model.Id = Guid.NewGuid();
            _dbContext.TestTasks.Add(model);
        }
    }
}
