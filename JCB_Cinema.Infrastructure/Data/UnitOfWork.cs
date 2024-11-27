using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using JCB_Cinema.Infrastructure.Data.Repositories;

namespace JCB_Cinema.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CinemaDbContext _dbContext;
        private readonly IUserContextService _userContextService;

        public UnitOfWork(CinemaDbContext dbContext, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        private readonly Dictionary<Type, object> _repositories = new();


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

        public async Task<int> SaveToDatabaseAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
