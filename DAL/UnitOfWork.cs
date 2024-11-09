using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;

namespace DAL
{
    public class UnitOfWork
    {
        private readonly TestingDBContext _dbContext;
        public readonly CompletedTaskRepository CompletedTaskRepository;
        public readonly ComplexityRepository ComplexityRepository;
        public readonly TestTaskRepository TestTaskRepository;
        public readonly RoleRepository RoleRepository;
        public readonly UserRepository UserRepository;
        public readonly TaskTypeRepository TaskTypeRepository;

        public UnitOfWork(TestingDBContext dBContext) 
        { 
            _dbContext = dBContext;
            CompletedTaskRepository = new CompletedTaskRepository(dBContext);
            ComplexityRepository = new ComplexityRepository(dBContext);
            TestTaskRepository = new TestTaskRepository(dBContext);
            RoleRepository = new RoleRepository(dBContext);
            UserRepository = new UserRepository(dBContext);
            TaskTypeRepository = new TaskTypeRepository(dBContext);
        }

        public async Task SaveAllAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
