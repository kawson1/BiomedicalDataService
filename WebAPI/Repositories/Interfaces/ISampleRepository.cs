using WebAPI.Models;
using WebAPI.Queries;

namespace WebAPI.Repositories.Interfaces
{
    public interface ISampleRepository
    {
        IEnumerable<Sample> GetSamples(SampleQuery query);
        int GetId();
        Sample GetSample(SensorType sensorType);

        void CreateSample(Sample sample);

        void Clear();
    }
}
