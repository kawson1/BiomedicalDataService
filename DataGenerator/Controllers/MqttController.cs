using DataGenerator.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    internal class MqttController : ControllerBase
    {
        private readonly IMqttService _mqttService;

        public MqttController(IMqttService mqttService)
        {
            _mqttService = mqttService;
        }

        //[Route("send/{topic}/{message}")]
        [HttpPost("send/{topic}/{message}")]
        public async Task<IActionResult> send([FromRoute] string topic,
                                              [FromRoute] string message)
        {
            return await _mqttService.SendMessage(message, topic) ? Ok() : StatusCode(500, "Couldn't send message to MQTT service.");
        }

        [HttpGet]
        public void test()
        {
            Console.WriteLine("TEST");
        }
    }
}
