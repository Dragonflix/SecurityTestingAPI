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

        public async Task<ICollection<TestTaskDto>> GetAllAsync(FilterModel filterModel)
        {
            var results = await _unitOfWork.TestTaskRepository.GetAllAsync();

            if (filterModel.ComplexityIds != null && filterModel.ComplexityIds.Any())
            {
                results = results.Where(r => filterModel.ComplexityIds.Contains(r.ComplexityId)).ToList();
            }
            if (filterModel.TaskTypeIdIds != null && filterModel.TaskTypeIdIds.Any())
            {
                results = results.Where(r => filterModel.TaskTypeIdIds.Contains(r.TypeId)).ToList();
            }

            return results.Select(_mapper.Map<TestTaskDto>).ToList();
        }

        public async Task<TestTaskDto> GetOneAsync(Guid id)
        {
            var result = await _unitOfWork.TestTaskRepository.GetOneAsync(id);
            return _mapper.Map<TestTaskDto>(result);
        }

        public async Task UpdateAsync(TestTaskDto model)
        {
            await _unitOfWork.TestTaskRepository.UpdateAsync(_mapper.Map<TestTask>(model));
            await _unitOfWork.SaveAllAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            await _unitOfWork.TestTaskRepository.RemoveAsync(id);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task CreateAsync(TestTaskDto model)
        {
            await _unitOfWork.TestTaskRepository.CreateAsync(_mapper.Map<TestTask>(model));
            await _unitOfWork.SaveAllAsync();
        }
    }
}
