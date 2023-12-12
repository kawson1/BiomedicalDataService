using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using System.Text;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class MqttService : IMqttService
    {
        private readonly IMqttClient _mqttClient;

        private readonly MqttSettings _mqttSettings;

        private Thread t1;
        private Thread t2;
        private Thread t3;
        private Thread t4;

        public MqttService(IMqttClient mqttClient, MqttSettings mqttSettings)
        {
            Console.WriteLine("MqttService registered");
            _mqttClient = mqttClient;
            _mqttSettings = mqttSettings;
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
                    f => f.WithTopic("example_topic")
                )
                .WithTopicFilter
                (
                    f => f.WithTopic("example_topic_2")
                )
                .Build();

            await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

            return connectResult.ResultCode == MqttClientConnectResultCode.Success;
        }

        public async Task ReceiveMessageHandler(MqttApplicationMessageReceivedEventArgs e)
        {
            await Console.Out.WriteLineAsync(e.ApplicationMessage.Topic + " : " + Encoding.UTF8.GetString(e.ApplicationMessage.Payload)); 
        }
    }
}
