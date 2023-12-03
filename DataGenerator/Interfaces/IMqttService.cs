using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DataGenerator.Interfaces
{
    public interface IMqttService
    {
        public Task<bool> InitializeAsync();

        public Task<bool> SendMessage(string message, string topic);

        public Task StartGenerator(string topic, int minValue, int maxValue, int timeStamp);

        public void StopGenerator(string topic);

        public void Disconnect();
    }
}
