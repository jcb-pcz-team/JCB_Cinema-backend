using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Infrastructure.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public ITRepository<T> Repository<T>() where T : EntityBase;
        public Task<int> SaveToDatabaseAsync();
        public CinemaDbContext DbContext();
    }
}
