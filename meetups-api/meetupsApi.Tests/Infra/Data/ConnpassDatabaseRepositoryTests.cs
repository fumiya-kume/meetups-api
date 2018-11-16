using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using meetupsApi.Domain.Entity;
using meetupsApi.JsonEntity;
using meetupsApi.Models;
using meetupsApi.Tests.Domain.Usecase;
using Microsoft.Data.Sqlite;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace meetupsApi.Tests.Repository.Data
{
    public class ConnpassDatabaseRepositoryTests
    {
        [Fact]
        void イベントデータを保存することができる()
        {
            using (var mock = new InmemoryDBTestMock<MeetupsApiContext>())
            {
                var connpassDatabaseRepository = new ConnpassDatabaseRepository(mock.Context());
                var dummyEventData = new List<ConnpassEventDataEntity>();
                var entity = new ConnpassEventDataEntity();
                dummyEventData.Add(entity);
                connpassDatabaseRepository.SaveEventData(dummyEventData);

                Assert.Equal(1, mock.Context().ConnpassEventDataEntities.Count());
            }
        }

        [Fact]
        void DBにデータを保存することができる()
        {
            using (var testMock = new InmemoryDBTestMock<MeetupsApiContext>())
            {
                var context = testMock.Context();

                var entity = new ConnpassEventDataEntity();
                entity.EventTitle = "タイトル";
                entity.EventDescription = "デスク";
                entity.EventUrl = "www.yahoo.co.jp";
                entity.Lat = 1.2;
                entity.Lon = 1.3;
                context.ConnpassEventDataEntities.Add(entity);
                context.SaveChanges();

                Assert.Equal(1, context.ConnpassEventDataEntities.Count());
                testMock.Dispose();
            }
        }

        [Fact]
        void DBに連続して書き込みを行ってもDisposableな実装にしていると以前のデータベースは消えている()
        {
            using (var testMock = new InmemoryDBTestMock<MeetupsApiContext>())
            {
                var context = testMock.Context();

                var entity = new ConnpassEventDataEntity();
                entity.EventTitle = "タイトル";
                entity.EventDescription = "デスク";
                entity.EventUrl = "www.yahoo.co.jp";
                entity.Lat = 1.2;
                entity.Lon = 1.3;
                context.ConnpassEventDataEntities.Add(entity);
                context.SaveChanges();

                Assert.Equal(1, context.ConnpassEventDataEntities.Count());
            }

            using (var testMock = new InmemoryDBTestMock<MeetupsApiContext>())
            {
                var context = testMock.Context();

                var entity = new ConnpassEventDataEntity();
                entity.EventTitle = "タイトル";
                entity.EventDescription = "デスク";
                entity.EventUrl = "www.yahoo.co.jp";
                entity.Lat = 1.2;
                entity.Lon = 1.3;
                context.ConnpassEventDataEntities.Add(entity);
                context.SaveChanges();

                Assert.Equal(1, context.ConnpassEventDataEntities.Count());
            }
        }

        [Fact]
        void Disposableにしない場合は以前のデータが残っている()
        {
            using (var testMock = new InmemoryDBTestMock<MeetupsApiContext>())
            {
                var context = testMock.Context();

                var entity = new ConnpassEventDataEntity();
                entity.EventTitle = "タイトル";
                entity.EventDescription = "デスク";
                entity.EventUrl = "www.yahoo.co.jp";
                entity.Lat = 1.2;
                entity.Lon = 1.3;
                context.ConnpassEventDataEntities.Add(entity);
                context.SaveChanges();

                Assert.Equal(1, context.ConnpassEventDataEntities.Count());

                var mock2 = new InmemoryDBTestMock<MeetupsApiContext>();

                var context2 = mock2.Context();

                var entity2 = new ConnpassEventDataEntity();
                entity2.EventTitle = "タイトル";
                entity2.EventDescription = "デスク";
                entity2.EventUrl = "www.yahoo.co.jp";
                entity2.Lat = 1.2;
                entity2.Lon = 1.3;
                context.ConnpassEventDataEntities.Add(entity2);
                context.SaveChanges();

                Assert.Equal(2, context2.ConnpassEventDataEntities.Count());

                testMock.Dispose();
            }
        }
    }
}

public class InmemoryDBTestMock<T> : IDisposable where T : DbContext
{
    private static readonly SqliteConnection Connection = new SqliteConnection("DataSource=:memory:");
    private readonly DbContextOptions _contextOptions = new DbContextOptionsBuilder<T>().UseSqlite(Connection).Options;

    public InmemoryDBTestMock()
    {
        Connection.Open();
        Context().Database.EnsureCreated();
    }

    public T Context() => (T) Activator.CreateInstance(typeof(T), _contextOptions);

    public void Dispose()
    {
        Context().Database.EnsureDeleted();
        Connection.Close();
    }
}

internal class ConnpassDatabaseRepository : IConnpassDatabaseRepository
{
    private readonly MeetupsApiContext _meetupsApiContext;

    public ConnpassDatabaseRepository(MeetupsApiContext meetupsApiContext)
    {
        _meetupsApiContext = meetupsApiContext;
    }

    public void SaveEventData(IEnumerable<ConnpassEventDataEntity> eventDataList)
    {
        foreach (var entity in eventDataList)
        {
            var exitsEntity = _meetupsApiContext.ConnpassEventDataEntities.Count(item => item.Id == entity.Id) != 0;
            if (exitsEntity)
            {
                var oldItemList = _meetupsApiContext.ConnpassEventDataEntities.Where(item => item.Id == entity.Id);
                _meetupsApiContext.ConnpassEventDataEntities.RemoveRange(oldItemList);
            }

            _meetupsApiContext.ConnpassEventDataEntities.Add(entity);
            _meetupsApiContext.SaveChanges();
        }
    }
}