using MongoDB.Driver;
using NoteTakingApp.Domain.Models;

namespace NoteTakingApp.Infrastructure.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameOrEmailAsync(string username, string email, CancellationToken cancellationToken);
}

public class UserRepository(IMongoDatabase mongoDatabase) : MongoRepository<User>(mongoDatabase), IUserRepository
{
    public async Task<User?> GetByUsernameOrEmailAsync(string username, string email, CancellationToken cancellationToken)
    {
        var users = await SearchAsync(u => u.Username == username || u.Email == email, cancellationToken);
        return users.FirstOrDefault();
    }
}