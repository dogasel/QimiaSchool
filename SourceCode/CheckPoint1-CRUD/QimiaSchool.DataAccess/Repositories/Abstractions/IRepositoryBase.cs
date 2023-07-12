using System.Linq.Expressions;

namespace QimiaSchool.DataAccess.Repositories.Abstractions;

public interface IRepositoryBase<T> where T : class
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
