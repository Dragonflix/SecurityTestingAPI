using AutoMapper;
using BLL.DTO;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ComplexityService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ComplexityService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<ComplexityDto>> GetAllAsync()
        {
            var results = await _unitOfWork.ComplexityRepository.GetAllAsync();
            return results.Select(_mapper.Map<ComplexityDto>).ToList();
        }

        public async Task<ComplexityDto> GetOneAsync(Guid id)
        {
            var result = await _unitOfWork.ComplexityRepository.GetOneAsync(id);
            return _mapper.Map<ComplexityDto>(result);
        }

        public async Task UpdateAsync(ComplexityDto model)
        {
            await _unitOfWork.ComplexityRepository.UpdateAsync(_mapper.Map<Complexity>(model));
            await _unitOfWork.SaveAllAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            await _unitOfWork.ComplexityRepository.RemoveAsync(id);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task CreateAsync(ComplexityDto model)
        {
            await _unitOfWork.ComplexityRepository.CreateAsync(_mapper.Map<Complexity>(model));
            await _unitOfWork.SaveAllAsync();
        }
    }
}
