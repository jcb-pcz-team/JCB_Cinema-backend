using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Application.Mappers
{
    public class CinemaHallServiceProfile : Profile
    {
        public CinemaHallServiceProfile()
        {
            CreateMap<CinemaHall, GetCinemaHallDTO>()
                .ForMember(dest => dest.CinemaHallId, opt => opt.MapFrom(src => src.CinemaHallId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<GetCinemaHallDTO, CinemaHall>()
                .ForMember(dest => dest.CinemaHallId, opt => opt.MapFrom(src => src.CinemaHallId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Seats, opt => opt.Ignore()); // Ignore Seats since it's not part of the DTO

            // Mapping Photo <-> GetPosterDTO
            CreateMap<Photo, GetPosterDTO>()
                .ForMember(dest => dest.PosterId, opt => opt.MapFrom(src => src.Id));

            CreateMap<GetPosterDTO, Photo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PosterId))
                .ForMember(dest => dest.Bytes, opt => opt.Ignore()) // Ignore Bytes in mapping since it's not part of the DTO
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.FileExtension, opt => opt.Ignore())
                .ForMember(dest => dest.Size, opt => opt.Ignore());

            // Mapping MovieProjection <-> GetMovieProjectionDTO
            CreateMap<MovieProjection, GetMovieProjectionDTO>()
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie))
                .ForMember(dest => dest.ScreeningTime, opt => opt.MapFrom(src => src.ScreeningTime))
                .ForMember(dest => dest.ScreenType, opt => opt.MapFrom(src => src.ScreenType.ToString()))
                .ForMember(dest => dest.CinemaHall, opt => opt.MapFrom(src => src.CinemaHall))
                .ForMember(dest => dest.NormalizedMovieTitle, opt => opt.MapFrom(src => src.MovieNormalizedTitle))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.OccupiedSeats, opt => opt.MapFrom(src => src.OccupiedSeats))
                .ForMember(dest => dest.AvailableSeats, opt => opt.MapFrom(src => src.AvailableSeats));

            CreateMap<GetMovieProjectionDTO, MovieProjection>()
                .ForMember(dest => dest.Movie, opt => opt.Ignore()) // Ignore Movie mapping for now as we don't have GetMovieDTO details
                .ForMember(dest => dest.ScreeningTime, opt => opt.MapFrom(src => src.ScreeningTime))
                .ForMember(dest => dest.ScreenType, opt => opt.MapFrom(src => Enum.Parse<ScreenType>(src.ScreenType ?? "2D"))) // Default to 2D
                .ForMember(dest => dest.CinemaHall, opt => opt.MapFrom(src => src.CinemaHall))
                .ForMember(dest => dest.MovieNormalizedTitle, opt => opt.MapFrom(src => src.NormalizedMovieTitle))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.MovieProjectionId, opt => opt.Ignore()) // Ignore MovieProjectionId
                .ForMember(dest => dest.OccupiedSeats, opt => opt.Ignore()) // Handled internally
                .ForMember(dest => dest.AvailableSeats, opt => opt.Ignore()); // Handled internally
        }
    }
}