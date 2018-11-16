using meetupsApi.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using meetupsApi.JsonEntity;

namespace meetupsApi.Models
{
    public class MeetupsApiContext : DbContext, IMeetupsApiContext
    {
        public MeetupsApiContext (DbContextOptions<MeetupsApiContext> options)
            : base(options)
        {
        }

        public DbSet<ConnpassEventDataEntity> ConnpassEventDataEntities { get; set; }
    }
}
