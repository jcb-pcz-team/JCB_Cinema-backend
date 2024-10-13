using JCB_Cinema.Application.Interfaces.Repositories;

namespace JCB_Cinema.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public ITRepository<T> Repository<T>() where T : class;
        public Task<int> SaveToDatabaseAsync();
    }
}
