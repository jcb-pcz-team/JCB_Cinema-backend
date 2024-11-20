using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Mappers
{
    public class BookingTicketServiceProfile : Profile
    {
        public BookingTicketServiceProfile()
        {
            CreateMap<BookingTicket, BookingTicketDTO>()
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.MovieProjection.Movie.Title))
                .ForMember(dest => dest.ScreenType, opt => opt.MapFrom(src => src.MovieProjection.ScreenType))
                .ForMember(dest => dest.ScreenignTime, opt => opt.MapFrom(src => src.MovieProjection.ScreeningTime))
                .ForMember(dest => dest.CienemaHall, opt => opt.MapFrom(src => src.MovieProjection.CinemaHall))
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.Number))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.BookingURL, opt => opt.MapFrom(_ => ""));
        }
    }
}
