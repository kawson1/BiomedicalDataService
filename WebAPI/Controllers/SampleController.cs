using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly ISampleRepository _repository;

        public SampleController(ISampleRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Sample>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Sample>>> GetSamples()
        {
            var samples = await _repository.GetSamples();
            return Ok(samples);
        }
    }
}
