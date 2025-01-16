using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Application.Mappers
{
    /// <summary>
    /// AutoMapper profile class for configuring mappings between various entities 
    /// related to cinema halls, movie projections, and posters.
    /// </summary>
    public class CinemaHallServiceProfile : Profile
    {
        /// <summary>
        /// Configures the object mappings for <see cref="CinemaHall"/>, <see cref="MovieProjection"/>, 
        /// and <see cref="Photo"/> entities to their corresponding DTOs.
        /// </summary>
        public CinemaHallServiceProfile()
        {
            // Mapping from CinemaHall to GetCinemaHallDTO, including CinemaHallId and Name
            CreateMap<CinemaHall, GetCinemaHallDTO>()
                .ForMember(dest => dest.CinemaHallId, opt => opt.MapFrom(src => src.CinemaHallId)) // Map CinemaHallId
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)); // Map Name

            // Mapping from GetCinemaHallDTO to CinemaHall, including CinemaHallId and Name
            // Ignoring Seats as it's not part of the DTO
            CreateMap<GetCinemaHallDTO, CinemaHall>()
                .ForMember(dest => dest.CinemaHallId, opt => opt.MapFrom(src => src.CinemaHallId)) // Map CinemaHallId
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)) // Map Name
                .ForMember(dest => dest.Seats, opt => opt.Ignore()); // Ignore Seats

            // Mapping from Photo to GetPosterDTO for poster-related data
            CreateMap<Photo, GetPosterDTO>()
                .ForMember(dest => dest.PosterId, opt => opt.MapFrom(src => src.Id)); // Map Id to PosterId

            // Mapping from GetPosterDTO to Photo, with ignoring of irrelevant fields (Bytes, Description, etc.)
            CreateMap<GetPosterDTO, Photo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PosterId)) // Map PosterId to Id
                .ForMember(dest => dest.Bytes, opt => opt.Ignore()) // Ignore Bytes field
                .ForMember(dest => dest.Description, opt => opt.Ignore()) // Ignore Description field
                .ForMember(dest => dest.FileExtension, opt => opt.Ignore()) // Ignore FileExtension field
                .ForMember(dest => dest.Size, opt => opt.Ignore()); // Ignore Size field

            // Mapping from MovieProjection to GetMovieProjectionDTO
            CreateMap<MovieProjection, GetMovieProjectionDTO>()
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie)) // Map Movie
                .ForMember(dest => dest.ScreeningTime, opt => opt.MapFrom(src => src.ScreeningTime)) // Map ScreeningTime
                .ForMember(dest => dest.ScreenType, opt => opt.MapFrom(src => src.ScreenType.ToString())) // Map ScreenType to string
                .ForMember(dest => dest.CinemaHall, opt => opt.MapFrom(src => src.CinemaHall)) // Map CinemaHall
                .ForMember(dest => dest.NormalizedMovieTitle, opt => opt.MapFrom(src => src.MovieNormalizedTitle)) // Map MovieNormalizedTitle
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price)) // Map Price
                .ForMember(dest => dest.OccupiedSeats, opt => opt.Ignore()) // Map OccupiedSeats
                .ForMember(dest => dest.AvailableSeats, opt => opt.Ignore()); // Map AvailableSeats

            // Mapping from GetMovieProjectionDTO to MovieProjection
            // Ignoring Movie mapping as the GetMovieDTO details are not provided in the DTO
            CreateMap<GetMovieProjectionDTO, MovieProjection>()
                .ForMember(dest => dest.Movie, opt => opt.Ignore()) // Ignore Movie
                .ForMember(dest => dest.ScreeningTime, opt => opt.MapFrom(src => src.ScreeningTime)) // Map ScreeningTime
                .ForMember(dest => dest.ScreenType, opt => opt.MapFrom(src => Enum.Parse<ScreenType>(src.ScreenType ?? "2D"))) // Default to 2D
                .ForMember(dest => dest.CinemaHall, opt => opt.MapFrom(src => src.CinemaHall)) // Map CinemaHall
                .ForMember(dest => dest.MovieNormalizedTitle, opt => opt.MapFrom(src => src.NormalizedMovieTitle)) // Map NormalizedMovieTitle
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price)) // Map Price
                .ForMember(dest => dest.MovieProjectionId, opt => opt.Ignore()); // Ignore MovieProjectionId
        }
    }
}
