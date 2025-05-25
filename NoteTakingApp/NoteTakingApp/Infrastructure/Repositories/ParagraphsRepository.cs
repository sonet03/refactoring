using MongoDB.Driver;
using NoteTakingApp.Domain.Models;

namespace NoteTakingApp.Infrastructure.Repositories;

public interface IParagraphsRepository : IRepository<NoteParagraph>;

public class ParagraphsRepository(IMongoDatabase mongoDatabase) : MongoRepository<NoteParagraph>(mongoDatabase), IParagraphsRepository;