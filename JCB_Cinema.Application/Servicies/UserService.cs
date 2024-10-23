using JCB_Cinema.Application.DTOs.Auth;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
using JCB_Cinema.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace JCB_Cinema.Application.Servicies
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRoleService _userRoleService;

        public UserService(IConfiguration configuration, IJwtService jwtService, UserManager<AppUser> userManager, IUserRoleService roleService)
        {
            _configuration = configuration;
            _jwtService = jwtService;
            _userManager = userManager;
            _userRoleService = roleService;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegistrationModel model)
        {
            var existingUser = await _userManager.FindByNameAsync(model.UserName);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User already exists." });
            }

            var newUser = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);
            return result;
        }

        public async Task<string> Login(LoginModel model)
        {
            AppUser? user = null;

            // Find user by username or email
            if (!string.IsNullOrEmpty(model.UserName))
            {
                user = await _userManager.FindByNameAsync(model.UserName);
            }
            else if (!string.IsNullOrEmpty(model.Email))
            {
                user = await _userManager.FindByEmailAsync(model.Email);
            }

            // If user is not found or password is incorrect, throw an exception
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new UnauthorizedAccessException("403");
            }

            // Generate JWT token for the authenticated user
            var token = await _jwtService.GenerateJwtAsync(user);

            // Return the generated JWT token
            return _jwtService.WriteToken(token);
        }
        public async Task<string> AssignUserToRole(AssignUserToRoleRequest roleRequest)
        {
            var user = await _userManager.FindByNameAsync(roleRequest.UserName);
            if (user == null)
                throw new InvalidOperationException("Could not find user");

            await _userRoleService.AssignRoleToUserAsync(user, roleRequest.Role);

            var token = await _jwtService.GenerateJwtAsync(user);
            return _jwtService.WriteToken(token);
        }
    }
}
