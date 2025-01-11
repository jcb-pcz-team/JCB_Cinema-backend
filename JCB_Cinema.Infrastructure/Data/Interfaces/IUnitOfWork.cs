namespace JCB_Cinema.Infrastructure.Data.Interfaces
{
    /// <summary>
    /// Defines a unit of work for managing database transactions and repositories.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Retrieves a repository instance for the specified entity type.
        /// </summary>
        /// <typeparam name="T">The type of entity the repository will manage. Must be a class.</typeparam>
        /// <returns>An instance of <see cref="ITRepository{T}"/> for the specified entity type.</returns>
        ITRepository<T> Repository<T>() where T : class;

        /// <summary>
        /// Saves all changes made in the current transaction to the database.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the number of state entries written to the database.
        /// </returns>
        Task<int> SaveToDatabaseAsync();
    }
}
