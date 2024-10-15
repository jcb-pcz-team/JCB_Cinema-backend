namespace JCB_Cinema.Application.Interfaces.Repositories
{
    public interface ITRepository<T>
    {
        public IQueryable<T> Queryable();
        public Task<T?> GetByIdAsync(int id);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
