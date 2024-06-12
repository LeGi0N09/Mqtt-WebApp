using MQTTnet;
using MQTTnet.Client;
using MQTTApp.Data;
using MQTTWebApp.Models;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MQTTWebApp.Services
{
    public class MqttClientService
    {
        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions _mqttClientOptions;
        private readonly IServiceScopeFactory _scopeFactory;

        public MqttClientService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            _mqttClientOptions = new MqttClientOptionsBuilder()
                .WithClientId("Client1")
                .WithTcpServer("192.168.1.7", 1883) // Replace with the broker's IP address
                .Build();
        }

        public async Task ConnectAsync()
        {
            _mqttClient.ConnectedAsync += OnConnected;
            _mqttClient.DisconnectedAsync += OnDisconnected;
            _mqttClient.ApplicationMessageReceivedAsync += OnMessageReceived;

            try
            {
                await _mqttClient.ConnectAsync(_mqttClientOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while connecting to MQTT broker: {ex.Message}");
                throw;
            }
        }

        private Task OnConnected(MqttClientConnectedEventArgs e)
        {
            Console.WriteLine("Connected successfully with MQTT Brokers.");
            // Subscribe to all topics using the wildcard "#"
            return _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("#").Build());
        }

        private Task OnDisconnected(MqttClientDisconnectedEventArgs e)
        {
            Console.WriteLine("Disconnected from MQTT Brokers.");
            return Task.CompletedTask;
        }

        private async Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Check the topic of the incoming message
                string topic = e.ApplicationMessage.Topic;

                // Save all messages to the database
                var message = new MqttPublishViewModel
                {
                    Topic = topic,
                    Message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload),
                    PublishDateTime = DateTime.Now // Use DateTime.Now for local machine time
                };

                context.MqttPublish.Add(message);
                await context.SaveChangesAsync();
            }

            Console.WriteLine($"Received Message: {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)} on Topic: {e.ApplicationMessage.Topic}");
        }

        public async Task PublishAsync(string topic, string payload)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                .WithRetainFlag()
                .Build();

            if (_mqttClient.IsConnected)
            {
                await _mqttClient.PublishAsync(message);
            }
        }


        public async Task SubscribeAsync(string topic)
        {
            if (_mqttClient.IsConnected)
            {
                await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
            }
        }

        public async Task UnsubscribeAsync(string topic)
        {
            if (_mqttClient.IsConnected)
            {
                await _mqttClient.UnsubscribeAsync(topic);
            }
        }
    }
}
