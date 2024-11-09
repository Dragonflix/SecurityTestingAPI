using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TaskTypeRepository
    {
        private readonly TestingDBContext _dbContext;

        public TaskTypeRepository(TestingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<TaskType>> GetAllAsync()
        {
            return await _dbContext.TaskTypes.ToListAsync();
        }

        public async Task<TaskType> GetOneAsync(Guid id)
        {
            return await _dbContext.TaskTypes.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(TaskType model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
        }

        public async Task RemoveAsync(Guid id)
        {
            var model = await _dbContext.TaskTypes.FirstOrDefaultAsync(e => e.Id == id);

            _dbContext.TaskTypes.Remove(model);
        }

        public async Task CreateAsync(TaskType model)
        {
            model.Id = Guid.NewGuid();
            _dbContext.TaskTypes.Add(model);
        }
    }
}
