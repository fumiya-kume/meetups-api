using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;
using meetupsApi.JsonEntity;
using meetupsApi.Models;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.EntityFrameworkCore.Storage;
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
        async Task 最新の30件の勉強会を取得することができる()
        {
            using (var mock = new InmemoryDbTestMock<MeetupsApiContext>())
            {
                using (var context = mock.Context())
                {
                    var dummyData = Enumerable.Range(1, 30).Select(id => new ConnpassEventDataEntity {Id = id});
                    context.ConnpassEventDataEntities.AddRange(dummyData);
                    context.SaveChanges();
                    
                    var connpassDatabaseRepository = new ConnpassDatabaseRepository(context);
                    IList<ConnpassEventDataEntity> eventList = await connpassDatabaseRepository.loadEventList(10);
                    Assert.Equal(10,eventList.Count);
                }
            }
        }
    }
}