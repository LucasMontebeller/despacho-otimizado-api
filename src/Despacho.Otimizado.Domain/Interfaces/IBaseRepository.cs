using Despacho.Otimizado.Domain.Abstractions;

namespace Despacho.Otimizado.Domain.Interfaces
{
    public interface IBaseRepository<T, TId> where T : EntityBase<TId>
    {
        Task<T> GetByIdAsync(TId id);
        Task<List<T>> GetAllAsync();
        Task<int> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(TId id);
    }
}