using System.ComponentModel.DataAnnotations;

namespace MQTTWebApp.Models
{
    public class MqttPublishViewModel
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Topic is required.")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; }
        [Required]
        public DateTime PublishDateTime { get; set; }
    }
}
