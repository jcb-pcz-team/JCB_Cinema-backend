using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Infrastructure.Data.Repositories
{
    /// <summary>
    /// A generic repository implementation for managing database operations.
    /// </summary>
    /// <typeparam name="T">The type of the entity being managed by this repository.</typeparam>
    public class TRepository<T> : ITRepository<T>
        where T : class
    {
        private readonly CinemaDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IUserContextService _userContextService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The database context used for entity operations.</param>
        /// <param name="userContextService">The service for retrieving user-related information.</param>
        public TRepository(CinemaDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _userContextService = userContextService;
        }

        /// <summary>
        /// Retrieves a queryable collection of entities, filtered by their status.
        /// </summary>
        /// <param name="entityStatus">The status of the entities to filter (e.g., Exists, All, Deleted).</param>
        /// <returns>An <see cref="IQueryable{T}"/> collection of entities.</returns>
        public IQueryable<T> Queryable(EntityStatusFilter entityStatus = EntityStatusFilter.Exists)
        {
            var entities = _dbSet.AsQueryable();
            if (entityStatus != EntityStatusFilter.All && typeof(EntityBase).IsAssignableFrom(typeof(T)))
            {
                var baseEntities = entities.Cast<EntityBase>();

                var filtered = entityStatus == EntityStatusFilter.Exists
                    ? baseEntities.Where(a => a.IsDeleted == false)
                    : baseEntities.Where(a => a.IsDeleted == true);

                entities = filtered.Cast<T>();
            }
            return entities;
        }

        /// <summary>
        /// Retrieves an entity by its unique identifier, filtered by its status.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="entityStatus">The status of the entity to filter (e.g., Exists, Deleted).</param>
        /// <returns>A task representing the asynchronous operation, with the entity if found; otherwise, null.</returns>
        public async Task<T?> GetByIdAsync(int id, EntityStatusFilter entityStatus = EntityStatusFilter.Exists)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return null;

            if (entity is EntityBase baseEntity)
            {
                if (entityStatus == EntityStatusFilter.Deleted && !baseEntity.IsDeleted)
                    return null;

                if (entityStatus == EntityStatusFilter.Exists && baseEntity.IsDeleted)
                    return null;
            }

            return entity as T;
        }

        /// <summary>
        /// Adds a new entity to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <exception cref="ArgumentNullException">Thrown if the entity is null.</exception>
        public async Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity is EntityBase entityBase)
            {
                FillEntityBase(entityBase);
            }

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <exception cref="ArgumentNullException">Thrown if the entity is null.</exception>
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity is EntityBase entityBase)
            {
                FillEntityBase(entityBase);
            }
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an entity by its unique identifier. Supports both soft and hard deletes.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <exception cref="KeyNotFoundException">Thrown if the entity with the specified ID is not found.</exception>
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                throw new KeyNotFoundException($"Entity with ID {id} not found.");

            if (entity is EntityBase baseEntity)
            {
                DeleteEntityBase(baseEntity);
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity); // Hard delete
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Populates common properties for an <see cref="EntityBase"/> instance during an add or update operation.
        /// </summary>
        /// <typeparam name="TEntityBase">The type of the entity, derived from <see cref="EntityBase"/>.</typeparam>
        /// <param name="entity">The entity to populate.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user cannot be identified.</exception>
        public void FillEntityBase<TEntityBase>(TEntityBase entity)
            where TEntityBase : EntityBase
        {
            var userName = _userContextService.GetUserName();
            if (userName == null)
            {
                throw new UnauthorizedAccessException();
            }
            entity.IsDeleted = false;
            if (!entity.Created.HasValue || string.IsNullOrEmpty(entity.Creator))
            {
                entity.Created = DateTime.UtcNow;
                entity.Creator = userName;
            }
            entity.Modified = DateTime.UtcNow;
            entity.Modifier = userName;
        }

        /// <summary>
        /// Marks an <see cref="EntityBase"/> instance as deleted during a soft delete operation.
        /// </summary>
        /// <typeparam name="TEntityBase">The type of the entity, derived from <see cref="EntityBase"/>.</typeparam>
        /// <param name="entity">The entity to mark as deleted.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user cannot be identified.</exception>
        public void DeleteEntityBase<TEntityBase>(TEntityBase entity)
            where TEntityBase : EntityBase
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
