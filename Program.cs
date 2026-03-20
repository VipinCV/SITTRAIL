using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using gps_tracking_api.Data;
using gps_tracking_api.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Render PORT fix
builder.WebHost.ConfigureKestrel(options =>
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    options.ListenAnyIP(int.Parse(port));
});

// Services
builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin());
});

var app = builder.Build();

app.UseCors("AllowAll");

app.MapControllers();
app.MapHub<TrackingHub>("/trackingHub");

app.Run();
