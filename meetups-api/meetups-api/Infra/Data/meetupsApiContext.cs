using meetupsApi.Domain.Entity;
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

        public DbSet<ConnpassEventDataEntity> ConnpassEventDataEntities { get; set; }
    }
}
