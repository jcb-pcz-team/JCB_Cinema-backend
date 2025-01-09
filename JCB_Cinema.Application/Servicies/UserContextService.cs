using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace JCB_Cinema.Application.Servicies
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public UserContextService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public string? GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        }

        public async Task<AppUser?> GetAppUser(string? email, string? userName)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                return await _userManager.FindByEmailAsync(email);
            }
            else if (!string.IsNullOrWhiteSpace(userName))
            {
                return await _userManager.FindByNameAsync(userName);
            }
            return null;
        }

        public async Task<AppUser> GetAppUser()
        {
            var userName = GetUserName();
            if (userName == null)
            {
                throw new UnauthorizedAccessException();
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            return user;
        }
    }
}
