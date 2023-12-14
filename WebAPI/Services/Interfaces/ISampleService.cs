using WebAPI.Models;
using WebAPI.Queries;

namespace WebAPI.Services.Interfaces;

public interface ISampleService
{
    public List<Sample> GetSortedAndFilteredSamples(SampleQuery query);
    public Sample GetNewestSample(SensorType sensorType);

    public double GetAvg(SensorType sensorType);

    public void Clear();
}