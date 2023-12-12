using MongoDB.Driver;
using WebAPI.Data.Interfaces;
using WebAPI.Models;
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

        public async Task<IEnumerable<Sample>> GetSamples()
        {
            return await _context
                .Samples
                .Find(s => true)
                .ToListAsync();
        }

        public Task<bool> UpdateSample(Sample sample)
        {
            throw new NotImplementedException();
        }
    }
}
