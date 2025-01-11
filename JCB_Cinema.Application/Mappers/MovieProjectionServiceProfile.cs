using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Tools;

namespace JCB_Cinema.Application.Mappers
{
    /// <summary>
    /// AutoMapper profile class for configuring mappings between the <see cref="MovieProjection"/> entity 
    /// and its related data transfer objects (DTOs), including <see cref="GetMovieProjectionDTO"/>, 
    /// and request models like <see cref="AddMovieProjectionRequest"/> and <see cref="UpdateMovieProjectionRequest"/>.
    /// </summary>
    public class MovieProjectionServiceProfile : Profile
    {
        /// <summary>
        /// Configures the object mappings for <see cref="MovieProjection"/> to <see cref="GetMovieProjectionDTO"/>, 
        /// and vice versa. Also includes mappings for adding and updating movie projections.
        /// </summary>
        public MovieProjectionServiceProfile()
        {
            // Mapping from MovieProjection to GetMovieProjectionDTO
            CreateMap<MovieProjection, GetMovieProjectionDTO>()
                .ForMember(dest => dest.MovieProjectionId, opt => opt.MapFrom(src => src.MovieProjectionId)) // MovieProjectionId mapping
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie)) // Movie mapping
                .ForMember(dest => dest.ScreeningTime, opt => opt.MapFrom(src => src.ScreeningTime)) // ScreeningTime mapping
                .ForMember(dest => dest.ScreenType, opt => opt.MapFrom(src => src.ScreenType.GetDescription())) // ScreenType description
                .ForMember(dest => dest.CinemaHall, opt => opt.MapFrom(src => src.CinemaHall)) // CinemaHall mapping
                .ForMember(dest => dest.NormalizedMovieTitle, opt => opt.MapFrom(src => src.MovieNormalizedTitle)) // NormalizedMovieTitle mapping
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price)) // Price mapping
                .ForMember(dest => dest.OccupiedSeats, opt => opt.MapFrom(src => src.OccupiedSeats)) // OccupiedSeats mapping
                .ForMember(dest => dest.AvailableSeats, opt => opt.MapFrom(src => src.AvailableSeats)); // AvailableSeats mapping

            // Mapping from GetMovieProjectionDTO to MovieProjection
            CreateMap<GetMovieProjectionDTO, MovieProjection>()
                .ForMember(dest => dest.Movie, opt => opt.Ignore()) // Ignore Movie mapping for now
                .ForMember(dest => dest.ScreeningTime, opt => opt.MapFrom(src => src.ScreeningTime)) // ScreeningTime mapping
                .ForMember(dest => dest.ScreenType, opt => opt.MapFrom(src => EnumExtensions.GetValueFromDescription<ScreenType>(src.ScreenType ?? ScreenType.TwoD.GetDescription()))) // ScreenType mapping using enum description
                .ForMember(dest => dest.CinemaHall, opt => opt.MapFrom(src => src.CinemaHall)) // CinemaHall mapping
                .ForMember(dest => dest.MovieNormalizedTitle, opt => opt.MapFrom(src => src.NormalizedMovieTitle)) // NormalizedMovieTitle mapping
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price)) // Price mapping
                .ForMember(dest => dest.MovieProjectionId, opt => opt.Ignore()) // Ignore MovieProjectionId
                .ForMember(dest => dest.OccupiedSeats, opt => opt.Ignore()) // OccupiedSeats mapping handled internally
                .ForMember(dest => dest.AvailableSeats, opt => opt.Ignore()); // AvailableSeats mapping handled internally

            // Mapping from AddMovieProjectionRequest to MovieProjection
            CreateMap<AddMovieProjectionRequest, MovieProjection>()
                .ForMember(dest => dest.ScreeningTime, opt => opt.MapFrom(src => src.ScreeningTime)) // ScreeningTime mapping
                .ForMember(dest => dest.ScreenType, opt => opt.MapFrom(src => src.ScreenType)) // ScreenType mapping
                .ForMember(dest => dest.CinemaHall, opt => opt.Ignore()) // Ignore CinemaHall mapping
                .ForMember(dest => dest.MovieNormalizedTitle, opt => opt.Ignore()); // Ignore MovieNormalizedTitle mapping

            // Mapping from UpdateMovieProjectionRequest to MovieProjection
            CreateMap<UpdateMovieProjectionRequest, MovieProjection>()
                .ForMember(dest => dest.CinemaHallId, opt => opt.MapFrom(src => src.CinemaHallId)) // CinemaHallId mapping
                .ForMember(dest => dest.MovieNormalizedTitle, opt => opt.Ignore()); // Ignore MovieNormalizedTitle mapping

            // Mapping for QueryMovieProjectionsCount request
            CreateMap<QueryMovieProjectionsCount, QueryMovieProjectionsCount>();
        }
    }
}
