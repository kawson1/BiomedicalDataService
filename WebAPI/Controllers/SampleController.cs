using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Queries;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly ISampleService _service;

        public SampleController(ISampleService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [ProducesResponseType(typeof(List<Sample>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Sample>>> GetSamples([FromQuery]SampleQuery query)
        {
            var samples = await _service.GetSortedAndFilteredSamples(query);
            return Ok(samples);
        }
    }
}
