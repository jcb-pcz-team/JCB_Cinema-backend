using AutoMapper;
using JCB_Cinema.Application.DTOs.Auth;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Infrastructure.Data.Interfaces;
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
        private readonly IUserContextService _userContextService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IConfiguration configuration, IJwtService jwtService, UserManager<AppUser> userManager, IUserRoleService roleService, IUserContextService userContextService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _jwtService = jwtService;
            _userManager = userManager;
            _userRoleService = roleService;
            _userContextService = userContextService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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

        public async Task ChangePassword(ChangeUserPassword changeUserPasswd)
        {
            // Get current user from context
            var currentUserName = _userContextService.GetUserName();

            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException();

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null)
                throw new UnauthorizedAccessException();

            IdentityResult? updateResult = null;

            // if admin
            if (await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                var user = await _userContextService.GetAppUser(changeUserPasswd.Email, changeUserPasswd.Login);
                if (user != null)
                {
                    updateResult = await _userManager.ChangePasswordAsync(user, changeUserPasswd.OldPassword, changeUserPasswd.NewPassword);
                    if (updateResult == null || !updateResult.Succeeded)
                        throw new InvalidOperationException("New or current password is invalid.");
                    return;
                }
            }

            updateResult = await _userManager.ChangePasswordAsync(currentUser, changeUserPasswd.OldPassword, changeUserPasswd.NewPassword);

            if (updateResult == null || !updateResult.Succeeded)
                throw new InvalidOperationException("New or current password is invalid.");
        }
    }
}
