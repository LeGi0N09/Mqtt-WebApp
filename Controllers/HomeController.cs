using Microsoft.AspNetCore.Mvc;
using MQTTWebApp.Models;
using System.Threading.Tasks;
using MQTTWebApp.Services;
using MQTTApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MQTTWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly MqttClientService _mqttClientService;
        private readonly ApplicationDbContext _context;

        public HomeController(MqttClientService mqttClientService, ApplicationDbContext context)
        {
            _mqttClientService = mqttClientService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Subscribe()
        {
            var model = new MqttSubscribeViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(MqttSubscribeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _mqttClientService.SubscribeAsync(model.Topic);
                   
                    TempData["SubscribeSuccessMessage"] = $"Subscribed to topic: {model.Topic}";
                    return RedirectToAction("Subscribe"); // Redirect to the GET action to prevent resubmission
                }
                catch
                {
                    TempData["SubscribeErrorMessage"] = "Subscription failed!";
                }
            }
            else
            {
                TempData["SubscribeErrorMessage"] = "Subscription failed: Model state is invalid.";
            }
            TempData.Keep(); // Ensure TempData is kept for the next request
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Unsubscribe(MqttSubscribeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _mqttClientService.UnsubscribeAsync(model.Topic);
                    TempData["UnsubscribeSuccessMessage"] = $"Unsubscribed from topic: {model.Topic}";
                    return RedirectToAction("Subscribe"); // Redirect to the GET action to prevent resubmission
                }
                catch
                {
                    TempData["UnsubscribeErrorMessage"] = "Unsubscription failed!";
                }
            }
            else
            {
                TempData["UnsubscribeErrorMessage"] = "Unsubscription failed: Model state is invalid.";
            }
            TempData.Keep(); // Ensure TempData is kept for the next request
            return View("Subscribe", model);
        }


        public IActionResult Publish()
        {
            var model = new MqttPublishViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Publish(MqttPublishViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _mqttClientService.PublishAsync(model.Topic, model.Message);

                    TempData["SuccessMessage"] = "Message published successfully!";
                    ModelState.Clear();
                    model.Topic = string.Empty; // Clear the topic field
                    model.Message = string.Empty; // Clear the message field

                    return RedirectToAction("Publish"); // Redirect to the GET action
                }
                catch (Exception ex)
                {
                    // Log the exception if necessary
                    Console.WriteLine(ex.Message);
                    TempData["ErrorMessage"] = "Message not published!";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Message not published!";
                // Log validation errors for debugging
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Error in {key}: {error.ErrorMessage}");
                    }
                }
            }
            TempData.Keep(); // Ensure TempData is kept for the next request
            return View(model);
        }


        // New Action to display published messages
        public async Task<IActionResult> ViewMessages()
        {
            var messages = await _context.MqttPublish.ToListAsync();
            return View(messages);
        }
    }
}
