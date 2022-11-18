using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Parking.Models.DTO;

namespace Parking.Models.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<IdentityUser, User>().ReverseMap();
        }
    }
}
