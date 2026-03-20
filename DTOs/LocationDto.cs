
using System;

namespace gps_tracking_api.DTOs
{
    public class LocationDto
    {
        public Guid UserId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public double Speed { get; set; }
    }
}
