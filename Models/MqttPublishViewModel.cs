using System;
using System.ComponentModel.DataAnnotations;

namespace MQTTWebApp.Models
{
    public class MqttPublishViewModel
    {
        [Key]
        public int Id { get; set; } // Primary key for database

        [Required(ErrorMessage = "Topic is required.")]
        public string Topic { get; set; } // Topic for MQTT messages

        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; } // Message content for MQTT

        [Required]
        public DateTime PublishDateTime { get; set; } // Timestamp for when the message is published
    }
}
