using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Application.Services;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Application.Servicies
{
    /// <summary>
    /// Service class that provides operations related to booking tickets, including editing,
    /// deleting tickets, retrieving booking history, and counting the number of booking tickets.
    /// </summary>
    public class BookingTicketService : ServiceBase, IBookingTicketService
    {
        private readonly IMovieProjectionService _movieProjectionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingTicketService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for mapping data objects.</param>
        /// <param name="userManager">The user manager for managing user-related operations.</param>
        /// <param name="userContextService">The service that provides the current user's context.</param>
        /// <param name="movieProjectionService">The movie projection service for handling seat reservations.</param>
        public BookingTicketService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService, IMovieProjectionService movieProjectionService)
            : base(unitOfWork, mapper, userManager, userContextService)
        {
            _movieProjectionService = movieProjectionService;
        }

        /// <summary>
        /// Deletes a booking ticket. If the user is an admin, the ticket can be deleted by anyone.
        /// If the user is a regular user, only their own tickets can be deleted.
        /// </summary>
        /// <param name="id">The ID of the booking ticket to be deleted.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the user is not authorized to delete the ticket.</exception>
        /// <exception cref="NullReferenceException">Thrown if the booking ticket does not exist.</exception>
        public async Task DeleteBookingTicket(int id)
        {
            var currentUserName = _userContextService.GetUserName();
            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException();

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null)
                throw new UnauthorizedAccessException();

            if (await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                // Admin can delete any booking ticket
                await _unitOfWork.Repository<BookingTicket>().DeleteAsync(id);
            }
            else
            {
                // User can only delete their own booking ticket
                BookingTicket? bt = _unitOfWork.Repository<BookingTicket>().Queryable().FirstOrDefault(b => b.BookingTicketId == id);
                if (bt == null)
                    throw new NullReferenceException();

                if (bt.AppUserId == currentUser.Id)
                    await _unitOfWork.Repository<BookingTicket>().DeleteAsync(id);
                else
                    throw new UnauthorizedAccessException();
            }
        }

        /// <summary>
        /// Edits an existing booking ticket. If the user is an admin, any booking ticket can be edited.
        /// If the user is a regular user, only their own booking tickets can be edited.
        /// </summary>
        /// <param name="request">The request containing the details for the booking ticket update.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the user is not authorized to edit the ticket.</exception>
        /// <exception cref="NullReferenceException">Thrown if the booking ticket to be edited does not exist.</exception>
        public async Task EditBookingTicket(UpdateBookingTicketRequest request)
        {
            var currentUserName = _userContextService.GetUserName();
            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException();

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null)
                throw new UnauthorizedAccessException();

            BookingTicket? bookingTicket = await _unitOfWork.Repository<BookingTicket>().Queryable().FirstOrDefaultAsync(bt => bt.BookingTicketId == request.BookingTicketId);
            if (bookingTicket == null)
                throw new NullReferenceException();

            if (request.MovieProjectionId == 0)
                request.MovieProjectionId = bookingTicket.MovieProjectionId;
            if (request.SeatId == 0)
                request.SeatId = bookingTicket.SeatId;

            if (await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                // Admin can edit any booking ticket
                BookingTicket updatedBookingTicket = _mapper.Map(request, bookingTicket);
                await _unitOfWork.Repository<BookingTicket>().UpdateAsync(updatedBookingTicket);
            }
            else
            {
                // User can only edit their own booking ticket
                if (bookingTicket.AppUserId == currentUser.Id)
                {
                    BookingTicket updatedBookingTicket = _mapper.Map(request, bookingTicket);
                    await _unitOfWork.Repository<BookingTicket>().UpdateAsync(updatedBookingTicket);
                }
                else
                    throw new UnauthorizedAccessException();
            }
        }

        /// <summary>
        /// Retrieves the booking history of a user. Admin users can retrieve the history of any user, 
        /// while regular users can only view their own booking history.
        /// </summary>
        /// <param name="requestAppUser">The request containing the login or email of the user whose booking history is to be retrieved.</param>
        /// <returns>A list of <see cref="BookingTicketDTO"/> representing the user's booking history, or null if no history is found.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the user is not authorized to retrieve the booking history.</exception>
        public async Task<IList<BookingTicketDTO>?> GetUserBookingHistoryAsync(QueryAppUser requestAppUser)
        {
            var currentUserName = _userContextService.GetUserName();
            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException("Brak uprawnień do wykonania tej operacji.");

            var include = _unitOfWork.Repository<AppUser>()
                .Queryable()
                .Include(a => a.BookingTickets)
                    .ThenInclude(a => a.Seat)
                .Include(a => a.BookingTickets)
                    .ThenInclude(a => a.MovieProjection)
                    .ThenInclude(a => a.Movie)
                .Include(a => a.BookingTickets)
                    .ThenInclude(a => a.MovieProjection)
                        .ThenInclude(a => a.CinemaHall);

            var currentUser = await _userManager.FindByNameAsync(currentUserName);

            if (currentUser == null)
                throw new UnauthorizedAccessException("Brak uprawnień do wykonania tej operacji.");

            if (await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                // Admin can retrieve booking history of any user
                AppUser? user = null;

                if (!string.IsNullOrEmpty(requestAppUser.Login))
                {
                    user = await include.FirstOrDefaultAsync(a => a.NormalizedUserName == _userManager.NormalizeName(requestAppUser.Login));
                    return user == null ? null : _mapper.Map<IList<BookingTicketDTO>?>(user.BookingTickets);
                }
                else if (!string.IsNullOrEmpty(requestAppUser.Email))
                {
                    user = await include.FirstOrDefaultAsync(a => a.NormalizedEmail == _userManager.NormalizeEmail(requestAppUser.Email));
                    return user == null ? null : _mapper.Map<IList<BookingTicketDTO>?>(user.BookingTickets);
                }
            }

            currentUser = await include.FirstOrDefaultAsync(a => a.NormalizedUserName == _userManager.NormalizeName(currentUserName));
            return _mapper.Map<IList<BookingTicketDTO>?>(currentUser?.BookingTickets);
        }

        /// <summary>
        /// Retrieves the count of booking tickets based on the specified query parameters, such as date range.
        /// </summary>
        /// <param name="request">The query containing the filters to be applied when counting the tickets.</param>
        /// <returns>The count of booking tickets that match the specified criteria.</returns>
        public async Task<int> GetBookingTicketsCount(QueryBookingTicket request)
        {
            var query = _unitOfWork.Repository<BookingTicket>().Queryable();
            if (request.MovieProjectionDateFrom.HasValue)
            {
                query = query.Where(a => a.MovieProjection.ScreeningTime >= request.MovieProjectionDateFrom);
            }
            if (request.MovieProjectionDateTo.HasValue)
            {
                query = query.Where(a => a.MovieProjection.ScreeningTime <= request.MovieProjectionDateTo);
            }

            return await query.CountAsync();
        }

        /// <summary>
        /// Adds a new booking ticket. Verifies if the seat is available, the movie projection exists, and is able to reserve the seat.
        /// </summary>
        /// <param name="userName">The username for the user making the booking (optional, for admin).</param>
        /// <param name="request">The request containing the booking ticket details.</param>
        /// <returns>The ID of the newly created booking ticket.</returns>
        /// <exception cref="NullReferenceException">Thrown if the user cannot be found or the price is not defined.</exception>
        /// <exception cref="Exception">Thrown if the seat is already reserved or the movie projection is invalid.</exception>
        public async Task<string> AddBookingTicket(string? userName, AddBookingTicketRequest request)
        {
            var user = await _userContextService.GetAppUser();
            if (await _userManager.IsInRoleAsync(user, "Admin") && !string.IsNullOrEmpty(userName))
            {
                user = await _userManager.FindByNameAsync(userName);
            }
            if (user == null)
            {
                throw new NullReferenceException("Could not find user.");
            }

            bool isProjectionExistsAndAbleToReserve = await _unitOfWork.Repository<MovieProjection>().Queryable()
                .AnyAsync(a => a.MovieProjectionId == request.MovieProjectionId
                    && a.ScreeningTime >= DateTime.Now.AddMinutes(-30));
            if (!isProjectionExistsAndAbleToReserve)
            {
                throw new TimeoutException("Unable to book a ticket. Projection does not exists or is not able to reserve.");
            }

            bool isSeatReserved = await _movieProjectionService.IsSeatReserved(request.MovieProjectionId, request.SeatId);
            if (isSeatReserved)
            {
                throw new Exception("Seat is not available.");
            }

            var booking = _mapper.Map<BookingTicket>(request);

            var price = await _unitOfWork.Repository<MovieProjection>().Queryable()
                .Where(a => a.MovieProjectionId == request.MovieProjectionId)
                .AsNoTracking()
                .Include(a => a.Price)
                .Select(a => a.Price)
                .FirstOrDefaultAsync();

            if (price == null)
            {
                throw new NullReferenceException("Price is not defined.");
            }

            booking.Price = price.AmountInCents;
            booking.AppUserId = user.Id;

            await _unitOfWork.Repository<BookingTicket>().AddAsync(booking);

            return booking.BookingTicketId.ToString();
        }

        /// <summary>
        /// Retrieves the details of a specific booking ticket.
        /// </summary>
        /// <param name="bookingId">The ID of the booking ticket.</param>
        /// <param name="userName">The username (optional, for admin).</param>
        /// <returns>The details of the booking ticket, or null if not found.</returns>
        public async Task<BookingTicketDTO?> GetBookingDetails(int bookingId, string? userName)
        {
            var user = await _userContextService.GetAppUser();
            bool clientIsAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (clientIsAdmin && !string.IsNullOrEmpty(userName))
            {
                user = await _userManager.FindByNameAsync(userName);
            }
            if (user == null)
            {
                throw new NullReferenceException("Could not find user.");
            }

            var query = await _unitOfWork.Repository<BookingTicket>()
                .Queryable()
                    .Include(a => a.Seat)
                    .Include(a => a.MovieProjection)
                        .ThenInclude(a => a.Movie)
                    .Include(a => a.MovieProjection)
                        .ThenInclude(a => a.CinemaHall)
                .FirstOrDefaultAsync(a => a.BookingTicketId == bookingId && a.AppUserId == user.Id);

            return _mapper.Map<BookingTicketDTO?>(query);
        }

        /// <summary>
        /// Confirms a booking ticket. Only admin or the ticket owner can confirm a booking.
        /// </summary>
        /// <param name="bookingId">The ID of the booking ticket to confirm.</param>
        /// <exception cref="NullReferenceException">Thrown if the booking is expired or does not exist.</exception>
        public async Task ConfirmBooking(int bookingId)
        {
            var user = await _userContextService.GetAppUser();
            var query = _unitOfWork.Repository<BookingTicket>().Queryable()
                .Where(a => !a.IsConfirmed && a.ExpiresAt >= DateTime.Now);
            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                query = query.Where(a => a.AppUserId == user.Id);
            }

            var entity = await query.FirstOrDefaultAsync(a => a.BookingTicketId == bookingId);
            if (entity == null)
            {
                throw new NullReferenceException("Booking is expired or does not exists.");
            }

            entity.ConfirmBooking();

            await _unitOfWork.Repository<BookingTicket>().UpdateAsync(entity);
        }
    }
}