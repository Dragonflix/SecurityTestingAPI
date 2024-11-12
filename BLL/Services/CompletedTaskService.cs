using DAL.Models;
using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;

namespace BLL.Services
{
    public class CompletedTaskService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompletedTaskService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<CompletedTaskDto>> GetAllAsync()
        {
            var results = await _unitOfWork.CompletedTaskRepository.GetAllAsync();
            return results.Select(_mapper.Map<CompletedTaskDto>).ToList();
        }
        public async Task<ICollection<CompletedTaskDto>> GetByUserAsync(Guid userId)
        {
            var results = await _unitOfWork.CompletedTaskRepository.GetByUserAsync(userId);
            return results.Select(_mapper.Map<CompletedTaskDto>).ToList();
        }

        public async Task<CompletedTaskDto> GetOneAsync(Guid id)
        {
            var result = await _unitOfWork.CompletedTaskRepository.GetOneAsync(id);
            return _mapper.Map<CompletedTaskDto>(result);
        }

        public async Task UpdateAsync(CompletedTaskDto model)
        {
            await _unitOfWork.CompletedTaskRepository.UpdateAsync(_mapper.Map<CompletedTask>(model));
            await _unitOfWork.SaveAllAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            await _unitOfWork.CompletedTaskRepository.RemoveAsync(id);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task CreateAsync(CompletedTaskDto model)
        {
            await _unitOfWork.CompletedTaskRepository.CreateAsync(_mapper.Map<CompletedTask>(model));
            await _unitOfWork.SaveAllAsync();
        }
    }
}
