﻿using AutoMapper;
using JCB_Cinema.Application.DTOs.Auth;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace JCB_Cinema.Application.Services
{
    /// <summary>
    /// Service for managing user-related operations including registration, login, role assignments, and password changes.
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Configuration service to access application settings.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Service for generating and validating JWT tokens.
        /// </summary>
        private readonly IJwtService _jwtService;

        /// <summary>
        /// User manager for managing user-related tasks.
        /// </summary>
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Service for assigning roles to users.
        /// </summary>
        private readonly IUserRoleService _userRoleService;

        /// <summary>
        /// Service to fetch the current user context.
        /// </summary>
        private readonly IUserContextService _userContextService;

        /// <summary>
        /// Unit of work for handling data transactions.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// AutoMapper instance for mapping DTOs and entities.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class with the provided dependencies.
        /// </summary>
        /// <param name="configuration">Configuration service for application settings.</param>
        /// <param name="jwtService">Service for generating and handling JWT tokens.</param>
        /// <param name="userManager">Manages user-related operations.</param>
        /// <param name="roleService">Service for assigning roles to users.</param>
        /// <param name="userContextService">Service for retrieving the current user's context.</param>
        /// <param name="mapper">AutoMapper instance for mapping between objects.</param>
        /// <param name="unitOfWork">Unit of work for handling data operations.</param>
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

        /// <summary>
        /// Registers a new user asynchronously.
        /// </summary>
        /// <param name="model">Registration model containing user details.</param>
        /// <returns>A task representing the asynchronous operation with the result of the registration process.</returns>
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

        /// <summary>
        /// Logs in a user by validating their credentials and returning a JWT token if successful.
        /// </summary>
        /// <param name="model">Login model containing user credentials (username/email and password).</param>
        /// <returns>A task representing the asynchronous operation with the generated JWT token.</returns>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the login credentials are incorrect or the user is not found.
        /// </exception>
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

        /// <summary>
        /// Assigns a role to a user and returns a new JWT token.
        /// </summary>
        /// <param name="roleRequest">Request model containing the user and role to assign.</param>
        /// <returns>A task representing the asynchronous operation with the updated JWT token.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the user is not found.
        /// </exception>
        public async Task<string> AssignUserToRole(AssignUserToRoleRequest roleRequest)
        {
            var user = await _userManager.FindByNameAsync(roleRequest.UserName);
            if (user == null)
                throw new InvalidOperationException("Could not find user");

            await _userRoleService.AssignRoleToUserAsync(user, roleRequest.Role);

            var token = await _jwtService.GenerateJwtAsync(user);
            return _jwtService.WriteToken(token);
        }

        /// <summary>
        /// Changes the current user's password or changes another user's password if the user is an admin.
        /// </summary>
        /// <param name="changeUserPasswd">Model containing the user's current password and the new password.</param>
        /// <returns>A task representing the asynchronous operation of the password change process.</returns>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown if the current user cannot be determined or if the password change fails.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the old or new password is invalid.
        /// </exception>
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
                var user = await _userContextService.GetAppUser(changeUserPasswd.Email, changeUserPasswd.UserName);
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