using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Mappers
{
    public class AppUserServiceProfile : Profile
    {
        public AppUserServiceProfile()
        {
            CreateMap<AppUser, GetAppUserDTO>()
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
            CreateMap<QueryAppUserDetails, AppUser>().ReverseMap();
            CreateMap<PutAppUserDetails, AppUser>().ReverseMap();
            CreateMap<ChangeUserPassword, AppUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.NewPassword));
        }
    }
}
