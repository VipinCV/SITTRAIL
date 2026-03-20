
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using gps_tracking_api.DTOs;
using gps_tracking_api.Models;
using gps_tracking_api.Data;
using Microsoft.AspNetCore.SignalR;
using gps_tracking_api.Hubs;

namespace gps_tracking_api.Controllers
{
    [ApiController]
    [Route("api")]
    public class TrackingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<TrackingHub> _hub;

        public TrackingController(AppDbContext context, IHubContext<TrackingHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        [HttpPost("location")]
        public async Task<IActionResult> Save(LocationDto dto)
        {
            var loc = new Location
            {
                UserId = dto.UserId,
                Latitude = dto.Lat,
                Longitude = dto.Lng,
                Speed = dto.Speed,
                CreatedAt = DateTime.UtcNow
            };

            _context.Locations.Add(loc);
            await _context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync("ReceiveLocation", loc);

            return Ok(loc);
        }
    }
}
