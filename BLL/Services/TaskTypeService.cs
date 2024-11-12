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
    public class TaskTypeService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskTypeService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<TaskTypeDto>> GetAllAsync()
        {
            var results = await _unitOfWork.TaskTypeRepository.GetAllAsync();
            return results.Select(_mapper.Map<TaskTypeDto>).ToList();
        }

        public async Task<TaskTypeDto> GetOneAsync(Guid id)
        {
            var result = await _unitOfWork.TaskTypeRepository.GetOneAsync(id);
            return _mapper.Map<TaskTypeDto>(result);
        }

        public async Task UpdateAsync(TaskTypeDto model)
        {
            await _unitOfWork.TaskTypeRepository.UpdateAsync(_mapper.Map<TaskType>(model));
            await _unitOfWork.SaveAllAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            await _unitOfWork.TaskTypeRepository.RemoveAsync(id);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task CreateAsync(TaskTypeDto model)
        {
            await _unitOfWork.TaskTypeRepository.CreateAsync(_mapper.Map<TaskType>(model));
            await _unitOfWork.SaveAllAsync();
        }
    }
}
