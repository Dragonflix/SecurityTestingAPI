using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace BLL
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(e => e.Roles.Select(r => r.Id)));
            CreateMap<Complexity, ComplexityDto>()
                .ReverseMap();
            CreateMap<TaskType, TaskTypeDto>()
                .ReverseMap();
            CreateMap<TestTask, TestTaskDto>()
                .ReverseMap();
            CreateMap<CompletedTask, CompletedTaskDto>()
                .ReverseMap();
        }
    }
}
