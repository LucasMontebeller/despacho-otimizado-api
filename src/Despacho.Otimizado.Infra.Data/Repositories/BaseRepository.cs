using Despacho.Otimizado.Domain.Abstractions;
using Despacho.Otimizado.Domain.Interfaces;
using Despacho.Otimizado.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Despacho.Otimizado.Infra.Data.Repositories
{
    public class BaseRepository<T, TId> : IBaseRepository<T, TId> where T : EntityBase<TId>
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<int> InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
    
}