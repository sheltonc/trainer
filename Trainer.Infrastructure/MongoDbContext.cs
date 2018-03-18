using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Trainer.Domain.Aggregates.Workout;

namespace Trainer.Infrastructure
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDbContext(IOptions<MongoConnection> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Workout> Workouts => _database.GetCollection<Workout>("Workouts");
    }
}