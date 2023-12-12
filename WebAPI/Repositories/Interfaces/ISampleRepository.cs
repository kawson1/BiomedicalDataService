using WebAPI.Models;
using WebAPI.Queries;

namespace WebAPI.Repositories.Interfaces
{
    public interface ISampleRepository
    {
        Task<IEnumerable<Sample>> GetSamples(SampleQuery query);
        Task<Sample> GetSample(string id);

        Task CreateSample(Sample sample);
        Task<bool> UpdateSample(Sample sample);
        Task<bool> DeleteSample(string id);
    }
}
