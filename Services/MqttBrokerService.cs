using MQTTnet;
using MQTTnet.Server;
using System.Net;
using System.Threading.Tasks;

namespace MQTTWebApp.Services
{
    public class MqttBrokerService
    {
        private MqttServer _mqttServer;

        public async Task StartBrokerAsync()
        {
            var optionsBuilder = new MqttServerOptionsBuilder()
                .WithDefaultEndpoint()
               // .WithDefaultEndpointBoundIPAddress(IPAddress.Parse("192.168.1.7"))
                .WithDefaultEndpointBoundIPAddress(System.Net.IPAddress.Any) // Bind to all available interfaces
                .WithDefaultEndpointPort(1883); // Ensure this port is available

            var mqttFactory = new MqttFactory();
            _mqttServer = mqttFactory.CreateMqttServer(optionsBuilder.Build());
            await _mqttServer.StartAsync();
        }

        public async Task StopBrokerAsync()
        {
            if (_mqttServer != null)
            {
                await _mqttServer.StopAsync();
            }
        }
    }
}
