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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(
        //            "Server=tcp:mqttweb.database.windows.net,1433;Initial Catalog=MQTT;Persist Security Info=False;User ID=sqladmin;Password=admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
        //            options => options.EnableRetryOnFailure(
        //                maxRetryCount: 5,
        //                maxRetryDelay: TimeSpan.FromSeconds(30),
        //                errorNumbersToAdd: null));
        //    }
        //}
    }
}
