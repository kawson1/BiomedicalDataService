using MongoDB.Driver;
using WebAPI.Data.Interfaces;
using WebAPI.Models;
using WebAPI.Queries;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class SampleRepository : ISampleRepository
    {
        private readonly ISampleContext _context;

        public SampleRepository(ISampleContext context) 
        { 
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void CreateSample(Sample sample)
        {
            _context.Samples.InsertOne(sample);
        }

        public Sample GetSample(SensorType sensorType)
        {
            var filterBuilder = Builders<Sample>.Filter;
            var filter = filterBuilder.Eq(s => s.SensorType, sensorType);
            var newestSample = _context
                .Samples
                .Find(filter)
                .SortByDescending(s => s.Id)
                .First();
            return newestSample;
        }
        
        public int GetId()
        {
            var newestSample = _context
                .Samples
                .Find(_ => true)
                .SortByDescending(s => s.Id);
            if (!newestSample.Any())
                return 0;
            return newestSample.First().Id;
        }
        
        

        public IEnumerable<Sample> GetSamples(SampleQuery query)
        {
            var builder = Builders<Sample>.Filter;
            var filter = builder.Empty;
            if (query.DateFrom != null)
            {
                filter &= builder.Gt(s => s.Date, query.DateFrom);
            }

            if (query.SensorType != null)
            {
                filter &= builder.Eq(s => s.SensorType, query.SensorType);
            }

            if (query.SensorId != null)
            {
                filter &= builder.Eq(s => s.SensorId, query.SensorId);
            }

            var filteredSamples = _context
                .Samples
                .Find(filter)
                .Limit(5000)
                .ToList();

            return filteredSamples;
        }

        public void Clear()
        {
            var builder = Builders<Sample>.Filter;
            var filter = builder.Empty;
            _context.Samples.DeleteMany(filter);
        }
    }
}
