using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Infrastructure.Data.Interfaces
{
    public interface ITRepository<T>
        where T : class
    {
        IQueryable<T> Queryable(EntityStatusFilter entityStatus = EntityStatusFilter.Exists);
        Task<T?> GetByIdAsync(int id, EntityStatusFilter entityStatus = EntityStatusFilter.Exists);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        void FillEntityBase<TEntityBase>(TEntityBase entity) where TEntityBase : EntityBase;
        void DeleteEntityBase<TEntityBase>(TEntityBase entity) where TEntityBase : EntityBase;
    }
}
