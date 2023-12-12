using MQTTnet.Client;

namespace WebAPI.Services.Interfaces
{
    public interface IMqttService
    {
        public Task<bool> InitializeAsync();

        public Task ReceiveMessageHandler(MqttApplicationMessageReceivedEventArgs e);
    }
}
