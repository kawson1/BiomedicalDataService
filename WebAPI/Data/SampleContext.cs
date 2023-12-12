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

            var samples = new List<Sample>
            {
                new Sample
                {
                    Id = 1,
                    Date = DateTime.Now.AddHours(-1),
                    Value = 50,
                    SensorId = 1,
                    SensorType = SensorType.HeartRate
                },
                new Sample
                {
                    Id = 2,
                    Date = DateTime.Now.AddHours(-2),
                    Value = 60,
                    SensorId = 2,
                    SensorType = SensorType.HeartRate
                },
                new Sample
                {
                    Id = 3,
                    Date = DateTime.Now.AddHours(-3),
                    Value = 70,
                    SensorId = 1,
                    SensorType = SensorType.Pressure
                },
                new Sample
                {
                    Id = 4,
                    Date = DateTime.Now.AddHours(-4),
                    Value = 80,
                    SensorId = 1,
                    SensorType = SensorType.Temperature
                },
            };
            //Samples.InsertMany(samples);
        }
    }
}
