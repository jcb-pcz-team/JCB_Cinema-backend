using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Mappers
{
    public class AppUserEmailServiceProfile : Profile
    {
        public AppUserEmailServiceProfile() 
        {
            CreateMap<AppUser, GetAppUserEmailDTO>()
                .ForMember(dest => dest.CurrentEmail, opt => opt.MapFrom(src => src.Email));
            CreateMap<QueryAppUserEmail, AppUser>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.NewEmail));
        }
    }
}
