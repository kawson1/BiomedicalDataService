using DataGenerator.Interfaces;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace DataGenerator.Services
{
    public class MqttService : IMqttService
    {
        private readonly IMqttClient _mqttClient;

        private readonly MqttSettings _mqttSettings;

        private readonly Dictionary<string, CancellationTokenSource> _threadsTokens = new Dictionary<string, CancellationTokenSource>();

        private int MAX_THREADS = 4;

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

            var connectResult = await _mqttClient.ConnectAsync(options);

            return connectResult.ResultCode == MqttClientConnectResultCode.Success;
        }

        public async Task<bool> SendMessage(string message, string topic)
        {
            var mqttMessage = new MqttApplicationMessageBuilder()
                        .WithTopic(topic)
                        .WithPayload(message)
                        .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                        .WithRetainFlag()
                        .Build();

            var result = await _mqttClient.PublishAsync(mqttMessage);
            return result.IsSuccess;
        }

        public async Task StartGenerator(string topic, int minValue, int maxValue, int timeStamp)
        {
            if (MAX_THREADS == 0)
                throw new Exception("No threads available");
            if (_threadsTokens.ContainsKey(topic))
                throw new Exception($"Generator for {topic} already exists.");
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            Thread thread = new Thread(async () =>
            {
                var random = new Random();
                while (true)
                {
                    if (tokenSource.Token.IsCancellationRequested)
                        return;
                    var value = random.Next(minValue, maxValue);
                    await SendMessage(value.ToString(), topic);
                    await Task.Delay(timeStamp);
                }
            });
            _threadsTokens.Add(topic, tokenSource);
            --MAX_THREADS;
            thread.Start();
        }

        public void StopGenerator(string topic)
        {
            if (_threadsTokens.ContainsKey(topic))
            {
                var tokenSource = _threadsTokens.GetValueOrDefault(topic);
                tokenSource.Cancel();
            }
        }

        public async void Disconnect()
        {
            await _mqttClient.DisconnectAsync();
        }

    }
}
