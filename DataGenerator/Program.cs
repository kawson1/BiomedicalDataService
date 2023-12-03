using DataGenerator.Interfaces;
using DataGenerator.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System.Security.Authentication;
using System.Text;

namespace DataGenerator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            // Create MQTT client factory
            var factory = new MqttFactory();
            // Create MQTT client instance
            var mqttClient = factory.CreateMqttClient();

            var mqttSettings = builder.Configuration.GetSection("MqttSettings").Get<MqttSettings>();

            builder.Services.AddControllers();
            builder.Services.AddSingleton<MqttSettings>(mqttSettings);
            builder.Services.AddSingleton<IMqttClient>(mqttClient);
            builder.Services.AddSingleton<IMqttService, MqttService>();

            var app = builder.Build();

            var mqttService = app.Services.GetRequiredService<IMqttService>();
            if (await mqttService.InitializeAsync())
            {
                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
                app.Run();
            }
            else
            {
                Console.WriteLine("Failed to initialize MQTT Service. Application cannot start.");
            }
        }
    }
}