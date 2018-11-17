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
        void すでに同じIDを持つデータ存在していることを判定することができる()
        {
            using (var mock = new InmemoryDBTestMock<MeetupsApiContext>())
            {
                var context = mock.Context();
                var targetEntity = new ConnpassEventDataEntity();
                context.ConnpassEventDataEntities.Add(targetEntity);
                context.SaveChanges();

                var target = new ConnpassDatabaseRepository(context);
                Assert.True(target.exitsEntity(targetEntity));
            }
        }

        [Fact]
        void 同じIDを持ったデータが存在していないことを判定することができる()
        {
            using (var mock = new InmemoryDBTestMock<MeetupsApiContext>())
            {
                var target = new ConnpassDatabaseRepository(mock.Context());

                var targetEntity = new ConnpassEventDataEntity
                {
                    Id = 114514
                };

                Assert.False(target.exitsEntity(targetEntity));

                var writingEntityList = new List<ConnpassEventDataEntity>();
                writingEntityList.Add(
                    new ConnpassEventDataEntity
                    {
                        Id = 810
                    }
                );
                target.SaveEventData(writingEntityList);
                Assert.False(target.exitsEntity(targetEntity));
            }
        }


        [Fact]
        void 重複するデータを書き込むと上書きされる()
        {
            using (var mock = new InmemoryDBTestMock<MeetupsApiContext>())
            {
                var connpassDatabaseRepository = new ConnpassDatabaseRepository(mock.Context());

                var dummyEventData = new List<ConnpassEventDataEntity>();
                var entity = new ConnpassEventDataEntity
                {
                    Id = 12,
                    EventTitle = "タイトル１"
                };

                dummyEventData.Add(entity);

                connpassDatabaseRepository.SaveEventData(dummyEventData);
            }

            using (var mock = new InmemoryDBTestMock<MeetupsApiContext>())
            {
                var connpassDatabaseRepository = new ConnpassDatabaseRepository(mock.Context());
                var dummyEventData2 = new List<ConnpassEventDataEntity>();
                var entity2 = new ConnpassEventDataEntity
                {
                    Id = 12,
                    EventTitle = "タイトル２"
                };
                dummyEventData2.Add(entity2);

                connpassDatabaseRepository.SaveEventData(dummyEventData2);

                Assert.Equal(1, mock.Context().ConnpassEventDataEntities.Count());

                Assert.Equal(
                    entity2.EventTitle,
                    mock.Context().ConnpassEventDataEntities.First().EventTitle
                );
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