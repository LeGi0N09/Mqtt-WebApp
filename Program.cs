using Microsoft.EntityFrameworkCore;
using MQTTApp.Data;
using MQTTWebApp.Services;
using System;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<MqttBrokerService>();
builder.Services.AddSingleton<MqttClientService>();
// Add database context with the connection string from configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStrings")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var mqttBrokerService = app.Services.GetRequiredService<MqttBrokerService>();

// Start the MQTT Broker
await mqttBrokerService.StartBrokerAsync();

// Ensure the broker has started before connecting the client
var mqttClientService = app.Services.GetRequiredService<MqttClientService>();
await mqttClientService.ConnectAsync();

app.Run();
