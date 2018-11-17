using System.Collections.Generic;
using System.Linq;
using meetupsApi.Domain.Entity;
using meetupsApi.Models;
using meetupsApi.Tests.Domain.Usecase;

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
                return;
            }

            _meetupsApiContext.ConnpassEventDataEntities.Add(entity);
            _meetupsApiContext.SaveChanges();
        }
    }
}