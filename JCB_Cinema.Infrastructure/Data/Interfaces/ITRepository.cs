using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Infrastructure.Data.Interfaces
{
    public interface ITRepository<T>
        where T : EntityBase
    {
        IQueryable<T> Queryable(EntityStatusFilter entityStatus = EntityStatusFilter.Exists);
        Task<T?> GetByIdAsync(int id, EntityStatusFilter entityStatus = EntityStatusFilter.Exists);
    }
}
