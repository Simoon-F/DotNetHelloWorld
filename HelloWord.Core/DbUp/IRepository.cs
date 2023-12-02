using System.Linq.Expressions;
using HelloWord.Core.Domain;

namespace HelloWord.Core.DbUp;

public interface IRepository
{
    Task<List<TEntity>> GetAllAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : EntityBase;
    
    IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>>? predicate = null) where TEntity : EntityBase;

}