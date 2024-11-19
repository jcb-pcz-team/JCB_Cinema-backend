namespace JCB_Cinema.Infrastructure.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public ITRepository<T> Repository<T>() where T : class;
        public Task<int> SaveToDatabaseAsync();
    }
}
