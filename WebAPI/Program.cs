using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client;
using WebAPI.Data.Interfaces;
using WebAPI.DbContext;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services;
using WebAPI.Services.Interfaces;

namespace WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Create MQTT client factory
            var factory = new MqttFactory();
            // Create MQTT client instance
            var mqttClient = factory.CreateMqttClient();

            var mqttSettings = builder.Configuration.GetSection("MqttSettings").Get<MqttSettings>();
            var mongodbSettings = builder.Configuration.GetSection("MongoDB").Get<MongoDBSettings>();

            builder.Services.AddSingleton<MongoDBSettings>(mongodbSettings);
            builder.Services.AddScoped<ISampleContext, SampleContext>();
            builder.Services.AddScoped<ISampleService, SampleService>();
            builder.Services.AddScoped<ISampleRepository, SampleRepository>();
            builder.Services.AddSingleton<MqttSettings>(mqttSettings);
            builder.Services.AddSingleton<IMqttClient>(mqttClient);
            builder.Services.AddSingleton<IMqttService, MqttService>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            var mqttService = app.Services.GetRequiredService<IMqttService>();
            if (await mqttService.InitializeAsync())
            {
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

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