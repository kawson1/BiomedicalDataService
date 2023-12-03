using DataGenerator.Interfaces;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Services
{
    internal class MqttService : IMqttService
    {
        private readonly IMqttClient _mqttClient;

        public MqttService(IMqttClient mqttClient)
        {
            Console.WriteLine("MqttService registered");
            _mqttClient = mqttClient;
        }

        public async Task<bool> InitializeAsync()
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(Settings.host, Settings.port)
                .WithCredentials(Settings.username, Settings.password)
                .WithClientId(Settings.clientId)
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

        public async void Disconnect()
        {
            await _mqttClient.DisconnectAsync();
        }

    }
}
