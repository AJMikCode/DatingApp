using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    // Profile class must have using AutoMapper
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
           CreateMap<AppUser, MemberDto>()
                    // Get the destination to where the Map is going to.
                    // Get source of where you're, mapping fronm.
                        // Goes into Photo collection of Photo.cs and gets first one with property IsMain and Url.
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => 
                    src.Photos.FirstOrDefault(x => x.IsMain).Url))
                    //Selects Age property from MemberDto, AppUser and Maps from Date
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
           CreateMap<Photo, PhotoDto>();
        }
    }
}