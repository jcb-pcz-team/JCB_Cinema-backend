using AutoMapper;
using JCB_Cinema.Application.DTOs.Auth;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
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
        private readonly IMapper _mapper;

        public UserService(IConfiguration configuration, IJwtService jwtService, UserManager<AppUser> userManager, IUserRoleService roleService, IUserContextService userContextService, IMapper mapper)
        {
            _configuration = configuration;
            _jwtService = jwtService;
            _userManager = userManager;
            _userRoleService = roleService;
            _userContextService = userContextService;
            _mapper = mapper;
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

            // if user
            // user wants to change his own password
            if (await _userManager.IsInRoleAsync(currentUser, "User"))
            {
                changeUserPasswd.Email = currentUser.Email;
                changeUserPasswd.Login = currentUser.UserName;
                _mapper.Map(changeUserPasswd, currentUser);

                updateResult = await _userManager.UpdateAsync(currentUser);
            }

            // if admin
            if (await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                // admin wants to change password of some user by his Email
                if (!string.IsNullOrEmpty(changeUserPasswd.Email))
                {
                    var someUser = await _userManager.FindByEmailAsync(changeUserPasswd.Email);

                    if (someUser == null)
                        throw new InvalidOperationException();
                    changeUserPasswd.Login = someUser.UserName;
                    _mapper.Map(changeUserPasswd, someUser);

                    updateResult = await _userManager.UpdateAsync(someUser);
                }
                // admin wants to change password of some user by his Login
                else if (!string.IsNullOrEmpty(changeUserPasswd.Login))
                {
                    var someUser = await _userManager.FindByNameAsync(changeUserPasswd.Login);

                    if (someUser == null)
                        throw new InvalidOperationException();
                    changeUserPasswd.Email = someUser.Email;
                    _mapper.Map(changeUserPasswd, someUser);

                    updateResult = await _userManager.UpdateAsync(someUser);
                }
                else
                {
                    // admin wants to change his own password
                    changeUserPasswd.Email = currentUser.Email;
                    changeUserPasswd.Login = currentUser.UserName;
                    _mapper.Map(changeUserPasswd, currentUser);

                    updateResult = await _userManager.UpdateAsync(currentUser);
                }

            }
            if (updateResult == null || !updateResult.Succeeded)
                throw new InvalidOperationException();
        }
    }
}
