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

        public Task CreateSample(Sample sample)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteSample(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Sample> GetSample(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Sample>> GetSamples(SampleQuery query)
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

            var filteredSamples = await _context
                .Samples
                .Find(filter)
                .ToListAsync();

            return filteredSamples;
        }

        public Task<bool> UpdateSample(Sample sample)
        {
            throw new NotImplementedException();
        }
    }
}
