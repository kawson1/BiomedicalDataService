using DataGenerator.Interfaces;
using DataGenerator.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            // Create MQTT client factory
            var factory = new MqttFactory();
            // Create MQTT client instance
            var mqttClient = factory.CreateMqttClient();

            builder.Services.AddControllers();
            builder.Services.AddSingleton<IMqttClient>(mqttClient);
            builder.Services.AddSingleton<IMqttService, MqttService>();

            var app = builder.Build();

            var mqttService = app.Services.GetRequiredService<IMqttService>();
            if (await mqttService.InitializeAsync())
            {
                //app.MapControllers();

                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers(); // Dodaj obsługę kontrolerów

                    // Tutaj możesz dodać inne endpointy lub obsługę statycznych plików, jeśli potrzebujesz
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

/*                app.MapGet("/myendpoint/{message}", async (HttpContext context) =>
                {
                    var message = context.Request.RouteValues["message"]?.ToString();

                    mqttService.SendMessage(message, "example_topic");
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    await context.Response.WriteAsync("Hello from MQTT Service!");
                });*/

/*                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });*/
/*                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });*/