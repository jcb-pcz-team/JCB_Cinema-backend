using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Application.Servicies
{
    public class BookingTicketService : ServiceBase, IBookingTicketService
    {
        public BookingTicketService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService) : base(unitOfWork, mapper, userManager, userContextService)
        {
        }

        public async Task<IList<BookingTicketDTO>?> GetUserBookingHistoryAsync(RequestAppUser requestAppUser)
        {
            var currentUserName = _userContextService.GetUserName();
            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException("Brak uprawnień do wykonania tej operacji.");

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null)
                throw new UnauthorizedAccessException("Brak uprawnień do wykonania tej operacji.");

            if (await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                // admin wants to get data of some user

                AppUser? user = null;

                if (!string.IsNullOrEmpty(requestAppUser.Login))
                {
                    user = await _userManager.FindByNameAsync(requestAppUser.Login);
                    return user == null ? null : _mapper.Map<IList<BookingTicketDTO>?>(user.BookingTickets);
                }
                else if (!string.IsNullOrEmpty(requestAppUser.Email))
                {
                    user = await _userManager.FindByEmailAsync(requestAppUser.Email);
                    return user == null ? null : _mapper.Map<IList<BookingTicketDTO>?>(user.BookingTickets);
                }
            }

            // user wants to get data about yourself
            return _mapper.Map<IList<BookingTicketDTO>?>(currentUser.BookingTickets);

        }
    }
}