using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Mappers
{
    public class MovieServiceProfile : Profile
    {
        public MovieServiceProfile()
        {
            CreateMap<Movie, GetMovieDTO>().ReverseMap();
        }
    }
}
