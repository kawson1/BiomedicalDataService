using WebAPI.Models;
using WebAPI.Queries;

namespace WebAPI.Repositories.Interfaces
{
    public interface ISampleRepository
    {
        IEnumerable<Sample> GetSamples(SampleQuery query);
        Sample GetSample(SensorType sensorType);

        Task CreateSample(Sample sample);
        Task<bool> UpdateSample(Sample sample);
        Task<bool> DeleteSample(string id);
    }
}
