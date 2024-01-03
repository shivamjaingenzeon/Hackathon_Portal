using AutoMapper;
using hackathonbackend.Data;
using hackathonbackend.Models;

namespace EventManagementTool.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Member, MemberDto>().ReverseMap();
            CreateMap<Hackathon, HackathonDto>().ReverseMap();
            CreateMap<UserStory, UserStoryDto>().ReverseMap();
            CreateMap<Team,TeamDto>().ReverseMap();  


        }
    }
}
