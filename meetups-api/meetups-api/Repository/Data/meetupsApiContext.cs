using Microsoft.EntityFrameworkCore;
using meetupsApi.JsonEntity;

namespace meetupsApi.Models
{
    public class meetupsApiContext : DbContext
    {
        public meetupsApiContext (DbContextOptions<meetupsApiContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Event { get; set; }
        public DbSet<Series> Series { get; set; }
    }
}
