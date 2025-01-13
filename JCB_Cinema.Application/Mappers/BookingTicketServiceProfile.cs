using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Tools;

namespace JCB_Cinema.Application.Mappers
{
    /// <summary>
    /// AutoMapper profile class for configuring mappings between the <see cref="BookingTicket"/> entity 
    /// and the <see cref="BookingTicketDTO"/> used in the application.
    /// </summary>
    public class BookingTicketServiceProfile : Profile
    {
        /// <summary>
        /// Configures the object mappings for <see cref="BookingTicket"/> and related DTOs.
        /// </summary>
        public BookingTicketServiceProfile()
        {
            // Map from BookingTicket to BookingTicketDTO, mapping relevant properties from BookingTicket and related entities
            CreateMap<BookingTicket, BookingTicketDTO>()
                .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.BookingTicketId)) // Map BookingTicketId to BookingId
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.MovieProjection.Movie.Title)) // Map MovieProjection.Movie.Title to MovieTitle
                .ForMember(dest => dest.ScreenType, opt => opt.MapFrom(src => src.MovieProjection.ScreenType.GetDescription())) // Map ScreenType description
                .ForMember(dest => dest.ScreenignTime, opt => opt.MapFrom(src => src.MovieProjection.ScreeningTime)) // Map ScreeningTime
                .ForMember(dest => dest.CienemaHall, opt => opt.MapFrom(src => src.MovieProjection.CinemaHall.Name)) // Map CinemaHall.Name to CienemaHall
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.Number)) // Map Seat.Number to SeatNumber
                .ForMember(dest => dest.MovieProjectionId, opt => opt.MapFrom(src => src.MovieProjectionId)); // Map MovieProjectionId

            // Map from UpdateBookingTicketRequest to BookingTicket
            CreateMap<UpdateBookingTicketRequest, BookingTicket>();

            CreateMap<AddBookingTicketRequest, BookingTicket>()
                .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => DateTime.Now.AddMinutes(2)))
                .ForMember(dest => dest.IsConfirmed, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.ReservationTime, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
