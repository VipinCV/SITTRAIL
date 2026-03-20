using System;

namespace gps_tracking_api.Models
{
    public class Location
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Speed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
