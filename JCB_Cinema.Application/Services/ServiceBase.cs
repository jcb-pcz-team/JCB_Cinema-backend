using AutoMapper;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JCB_Cinema.Application.Services
{
    /// <summary>
    /// Abstract base class for application services.
    /// </summary>
    /// <remarks>
    /// This class provides common functionality for managing entities, such as creation, updates, and deletions.
    /// It enforces user context validation and tracks changes to entities.
    /// </remarks>
    public abstract class ServiceBase
    {
        /// <summary>
        /// The unit of work for managing database operations.
        /// </summary>
        protected readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// The user context service for retrieving information about the current user.
        /// </summary>
        protected readonly IUserContextService _userContextService;

        /// <summary>
        /// The ASP.NET Core identity user manager for managing users.
        /// </summary>
        protected readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// The mapper for converting between domain entities and DTOs.
        /// </summary>
        protected readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for managing repository interactions.</param>
        /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
        /// <param name="userManager">The user manager for managing application users.</param>
        /// <param name="userContextService">The service for retrieving the current user's context.</param>
        public ServiceBase(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _userContextService = userContextService;
        }

        /// <summary>
        /// Fills the creation metadata for an entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity, which must inherit from <see cref="EntityBase"/>.</typeparam>
        /// <param name="entity">The entity to populate with creation metadata.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown when the current user's name cannot be retrieved.</exception>
        public void CreateFillEntity<T>(T entity)
            where T : EntityBase
        {
            var userName = _userContextService.GetUserName();
            if (userName == null)
            {
                throw new UnauthorizedAccessException();
            }

            entity.IsDeleted = false;
            entity.Created = DateTime.UtcNow;
            entity.Creator = userName;
        }

        /// <summary>
        /// Fills the update metadata for an entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity, which must inherit from <see cref="EntityBase"/>.</typeparam>
        /// <param name="entity">The entity to populate with update metadata.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown when the current user's name cannot be retrieved.</exception>
        public void UpdateFillEntity<T>(T entity)
            where T : EntityBase
        {
            var userName = _userContextService.GetUserName();
            if (userName == null)
            {
                throw new UnauthorizedAccessException();
            }

            entity.IsDeleted = false;
            entity.Modified = DateTime.UtcNow;
            entity.Modifier = userName;
        }

        /// <summary>
        /// Marks an entity as deleted and updates its deletion metadata.
        /// </summary>
        /// <typeparam name="T">The type of the entity, which must inherit from <see cref="EntityBase"/>.</typeparam>
        /// <param name="entity">The entity to mark as deleted.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown when the current user's name cannot be retrieved.</exception>
        public void Delete<T>(T entity)
            where T : EntityBase
        {
            var userName = _userContextService.GetUserName();
            if (userName == null)
            {
                throw new UnauthorizedAccessException();
            }

            entity.IsDeleted = true;
            entity.Modified = DateTime.UtcNow;
            entity.Modifier = userName;
        }
    }
}
