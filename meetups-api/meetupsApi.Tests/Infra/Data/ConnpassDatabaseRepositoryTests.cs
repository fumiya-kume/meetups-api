using System.Collections.Generic;
using System.Linq;
using meetupsApi.Domain.Entity;
using meetupsApi.Models;
using meetupsApi.Tests.Domain.Usecase;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace meetupsApi.Tests.Repository.Data
{
    public class ConnpassDatabaseRepositoryTests
    {
        [Fact]
        void ConnpassDatabaseRepoisitoryはDBContextを受け取る()
        {
            var meetupApiContextMoq = new Mock<IMeetupsApiContext>();
            var target = new ConnpassDatabaseRepository(meetupApiContextMoq.Object);

            Assert.NotNull(target);
        }

        [Fact]
        void イベントデータを保存することができる()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<MeetupsApiContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new MeetupsApiContext(options))
            {
                context.Database.EnsureCreated();
            }

            using (var context = new MeetupsApiContext(options))
            {
                 Assert.Empty(context.ConnpassEventDataEntities);
            }
        }
    }

    internal class ConnpassDatabaseRepository : IConnpassDatabaseRepository
    {
        private readonly IMeetupsApiContext _meetupsApiContext;

        public ConnpassDatabaseRepository(IMeetupsApiContext meetupsApiContext)
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
            }
        }
    }
}