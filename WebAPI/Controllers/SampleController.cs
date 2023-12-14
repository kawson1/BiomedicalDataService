using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using WebAPI.Models;
using WebAPI.Queries;
using WebAPI.Services.Interfaces;
using WebAPI.Utilities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly ISampleService _service;

        public SampleController(ISampleService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("Get")]
        [ProducesResponseType(typeof(List<Sample>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Sample>>> GetSamples([FromQuery]SampleQuery query)
        {
            var samples = _service.GetSortedAndFilteredSamples(query);
            return Ok(samples);
        }

        [HttpGet("GetNewest")]
        [ProducesResponseType(typeof(Sample), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Sample>> GetNewestSample([FromQuery]SensorType sensorType)
        {
            var newestSample = _service.GetNewestSample(sensorType);
            return Ok(newestSample);
        }
        
        [HttpGet("GetAvg")]
        [ProducesResponseType(typeof(double), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<double>> GetAbg([FromQuery]SensorType sensorType)
        {
            var avg = _service.GetAvg(sensorType);
            return Ok(avg);
        }

        [HttpGet("GetCsv")]
        public async Task<FileResult> GetCsv([FromQuery] SampleQuery query)
        {
            var csvContent = CsvSerializer.SerializeToUtf8Bytes(_service.GetSortedAndFilteredSamples(query));
            return File(csvContent, "application/csv", "Samples.csv");
        }
        
        [HttpGet("GetJson")]
        public async Task<FileResult> GetJson([FromQuery] SampleQuery query)
        {
            var jsonContent = JsonSerializer.SerializeToUtf8Bytes(_service.GetSortedAndFilteredSamples(query));
            return File(jsonContent, "application/json", "Samples.json");
        }

        [HttpGet("Clear")]
        public async Task<ActionResult> Clear()
        {
            _service.Clear();
            return Ok();
        }
    }
}
