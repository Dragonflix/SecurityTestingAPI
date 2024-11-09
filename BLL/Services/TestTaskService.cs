using AutoMapper;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Services
{
    public class TestTaskService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TestTaskService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<TestTaskDTO>> GetAllAsync()
        {
            var results = await _unitOfWork.TaskTypeRepository.GetAllAsync();
            return results.Select(_mapper.Map<TestTaskDTO>).ToList();
        }

        public async Task<TestTaskDTO> GetOneAsync(Guid id)
        {
            var result = await _unitOfWork.TaskTypeRepository.GetOneAsync(id);
            return _mapper.Map<TestTaskDTO>(result);
        }

        public async Task UpdateAsync(TestTaskDTO model)
        {
            await _unitOfWork.TestTaskRepository.UpdateAsync(_mapper.Map<TestTask>(model));
            await _unitOfWork.SaveAllAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            await _unitOfWork.TestTaskRepository.RemoveAsync(id);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task CreateAsync(TestTaskDTO model)
        {
            await _unitOfWork.TestTaskRepository.CreateAsync(_mapper.Map<TestTask>(model));
            await _unitOfWork.SaveAllAsync();
        }
    }
}
