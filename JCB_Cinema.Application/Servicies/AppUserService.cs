using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JCB_Cinema.Application.Servicies
{

    public class AppUserService : ServiceBase, IAppUserService
    {
        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService) : base(unitOfWork, mapper, userManager, userContextService) { }


        public async Task<AppUserDTO?> GetAppUserAsync(RequestAppUser request)
        {
            var currentUserName = _userContextService.GetUserName();
            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException("Brak uprawnień do wykonania tej operacji.");

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null)
                throw new UnauthorizedAccessException("Brak uprawnień do wykonania tej operacji.");

            if (!await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                // the user is not admin, so he cant get other users details
                return _mapper.Map<AppUserDTO>(currentUser);
            }

            AppUser? user = null;
            if (!string.IsNullOrEmpty(request.Login))
            {
                user = await _userManager.FindByNameAsync(request.Login);
            }
            else if (!string.IsNullOrEmpty(request.Email))
            {
                user = await _userManager.FindByNameAsync(request.Email);
            }
            else
            {
                // if every RequestAppUser property is null, thats mean Admin wants own User details
                return _mapper.Map<AppUserDTO>(currentUser);
            }

            return _mapper.Map<AppUserDTO>(user);
        }


        public async Task PutAppUserAsync(PutAppUserDetails appUserRequest)
        {
            var currentUserName = _userContextService.GetUserName();

            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException();

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null)
                throw new UnauthorizedAccessException();

            _mapper.Map(appUserRequest, currentUser);
            var updateResult = await _userManager.UpdateAsync(currentUser);

            if (!updateResult.Succeeded)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
