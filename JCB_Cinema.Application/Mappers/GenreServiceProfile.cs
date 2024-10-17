using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Tools;

namespace JCB_Cinema.Application.Mappers
{
    public class GenreServiceProfile : Profile
    {
        public GenreServiceProfile()
        {
            CreateMap<Genre, GetGenereDTO>()
               .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => (int?)src))
               .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.GetDescription()))  // Mapowanie opisu gatunku
               .ReverseMap();
        }
    }
}
