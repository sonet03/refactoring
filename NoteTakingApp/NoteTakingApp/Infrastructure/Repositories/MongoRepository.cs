using System.Linq.Expressions;
using MongoDB.Driver;
using NoteTakingApp.Domain;

namespace NoteTakingApp.Infrastructure.Repositories;

public class MongoRepository<TEntity>(IMongoDatabase mongoDatabase) : IRepository<TEntity> where TEntity : IHasId
{
    protected readonly IMongoCollection<TEntity>
        Collection = mongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name);

    public async Task UpsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity, new ReplaceOptions { IsUpsert = true },
            cancellationToken: cancellationToken);
    }
    
    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        await Collection.DeleteOneAsync(e => e.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<TEntity?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var res = await Collection.FindAsync(e => e.Id == id, cancellationToken: cancellationToken);
        return res.SingleOrDefault();
    }

    public async Task<IList<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        var res = await Collection.FindAsync(filter, cancellationToken: cancellationToken);
        return await res.ToListAsync(cancellationToken: cancellationToken);
    }
}