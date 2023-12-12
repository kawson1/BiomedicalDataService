using WebAPI.Models;

namespace WebAPI.Queries;

public class SampleQuery
{
    public string? SortKey { get; set; }
    public int? SortDirection { get; set; }
    public DateTime? DateFrom { get; set; }
    public SensorType? SensorType { get; set; }
    public int? SensorId { get; set; }
}