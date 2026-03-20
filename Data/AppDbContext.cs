using Microsoft.EntityFrameworkCore;
using gps_tracking_api.Models;

namespace gps_tracking_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Location> Locations { get; set; }
    }
}
