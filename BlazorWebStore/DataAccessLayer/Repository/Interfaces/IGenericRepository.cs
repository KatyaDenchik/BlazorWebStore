using DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace DataAccessLayer.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<int> CreateAsync(T entity);
        Task<int> DeleteAsync(Expression<Func<T, bool>> specification);
        Task<int> DeleteByIdAsync(int id);
        Task<IEnumerable<T>> GetAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
    }
}