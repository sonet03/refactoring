using MongoDB.Driver;
using NoteTakingApp.Domain.Models;

namespace NoteTakingApp.Infrastructure.Repositories;

public interface IProjectsRepository : IRepository<Project>;

public class ProjectsRepository(IMongoDatabase mongoDatabase) : MongoRepository<Project>(mongoDatabase), IProjectsRepository;