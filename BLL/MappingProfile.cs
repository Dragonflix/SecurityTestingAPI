using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace BLL
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(e => e.Roles.Select(r => r.Id)));
            CreateMap<Complexity, ComplexityDTO>()
                .ReverseMap();
            CreateMap<TaskType, TaskTypeDTO>()
                .ReverseMap();
            CreateMap<TestTask, TestTaskDTO>()
                .ReverseMap();
            CreateMap<CompletedTask, CompletedTaskDTO>()
                .ReverseMap();
        }
    }
}
