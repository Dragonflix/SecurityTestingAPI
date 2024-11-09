using AutoMapper;
using SecurityTestingAPI.DTO;
using SecurityTestingAPI.Models;

namespace SecurityTestingAPI
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(e => e.Roles.Select(r => r.Id)));
            CreateMap<Role, RoleDTO>()
                .ReverseMap(); ;
            CreateMap<TestTask, TestTaskDTO>()
                .ReverseMap(); ;
            CreateMap<CompletedTask, CompletedTaskDTO>()
                .ReverseMap(); ;
        }
    }
}
