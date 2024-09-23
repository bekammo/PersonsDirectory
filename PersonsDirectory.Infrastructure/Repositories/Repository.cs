using Microsoft.EntityFrameworkCore;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace PersonsDirectory.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly PersonsDirectoryDbContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(PersonsDirectoryDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }
    }
}
