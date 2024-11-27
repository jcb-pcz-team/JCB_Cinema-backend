using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Queries;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IBookingTicketService
    {
        public Task<IList<BookingTicketDTO>?> GetUserBookingHistoryAsync(QueryAppUser requestAppUser);
    }
}
