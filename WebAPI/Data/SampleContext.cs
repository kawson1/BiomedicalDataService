using MongoDB.Driver;
using WebAPI.Data.Interfaces;
using WebAPI.Models;

namespace WebAPI.DbContext
{
    public class SampleContext : ISampleContext
    {
        public IMongoCollection<Sample> Samples { get; }

        public SampleContext(MongoDBSettings settings) 
        {
            var client = new MongoClient(settings.ConnectionURI);
            var database = client.GetDatabase(settings.DatabaseName);

            Samples = database.GetCollection<Sample>(settings.CollectionName);
        }
    }
}
