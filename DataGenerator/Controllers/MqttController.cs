using DataGenerator.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MqttController : ControllerBase
    {
        private readonly IMqttService _mqttService;

        public MqttController(IMqttService mqttService)
        {
            _mqttService = mqttService;
        }

        // POST: http://localhost:5189/api/mqtt/send?topic=example_topic&message=message
        [HttpPost("send")]
        public async Task<IActionResult> send([FromQuery] string topic,
                                              [FromQuery] string message)
        {
            return await _mqttService.SendMessage(message, topic) ? Ok() : StatusCode(500, "Couldn't send message to MQTT service.");
        }

        // GET: http://localhost:5189/api/mqtt/startgenerator?topic=example_topic&minValue=5&maxValue=10&timeStamp=1000
        [HttpGet("startgenerator")]
        public async Task<IActionResult> startGenerator([FromQuery] string topic,
                                                        [FromQuery] int minValue,
                                                        [FromQuery] int maxValue,
                                                        [FromQuery] int timeStamp)
        {
            try
            {
                await _mqttService.StartGenerator(topic, minValue, maxValue, timeStamp);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        // GET: http://localhost:5189/api/mqtt/stopgenerator?topic=example_topic
        [HttpGet("stopgenerator")]
        public async Task<IActionResult> stopGenerator([FromQuery] string topic)
        {
            _mqttService.StopGenerator(topic);
            return Ok();
        }

    }
}
