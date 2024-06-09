using Microsoft.EntityFrameworkCore;
using MQTTWebApp.Models;

namespace MQTTApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
             public DbSet<MqttPublishViewModel> MqttPublish { get; set; }
    }

}

