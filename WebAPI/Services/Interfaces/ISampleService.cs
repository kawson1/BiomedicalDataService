using WebAPI.Models;
using WebAPI.Queries;

namespace WebAPI.Services.Interfaces;

public interface ISampleService
{
    public Task<List<Sample>> GetSortedAndFilteredSamples(SampleQuery query);
}