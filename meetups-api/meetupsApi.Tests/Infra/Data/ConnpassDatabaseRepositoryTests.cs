using System.Collections;
using System.Collections.Generic;
using System.Linq;
using meetupsApi.Domain.Entity;
using meetupsApi.JsonEntity;
using meetupsApi.Models;
using Microsoft.DotNet.PlatformAbstractions;
using Moq;
using Xunit;

namespace meetupsApi.Tests.Repository.Data
{
    public class ConnpassDatabaseRepositoryTests
    {
        [Fact]
        void イベントデータを保存することができる()
        {
            using (var mock = new InmemoryDbTestMock<MeetupsApiContext>())
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
            using (var mock = new InmemoryDbTestMock<MeetupsApiContext>())
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
            using (var mock = new InmemoryDbTestMock<MeetupsApiContext>())
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
            using (var mock = new InmemoryDbTestMock<MeetupsApiContext>())
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

            using (var mock = new InmemoryDbTestMock<MeetupsApiContext>())
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
            using (var testMock = new InmemoryDbTestMock<MeetupsApiContext>())
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
            using (var testMock = new InmemoryDbTestMock<MeetupsApiContext>())
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

            using (var testMock = new InmemoryDbTestMock<MeetupsApiContext>())
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
            using (var testMock = new InmemoryDbTestMock<MeetupsApiContext>())
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

                var mock2 = new InmemoryDbTestMock<MeetupsApiContext>();

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