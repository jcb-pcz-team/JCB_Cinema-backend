using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using JCB_Cinema.Infrastructure.Data.Repositories;

namespace JCB_Cinema.Infrastructure.Data
{
    /// <summary>
    /// Represents a unit of work pattern for managing transactions and repositories.
    /// Implements <see cref="IUnitOfWork"/>.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CinemaDbContext _dbContext;
        private readonly IUserContextService _userContextService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="dbContext">The <see cref="CinemaDbContext"/> for accessing the database.</param>
        /// <param name="userContextService">The <see cref="IUserContextService"/> for retrieving user-related information.</param>
        public UnitOfWork(CinemaDbContext dbContext, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        /// <summary>
        /// A dictionary that holds instances of repositories, indexed by their entity types.
        /// </summary>
        private readonly Dictionary<Type, object> _repositories = new();

        /// <summary>
        /// Gets the repository for a specified entity type.
        /// If the repository does not exist, a new one will be created.
        /// </summary>
        /// <typeparam name="T">The entity type for which the repository is needed.</typeparam>
        /// <returns>An instance of <see cref="ITRepository{T}"/> for the specified entity type.</returns>
        public ITRepository<T> Repository<T>()
            where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repositoryType = typeof(TRepository<>);
                _repositories.Add(typeof(T), Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T))!, _dbContext, _userContextService)!);
            }
            return (ITRepository<T>)_repositories[typeof(T)];
        }

        /// <summary>
        /// Saves all changes made in the context to the database asynchronously.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public async Task<int> SaveToDatabaseAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Disposes the resources used by the <see cref="UnitOfWork"/> class.
        /// </summary>
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
