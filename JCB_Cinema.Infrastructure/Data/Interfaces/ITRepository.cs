using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Infrastructure.Data.Interfaces
{
    /// <summary>
    /// Defines a generic repository interface for handling database operations for a specific entity type.
    /// </summary>
    /// <typeparam name="T">The type of entity this repository will manage. Must be a class.</typeparam>
    public interface ITRepository<T> where T : class
    {
        /// <summary>
        /// Returns an <see cref="IQueryable{T}"/> to allow querying the specified entity type.
        /// </summary>
        /// <param name="entityStatus">The status filter for the entities (e.g., Exists, Deleted, or All).</param>
        /// <returns>An <see cref="IQueryable{T}"/> of the specified entity type.</returns>
        IQueryable<T> Queryable(EntityStatusFilter entityStatus = EntityStatusFilter.Exists);

        /// <summary>
        /// Retrieves an entity by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="entityStatus">The status filter for the entity.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the entity or <c>null</c> if not found.</returns>
        Task<T?> GetByIdAsync(int id, EntityStatusFilter entityStatus = EntityStatusFilter.Exists);

        /// <summary>
        /// Asynchronously adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Asynchronously updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity with updated information.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Asynchronously deletes an entity from the repository by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Fills base properties of an entity implementing <see cref="EntityBase"/>.
        /// </summary>
        /// <typeparam name="TEntityBase">The type of entity that implements <see cref="EntityBase"/>.</typeparam>
        /// <param name="entity">The entity to fill.</param>
        void FillEntityBase<TEntityBase>(TEntityBase entity) where TEntityBase : EntityBase;

        /// <summary>
        /// Marks an entity implementing <see cref="EntityBase"/> as deleted.
        /// </summary>
        /// <typeparam name="TEntityBase">The type of entity that implements <see cref="EntityBase"/>.</typeparam>
        /// <param name="entity">The entity to mark as deleted.</param>
        void DeleteEntityBase<TEntityBase>(TEntityBase entity) where TEntityBase : EntityBase;
    }
}
