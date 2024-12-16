using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Tools;

namespace JCB_Cinema.Application.Mappers
{
    public class ScreenTypeServiceProfile : Profile
    {
        public ScreenTypeServiceProfile() { 
            CreateMap<ScreenType, GetScreenTypeDTO>()
               .ForMember(dest => dest.ScreenTypeId, opt => opt.MapFrom(src => (int?)src))
               .ForMember(dest => dest.ScreenTypeName, opt => opt.MapFrom(src => src.GetDescription()))  // Mapowanie opisu gatunku
               .ReverseMap();
        }
    }
}
