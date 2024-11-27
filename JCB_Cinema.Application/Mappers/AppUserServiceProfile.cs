using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Mappers
{
    public class AppUserServiceProfile : Profile
    {
        public AppUserServiceProfile()
        {
            CreateMap<AppUser, GetAppUserDTO>().ReverseMap();
            CreateMap<PutAppUserDetails, AppUser>().ReverseMap();
        }
    }
}
