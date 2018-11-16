using meetupsApi.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace meetupsApi.Models
{
    public interface IMeetupsApiContext
    {
        DbSet<ConnpassEventDataEntity> ConnpassEventDataEntities { get; set; }
    }
}