using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
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


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously adds a new booking ticket for a user based on the provided request.
        /// </summary>
        /// <param name="userName">
        /// An optional <see cref="string"/> representing the username of the user making the booking.
        /// If null, the current user's context is used.
        /// </param>
        /// <param name="request">
        /// A <see cref="AddBookingTicketRequest"/> containing the details of the booking ticket to be added.
        /// </param>
        /// <returns>
        /// A <see cref="Task{string}"/> representing the asynchronous operation. The result contains the ID of the newly created booking ticket.
        /// </returns>
        Task<string> AddBookingTicket(string? userName, AddBookingTicketRequest request);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves the details of a specific booking ticket based on the booking ID and optional username.
        /// </summary>
        /// <param name="bookingId">
        /// An <see cref="int"/> representing the unique identifier of the booking ticket whose details are to be retrieved.
        /// </param>
        /// <param name="userName">
        /// An optional <see cref="string"/> representing the username of the user making the request. 
        /// If null, the current user's context is used.
        /// </param>
        /// <returns>
        /// A <see cref="Task{BookingTicketDTO?}"/> representing the asynchronous operation. The result contains a <see cref="BookingTicketDTO"/>
        /// representing the details of the booking ticket or null if no such booking ticket is found.
        /// </returns>
        Task<BookingTicketDTO?> GetBookingDetails(int bookingId, string? userName);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

        /// <summary>
        /// Asynchronously confirms a booking ticket based on the provided booking ID.
        /// </summary>
        /// <param name="bookingId">
        /// An <see cref="int"/> representing the unique identifier of the booking ticket to be confirmed.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous confirmation operation.
        /// </returns>
        Task ConfirmBooking(int bookingId);
    }
}
