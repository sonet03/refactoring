using System.Linq.Expressions;
using NoteTakingApp.Domain;

namespace NoteTakingApp.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : IHasId
{
    Task UpsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<IList<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
}