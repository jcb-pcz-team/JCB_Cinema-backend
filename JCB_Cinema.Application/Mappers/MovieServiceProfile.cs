using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Mappers
{
    public class MovieServiceProfile : Profile
    {
        public MovieServiceProfile()
        {
            CreateMap<Movie, GetMovieDTO>()
             .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId))
             .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
             .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
             .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
             .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre)) // Mapowanie do GetGenreDTO
             .ForMember(dest => dest.PosterURL, opt => opt.MapFrom(src => src.PosterURL))
             .ForMember(dest => dest.Release, opt => opt.MapFrom(src => src.ReleaseDate))
             .ReverseMap()
             .ForMember(src => src.Genre, opt => opt.MapFrom(dest => dest.Genre))
             .ForPath(src => src.ReleaseDate, opt => opt.MapFrom(dest => dest.Release));

            CreateMap<AddMovieDTO, Movie>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dest => dest.Poster, opt => opt.Ignore());
            CreateMap<UpdateMovieDTO, Movie>();
        }
    }
}
