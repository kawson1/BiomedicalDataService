using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using System.Text;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class MqttService : IMqttService
    {
        private readonly IMqttClient _mqttClient;

        private readonly MqttSettings _mqttSettings;
        
        private readonly ISampleRepository _repository;

        public MqttService(IMqttClient mqttClient, MqttSettings mqttSettings, ISampleRepository repository)
        {
            Console.WriteLine("MqttService registered");
            _mqttClient = mqttClient;
            _mqttSettings = mqttSettings;
            _repository = repository;
        }

        public async Task<bool> InitializeAsync()
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(_mqttSettings.Host, _mqttSettings.Port)
                .WithCredentials(_mqttSettings.Username, _mqttSettings.Password)
                .WithClientId(Guid.NewGuid().ToString())
                .Build();

            _mqttClient.ApplicationMessageReceivedAsync += ReceiveMessageHandler;

            var connectResult = await _mqttClient.ConnectAsync(options);

            var mqttFactory = new MqttFactory();
            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter
                (
                    f => f.WithTopic("heartrate")
                )
                .WithTopicFilter
                (
                    f => f.WithTopic("temperature")
                )
                .WithTopicFilter
                (
                    f => f.WithTopic("respirationrate")
                )
                .WithTopicFilter
                (
                    f => f.WithTopic("pressure")
                )
                .Build();

            await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

            return connectResult.ResultCode == MqttClientConnectResultCode.Success;
        }

        public async Task ReceiveMessageHandler(MqttApplicationMessageReceivedEventArgs e)
        {
            string[] message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload).Split(",");
            var value = Int32.Parse(message[0]);
            var instance = Int32.Parse(message[1]);
            SensorType sensorType = SensorType.HeartRate;
            switch (e.ApplicationMessage.Topic)
            {
                case "heartrate":
                    sensorType = SensorType.HeartRate;
                    break;
                case "temperature":
                    sensorType = SensorType.Temperature;
                    break;
                case "respirationrate":
                    sensorType = SensorType.RespirationRate;
                    break;
                case "pressure":
                    sensorType = SensorType.Pressure;
                    break;
            }

            int id = _repository.GetId();
            
            var newSample = new Sample()
            {
                Id = id + 1,
                Date = DateTime.Now,
                SensorType = sensorType,
                SensorId = instance,
                Value = value
            };
            _repository.CreateSample(newSample);
        }
    }
}
