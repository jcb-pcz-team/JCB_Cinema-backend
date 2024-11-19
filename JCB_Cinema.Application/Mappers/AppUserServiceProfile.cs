using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests;
using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Mappers
{
    public class AppUserServiceProfile : Profile
    {
        public AppUserServiceProfile()
        {
            CreateMap<AppUser, AppUserDTO>().ReverseMap();
            CreateMap<PutAppUserDetails, AppUser>().ReverseMap();
        }
    }
}
