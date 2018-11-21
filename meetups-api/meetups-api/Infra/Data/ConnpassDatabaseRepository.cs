using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;
using meetupsApi.Models;
using meetupsApi.Tests.Domain.Usecase;
using Microsoft.EntityFrameworkCore;

public class ConnpassDatabaseRepository : IConnpassDatabaseRepository
{
    private readonly MeetupsApiContext _meetupsApiContext;

    public ConnpassDatabaseRepository(MeetupsApiContext meetupsApiContext)
    {
        _meetupsApiContext = meetupsApiContext;
    }

    public bool exitsEntity(ConnpassEventDataEntity entity) =>
        _meetupsApiContext.ConnpassEventDataEntities.Count(item => item.Id == entity.Id) != 0;

    public void SaveEventData(IEnumerable<ConnpassEventDataEntity> eventDataList)
    {
        foreach (var entity in eventDataList)
        {
            if (exitsEntity(entity))
            {
                _meetupsApiContext.ConnpassEventDataEntities.Update(entity);
            }
            else
            {
                _meetupsApiContext.ConnpassEventDataEntities.Add(entity);
            }
        }

        _meetupsApiContext.Database.OpenConnection();
        _meetupsApiContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.ConnpassEventDataEntities ON");
        _meetupsApiContext.SaveChanges();
        _meetupsApiContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.ConnpassEventDataEntities OFF");
        _meetupsApiContext.Database.CloseConnection();
    }

    public async Task<IList<ConnpassEventDataEntity>> loadEventList(int count)
        => await _meetupsApiContext.ConnpassEventDataEntities.Take(count).ToListAsync();
}