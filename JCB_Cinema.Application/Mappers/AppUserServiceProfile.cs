using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Mappers
{
    /// <summary>
    /// AutoMapper profile class for configuring mappings between the <see cref="AppUser"/> entity and various DTOs 
    /// (Data Transfer Objects) used in the application.
    /// </summary>
    public class AppUserServiceProfile : Profile
    {
        /// <summary>
        /// Configures the object mappings for <see cref="AppUser"/> and related DTOs.
        /// </summary>
        public AppUserServiceProfile()
        {
            // Map from AppUser to GetAppUserDTO, mapping the UserName property to Login
            CreateMap<AppUser, GetAppUserDTO>()
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();

            // Map between QueryAppUserDetails and AppUser
            CreateMap<QueryAppUserDetails, AppUser>().ReverseMap();

            // Map between PutAppUserDetails and AppUser
            CreateMap<PutAppUserDetails, AppUser>().ReverseMap();

            // Map from ChangeUserPassword to AppUser, mapping the NewPassword to PasswordHash
            CreateMap<ChangeUserPassword, AppUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.NewPassword));

            // Map from AppUser to GetAppUserEmailDTO, mapping Email to CurrentEmail
            CreateMap<AppUser, GetAppUserEmailDTO>()
                .ForMember(dest => dest.CurrentEmail, opt => opt.MapFrom(src => src.Email));

            // Map from QueryAppUserEmail to AppUser, mapping NewEmail to Email
            CreateMap<QueryAppUserEmail, AppUser>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.NewEmail));
        }
    }
}
