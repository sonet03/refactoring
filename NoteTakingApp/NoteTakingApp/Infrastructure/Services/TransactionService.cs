using MongoDB.Driver;

namespace NoteTakingApp.Infrastructure.Services;

public interface ITransactionService
{
    Task<T> ExecuteInTransaction<T>(Func<Task<T>> operation);
    Task ExecuteInTransaction(Func<Task> operation);
}

public class TransactionService(IMongoDatabase database) : ITransactionService
{
    public async Task<T> ExecuteInTransaction<T>(Func<Task<T>> operation)
    {
        using var session = await database.Client.StartSessionAsync();
        session.StartTransaction();
        try
        {
            var result = await operation();
            await session.CommitTransactionAsync();
            return result;
        }
        catch
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }

    public async Task ExecuteInTransaction(Func<Task> operation)
    {
        using var session = await database.Client.StartSessionAsync();
        session.StartTransaction();
        try
        {
            await operation();
            await session.CommitTransactionAsync();
        }
        catch
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }
} 