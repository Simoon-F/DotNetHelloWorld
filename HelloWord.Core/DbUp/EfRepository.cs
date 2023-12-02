using System.Linq.Expressions;
using HelloWord.Core.Data;
using HelloWord.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace HelloWord.Core.DbUp;

public class EfRepository: IRepository
{
    private readonly SystemDbContext _dbContext;
    
    public EfRepository(SystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<TEntity>> GetAllAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : EntityBase
    {
        return _dbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>>? predicate = null)
        where TEntity : EntityBase
    {
        return predicate == null
            ? _dbContext.Set<TEntity>()
            : _dbContext.Set<TEntity>().Where(predicate);
    }
}