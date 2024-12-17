using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Tools;

namespace JCB_Cinema.Application.Mappers
{
    public class MovieProjectionServiceProfile : Profile
    {
        public MovieProjectionServiceProfile()
        {
            CreateMap<MovieProjection, GetMovieProjectionDTO>()
                .ForMember(dest => dest.MovieProjectionId, opt => opt.MapFrom(src => src.MovieProjectionId))
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie))
                .ForMember(dest => dest.ScreeningTime, opt => opt.MapFrom(src => src.ScreeningTime))
                .ForMember(dest => dest.ScreenType, opt => opt.MapFrom(src => src.ScreenType.GetDescription()))
                .ForMember(dest => dest.CinemaHall, opt => opt.MapFrom(src => src.CinemaHall))
                .ForMember(dest => dest.NormalizedMovieTitle, opt => opt.MapFrom(src => src.MovieNormalizedTitle))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.OccupiedSeats, opt => opt.MapFrom(src => src.OccupiedSeats))
                .ForMember(dest => dest.AvailableSeats, opt => opt.MapFrom(src => src.AvailableSeats));

            CreateMap<GetMovieProjectionDTO, MovieProjection>()
                .ForMember(dest => dest.Movie, opt => opt.Ignore())
                .ForMember(dest => dest.ScreeningTime, opt => opt.MapFrom(src => src.ScreeningTime))
                .ForMember(dest => dest.ScreenType, opt => opt.MapFrom(src => EnumExtensions.GetValueFromDescription<ScreenType>(src.ScreenType ?? ScreenType.TwoD.GetDescription())))
                .ForMember(dest => dest.CinemaHall, opt => opt.MapFrom(src => src.CinemaHall))
                .ForMember(dest => dest.MovieNormalizedTitle, opt => opt.MapFrom(src => src.NormalizedMovieTitle))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.MovieProjectionId, opt => opt.Ignore())
                .ForMember(dest => dest.OccupiedSeats, opt => opt.Ignore())
                .ForMember(dest => dest.AvailableSeats, opt => opt.Ignore());

            CreateMap<AddMovieProjectionRequest, MovieProjection>()
                .ForMember(dest => dest.ScreeningTime, opt => opt.MapFrom(src => src.ScreeningTime))
                .ForMember(dest => dest.ScreenType, opt => opt.MapFrom(src => src.ScreenType))
                .ForMember(dest => dest.CinemaHall, opt => opt.Ignore())
                .ForMember(dest => dest.MovieNormalizedTitle, opt => opt.Ignore());

            CreateMap<UpdateMovieProjectionRequest, MovieProjection>();
        }
    }
}