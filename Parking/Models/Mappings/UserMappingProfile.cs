using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Parking.API.Models.DTO;

namespace Parking.API.Models.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<IdentityUser, User>().ReverseMap();
        }
    }
}
