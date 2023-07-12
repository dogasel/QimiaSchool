using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using QimiaSchool.DataAccess.Exceptions;
using System.Linq.Expressions;
using System.Threading;

namespace QimiaSchool.DataAccess.Repositories.Abstractions;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class 
{
    protected QimiaSchoolDbContext DbContext { get; set; }

    private readonly DbSet<T> DbSet;

    protected RepositoryBase(QimiaSchoolDbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        DbSet = dbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetByConditionAsync(
        Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(expression).ToListAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {

        return await DbSet.FindAsync(
            id,
            cancellationToken) ??
            throw new EntityNotFoundException<T>(id);
    }

    public async Task CreateAsync(
        T entity,
        CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(
            entity,
            cancellationToken);

        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        T entity,
        CancellationToken cancellationToken)
    {
        DbSet.Update(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id); // Await the GetByIdAsync method
        DbSet.Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }


    public async Task<List<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
    {

        return await DbSet.Where(expression).ToListAsync();
    }

     async Task<List<T>> IRepositoryBase<T>.GetAllAsync(CancellationToken cancellationToken)
    {
        return  await DbSet.ToListAsync(cancellationToken);
    }

   
}
