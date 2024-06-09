using Microsoft.AspNetCore.Mvc;
using MQTTWebApp.Services;
using System.Threading.Tasks;

namespace MQTTWebApp.Controllers
{
    public class MqttController : Controller
    {
        private readonly MqttClientService _mqttClientService;

        public MqttController(MqttClientService mqttClientService)
        {
            _mqttClientService = mqttClientService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Publish(string topic, string message)
        {
            await _mqttClientService.PublishAsync(topic, message);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string topic)
        {
            await _mqttClientService.SubscribeAsync(topic);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Unsubscribe(string topic)
        {
            await _mqttClientService.UnsubscribeAsync(topic);
            return RedirectToAction("Index");
        }
    }
}
