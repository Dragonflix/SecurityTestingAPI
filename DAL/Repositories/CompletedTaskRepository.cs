using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CompletedTaskRepository
    {
        private readonly TestingDBContext _dbContext;

        public CompletedTaskRepository(TestingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<CompletedTask>> GetAllAsync()
        {
            return await _dbContext.CompletedTasks.ToListAsync();
        }

        public async Task<ICollection<CompletedTask>> GetByUserAsync(Guid userId)
        {
            return await _dbContext.CompletedTasks.Where(e => e.UserId == userId).ToListAsync();
        }

        public async Task<CompletedTask> GetOneAsync(Guid id)
        {
            return await _dbContext.CompletedTasks.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(CompletedTask model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
        }

        public async Task RemoveAsync(Guid id)
        {
            var model = await _dbContext.CompletedTasks.FirstOrDefaultAsync(e => e.Id == id);

            _dbContext.CompletedTasks.Remove(model);
        }

        public async Task CreateAsync(CompletedTask model)
        {
            model.Id = Guid.NewGuid();
            _dbContext.CompletedTasks.Add(model);
        }
    }
}
