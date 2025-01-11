using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;

namespace JCB_Cinema.Application.Interfaces
{
    /// <summary>
    /// Interface for the service responsible for managing booking ticket operations.
    /// </summary>
    public interface IBookingTicketService
    {

#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves the booking history for a specific user based on the provided query.
        /// </summary>
        /// <param name="requestAppUser">
        /// A <see cref="QueryAppUser"/> containing the details of the user for whom the booking history is requested.
        /// </param>
        /// <returns>
        /// A <see cref="Task{IList{BookingTicketDTO}?}"/> representing the asynchronous operation. 
        /// The result contains a list of <see cref="BookingTicketDTO"/> representing the user's booking history or null if no bookings are found.
        /// </returns>
        Task<IList<BookingTicketDTO>?> GetUserBookingHistoryAsync(QueryAppUser requestAppUser);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

        /// <summary>
        /// Asynchronously edits an existing booking ticket based on the provided update request.
        /// </summary>
        /// <param name="request">
        /// A <see cref="UpdateBookingTicketRequest"/> containing the details to update the booking ticket.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous update operation.
        /// </returns>
        Task EditBookingTicket(UpdateBookingTicketRequest request);

        /// <summary>
        /// Asynchronously deletes a booking ticket based on the specified booking ticket ID.
        /// </summary>
        /// <param name="id">
        /// An <see cref="int"/> representing the unique identifier of the booking ticket to be deleted.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous delete operation.
        /// </returns>
        Task DeleteBookingTicket(int id);


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves the total number of booking tickets matching the provided query criteria.
        /// </summary>
        /// <param name="request">
        /// A <see cref="QueryBookingTicket"/> containing the search criteria for counting the booking tickets.
        /// </param>
        /// <returns>
        /// A <see cref="Task{int}"/> representing the asynchronous operation. The result contains the number of booking tickets matching the criteria.
        /// </returns>
        Task<int> GetBookingTicketsCount(QueryBookingTicket request);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref
    }
}
