using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Infrastructure.Data.Repositories
{
    public class TRepository<T> : ITRepository<T>
        where T : class
    {
        private readonly CinemaDbContext _context;
        private readonly DbSet<T> _dbSet;

        public TRepository(CinemaDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

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
    }
}
