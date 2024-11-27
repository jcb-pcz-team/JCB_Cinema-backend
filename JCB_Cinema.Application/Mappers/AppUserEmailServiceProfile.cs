using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests;
using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Mappers
{
    public class AppUserEmailServiceProfile : Profile
    {
        public AppUserEmailServiceProfile() 
        {
            CreateMap<AppUser, GetAppUserEmailDTO>()
                .ForMember(dest => dest.CurrentEmail, opt => opt.MapFrom(src => src.Email));
            CreateMap<RequestAppUserEmail, AppUser>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.NewEmail));
        }
    }
}
