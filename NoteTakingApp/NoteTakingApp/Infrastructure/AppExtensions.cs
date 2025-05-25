using MongoDB.Driver;
using NoteTakingApp.Infrastructure.Repositories;
using NoteTakingApp.Infrastructure.Services;

namespace NoteTakingApp.Infrastructure;

public static class AppExtensions
{
    public static void AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB")!;
        var mongoClient = new MongoClient(connectionString);
        
        serviceCollection.AddSingleton(mongoClient.GetDatabase("NoteTakingApp"));
        
        serviceCollection.AddHealthChecks()
            .AddMongoDb(connectionString, name: "mongodb");
        
        serviceCollection.AddSingleton<IParagraphsRepository, ParagraphsRepository>();
        serviceCollection.AddSingleton<INotesRepository, NotesRepository>();
        serviceCollection.AddSingleton<IProjectsRepository, ProjectsRepository>();
        serviceCollection.AddSingleton<IUserRepository, UserRepository>();

        serviceCollection.AddSingleton<IPasswordHasher, PasswordHasher>();
    }
}