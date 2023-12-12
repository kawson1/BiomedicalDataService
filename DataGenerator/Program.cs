using DataGenerator.Interfaces;
using DataGenerator.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
<<<<<<< HEAD
using Microsoft.Extensions.Hosting;
=======
>>>>>>> main
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
<<<<<<< HEAD
            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                 .AddEnvironmentVariables();
=======
            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

>>>>>>> main
            // Create MQTT client factory
            var factory = new MqttFactory();
            // Create MQTT client instance
            var mqttClient = factory.CreateMqttClient();

            var mqttSettings = builder.Configuration.GetSection("MqttSettings").Get<MqttSettings>();
<<<<<<< HEAD
            await Console.Out.WriteLineAsync(mqttSettings.Username);
=======
>>>>>>> main

            builder.Services.AddControllers();
            builder.Services.AddSingleton<MqttSettings>(mqttSettings);
            builder.Services.AddSingleton<IMqttClient>(mqttClient);
            builder.Services.AddSingleton<IMqttService, MqttService>();
<<<<<<< HEAD
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
=======
>>>>>>> main

            var app = builder.Build();

            var mqttService = app.Services.GetRequiredService<IMqttService>();
            if (await mqttService.InitializeAsync())
            {
<<<<<<< HEAD
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
=======
                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
>>>>>>> main
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