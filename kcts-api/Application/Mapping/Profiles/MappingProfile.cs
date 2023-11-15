using Application.Auth.Models;
using Application.Features.Auth.ResponseModels;
using Application.Features.Common.ResponseModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Mapping.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tuple<ApplicationUser, IList<string>>, UserResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Item1.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Item1.Email))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Item2.ToList()));

            CreateMap<IdentityError, ErrorInfo>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<IdentityResult, ResultResponse>()
                .ForMember(dest => dest.Succeeded, opt => opt.MapFrom(src => src.Succeeded))
                .ForMember(dest => dest.Errors, opt => opt.MapFrom(src => src.Errors));
        }

    }
}
