using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using JCB_Cinema.Infrastructure.Data.Repositories;

namespace JCB_Cinema.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CinemaDbContext _dbContext;

        public UnitOfWork(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly Dictionary<Type, object> _repositories = new();


        public ITRepository<T> Repository<T>()
            where T : EntityBase
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repositoryType = typeof(TRepository<>);
                _repositories.Add(typeof(T), Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T))!, _dbContext)!);
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
