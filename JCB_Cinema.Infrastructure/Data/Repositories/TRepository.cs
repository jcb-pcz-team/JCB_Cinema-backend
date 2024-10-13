using JCB_Cinema.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Infrastructure.Data.Repositories
{
    public class TRepository<T> : ITRepository<T> where T : class
    {
        private readonly CinemaDbContext _context;
        private readonly DbSet<T> _dbSet;

        public TRepository(CinemaDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // Zwraca IQueryable<T>, co pozwala na budowanie zapytań w wyższych warstwach
        public IQueryable<T> Queryable()
        {
            return _dbSet.AsQueryable();
        }

        // Pobieranie encji po id
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Dodawanie nowej encji
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // Aktualizacja encji
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        // Usuwanie encji
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }

}
