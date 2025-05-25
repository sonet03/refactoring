using MongoDB.Driver;
using NoteTakingApp.Domain.Models;

namespace NoteTakingApp.Infrastructure.Repositories;

public interface INotesRepository : IRepository<Note>;

public class NotesRepository(IMongoDatabase mongoDatabase) : MongoRepository<Note>(mongoDatabase), INotesRepository;