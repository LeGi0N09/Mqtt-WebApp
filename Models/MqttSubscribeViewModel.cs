using System.ComponentModel.DataAnnotations;

namespace MQTTWebApp.Models
{
    public class MqttSubscribeViewModel
    {
        [Required(ErrorMessage = "Topic is required.")]
        public string Topic { get; set; }

        public bool IsSubscribed { get; set; } = false;
    }
}
