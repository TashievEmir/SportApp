using AutoMapper;
using SportApp.Entities;
using SportApp.Models;

namespace SportApp.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<RegisterDto, User>();
        }
    }
}
