using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using gps_tracking_api.Data;
using gps_tracking_api.Hubs;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// 🔧 CORRECT WAY: Set port via environment variable (.NET 6+)
// ============================================================
builder.WebHost.ConfigureKestrel(options =>
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    options.ListenAnyIP(int.Parse(port));
});

// ============================================================
// 📦 Register Services
// ============================================================
builder.Services.AddControllers();
builder.Services.AddSignalR();

// 🔑 CORRECT: Use the connection string properly
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// ============================================================
// 🌐 CORS Configuration
// ============================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin());
});

// ============================================================
// 🏗️ Build the App
// ============================================================
var app = builder.Build();

// ============================================================
// 🚀 Middleware Pipeline
// ============================================================
app.UseCors("AllowAll");

app.MapControllers();
app.MapHub<TrackingHub>("/trackingHub");

app.Run();