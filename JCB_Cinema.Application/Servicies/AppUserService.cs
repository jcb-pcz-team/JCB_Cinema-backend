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

    public class AppUserService : ServiceBase, IAppUserService
    {
        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService) : base(unitOfWork, mapper, userManager, userContextService) { }


        public async Task<AppUserDTO?> GetAppUserAsync(string id)
        {
            var currentUserName = _userContextService.GetUserName();

            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException("Brak uprawnień do wykonania tej operacji.");

            var user = await _unitOfWork.DbContext().AppUsers.FirstOrDefaultAsync(u => u.UserName == currentUserName);

            if (user == null)
                throw new KeyNotFoundException("Użytkownik nie został znaleziony.");
            
            if (user.Id != id)
                throw new UnauthorizedAccessException("Nie możesz edytować danych innego użytkownika.");

            return _mapper.Map<AppUserDTO>(user);
        }


        public async Task PutAppUserAsync(RequestAppUser appUserRequest)
        {
            var currentUserName = _userContextService.GetUserName();

            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException("Brak uprawnień do wykonania tej operacji.");

            var user = await _unitOfWork.DbContext().AppUsers.FirstOrDefaultAsync(u => u.UserName == currentUserName);

            if (user == null)
                throw new KeyNotFoundException("Użytkownik nie został znaleziony.");

            if (user.UserName != currentUserName)
                throw new UnauthorizedAccessException("Nie możesz edytować danych innego użytkownika.");

            user.FirstName = appUserRequest.FirstName;
            user.LastName = appUserRequest.LastName;
            user.Street = appUserRequest.Street;
            user.HouseNumber = appUserRequest.HouseNumber;
            user.Town = appUserRequest.Town;
            user.PhoneNumber = appUserRequest.PhoneNumber;
            user.Email = appUserRequest.Email;
            
            _unitOfWork.DbContext().AppUsers.Update(user);

            await _unitOfWork.SaveToDatabaseAsync();
        }
    }
}
