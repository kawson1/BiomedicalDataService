using WebAPI.Models;

namespace WebAPI.Repositories.Interfaces
{
    public interface ISampleRepository
    {
        Task<IEnumerable<Sample>> GetSamples();
        Task<Sample> GetSample(string id);

        Task CreateSample(Sample sample);
        Task<bool> UpdateSample(Sample sample);
        Task<bool> DeleteSample(string id);
    }
}
