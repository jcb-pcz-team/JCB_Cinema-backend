using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IBookingTicketService
    {
        Task<IList<BookingTicketDTO>?> GetUserBookingHistoryAsync(QueryAppUser requestAppUser);
        Task EditBookingTicket(UpdateBookingTicketRequest request);
        Task DeleteBookingTicket(int id);
        Task<int> GetBookingTicketsCount(QueryBookingTicket request);
    }
}
