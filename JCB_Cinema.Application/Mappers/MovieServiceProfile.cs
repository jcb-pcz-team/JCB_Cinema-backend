using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Tools;

namespace JCB_Cinema.Application.Mappers
{
    /// <summary>
    /// AutoMapper profile class for configuring mappings between the <see cref="Movie"/> entity
    /// and its related data transfer objects (DTOs), including <see cref="GetMovieDTO"/>, 
    /// <see cref="AddMovieRequest"/>, and <see cref="UpdateMovieRequest"/>.
    /// </summary>
    public class MovieServiceProfile : Profile
    {
        /// <summary>
        /// Configures the object mappings for <see cref="Movie"/> to <see cref="GetMovieDTO"/>, 
        /// <see cref="GetMovieTitleDTO"/>, and request models like <see cref="AddMovieRequest"/> and <see cref="UpdateMovieRequest"/>.
        /// </summary>
        public MovieServiceProfile()
        {
            // Mapping from Movie to GetMovieDTO
            CreateMap<Movie, GetMovieDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title)) // Title mapping
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description)) // Description mapping
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration)) // Duration mapping
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate)) // ReleaseDate mapping
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre)) // Genre mapping (Map to GetGenreDTO)
                .ForMember(dest => dest.NormalizedTitle, opt => opt.MapFrom(src => src.NormalizedTitle)) // NormalizedTitle mapping
                .ForMember(dest => dest.Release, opt => opt.MapFrom(src => src.ReleaseDate)) // Release mapping
                .ReverseMap() // Reverse map configuration
                .ForMember(src => src.Genre, opt => opt.MapFrom(dest => dest.Genre)) // Reverse Genre mapping
                .ForPath(src => src.ReleaseDate, opt => opt.MapFrom(dest => dest.Release)); // Reverse ReleaseDate mapping

            // Mapping from AddMovieRequest to Movie
            CreateMap<AddMovieRequest, Movie>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title)) // Title mapping
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description)) // Description mapping
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration)) // Duration mapping
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate)) // ReleaseDate mapping
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => EnumExtensions.GetValueFromDescription<Genre>(src.Genre))) // Genre mapping using Enum description
                .ForMember(dest => dest.NormalizedTitle, opt => opt.Ignore()) // Ignore NormalizedTitle
                .ForMember(dest => dest.Photo, opt => opt.Ignore()); // Ignore Photo

            // Mapping from UpdateMovieRequest to Movie
            CreateMap<UpdateMovieRequest, Movie>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => EnumExtensions.GetValueFromDescription<Genre>(src.Genre))); // Genre mapping using Enum description

            // Mapping from Movie to GetMovieTitleDTO
            CreateMap<Movie, GetMovieTitleDTO>();
        }
    }
}
