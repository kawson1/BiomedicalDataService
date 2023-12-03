using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Interfaces
{
    internal interface IMqttService
    {
        public Task<bool> InitializeAsync();

        public Task<bool> SendMessage(string message, string topic);

        public void Disconnect();
    }
}
