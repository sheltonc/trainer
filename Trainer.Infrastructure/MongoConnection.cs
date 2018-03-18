using MongoDB.Driver;

namespace Trainer.Infrastructure
{
    public class MongoConnection
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}