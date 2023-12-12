using WebAPI.Models;
using WebAPI.Queries;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services;

public class SampleService : ISampleService
{
    private readonly ISampleRepository _repository;
    
    public SampleService(ISampleRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<List<Sample>> GetSortedAndFilteredSamples(SampleQuery query)
    {
        var filteredSamples = await _repository.GetSamples(query);
        var sortedSamples = sortSamples(filteredSamples, query.SortKey, query.SortDirection);
        return sortedSamples;
    }

    private List<Sample> sortSamples(IEnumerable<Sample> samples, string? key, int? direction)
    {
        Func<Sample, Object> orderByFunc = null;
        if (direction == null)
        {
            orderByFunc = sample => sample.Id;
            direction = 1;
        }
        else if (key.Equals("id"))
        {
            orderByFunc = sample => sample.Id;
        }
        else if (key.Equals("date"))
        {
            orderByFunc = sample => sample.Date;
        }
        else if (key.Equals("sensorType"))
        {
            orderByFunc = sample => sample.SensorType;
        }
        else if (key.Equals("instance"))
        {
            orderByFunc = sample => sample.SensorId;
        }
        else
        {
            orderByFunc = sample => sample.Value;
        }

        if (direction == 1)
        {
            return samples.OrderBy(orderByFunc).ToList();
        }

        return samples.OrderByDescending(orderByFunc).ToList();
    }
}