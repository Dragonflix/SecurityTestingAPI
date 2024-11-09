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

        public async Task<ICollection<CompletedTaskDTO>> GetAllAsync()
        {
            var results = await _unitOfWork.CompletedTaskRepository.GetAllAsync();
            return results.Select(_mapper.Map<CompletedTaskDTO>).ToList();
        }

        public async Task<CompletedTaskDTO> GetOneAsync(Guid id)
        {
            var result = await _unitOfWork.CompletedTaskRepository.GetOneAsync(id);
            return _mapper.Map<CompletedTaskDTO>(result);
        }

        public async Task UpdateAsync(CompletedTaskDTO model)
        {
            await _unitOfWork.CompletedTaskRepository.UpdateAsync(_mapper.Map<CompletedTask>(model));
            await _unitOfWork.SaveAllAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            await _unitOfWork.CompletedTaskRepository.RemoveAsync(id);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task CreateAsync(CompletedTaskDTO model)
        {
            await _unitOfWork.CompletedTaskRepository.CreateAsync(_mapper.Map<CompletedTask>(model));
            await _unitOfWork.SaveAllAsync();
        }
    }
}
