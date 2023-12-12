using MongoDB.Driver;
using WebAPI.Models;

namespace WebAPI.Data.Interfaces
{
    public interface ISampleContext
    {
        IMongoCollection<Sample> Samples { get; }
    }
}
