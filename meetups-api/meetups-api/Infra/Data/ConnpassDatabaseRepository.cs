using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;
using meetupsApi.Models;
using meetupsApi.Tests.Domain.Usecase;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Razor.Language;
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

    public async Task SaveEventData(IEnumerable<ConnpassEventDataEntity> eventDataList)
    {
        eventDataList.GroupBy(item => item.Id).Select(list => list.First()).ToList().ForEach(item =>
        {
            if (exitsEntity(item))
            {
                _meetupsApiContext.ConnpassEventDataEntities.Update(item);
            }
            else
            {
                _meetupsApiContext.ConnpassEventDataEntities.Add(item);
            }
        });

        if (_meetupsApiContext.Database.IsSqlServer())
        {
            try
            {
                _meetupsApiContext.Database.OpenConnection();
                _meetupsApiContext.Database.ExecuteSqlCommand(
                    "SET IDENTITY_INSERT dbo.ConnpassEventDataEntities ON");
                _meetupsApiContext.SaveChanges();
                _meetupsApiContext.Database.ExecuteSqlCommand(
                    "SET IDENTITY_INSERT dbo.ConnpassEventDataEntities OFF");
                _meetupsApiContext.Database.CloseConnection();
            }
            catch (Exception e)
            {
                _meetupsApiContext.Database.CloseConnection();
                throw;
            }
        }
        else
        {
            await _meetupsApiContext.SaveChangesAsync();
        }
    }

    public async Task<IList<ConnpassEventDataEntity>> loadEventList(int count = 300)
        => await _meetupsApiContext.ConnpassEventDataEntities.Take(count).ToListAsync();
}