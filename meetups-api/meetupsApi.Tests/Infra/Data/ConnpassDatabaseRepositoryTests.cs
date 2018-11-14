using System.Collections.Generic;
using meetupsApi.Domain.Entity;
using meetupsApi.Tests.Domain.Usecase;
using Xunit;

namespace meetupsApi.Tests.Repository.Data
{
    public class ConnpassDatabaseRepositoryTests
    {
        [Fact]
        void ConnpassDatabaseRepositoryというクラスが存在する()
        {
            var target = new ConnpassDatabaseRepository();
            Assert.NotNull(target);
        }

        [Fact]
        void ConnpassDatabaseRepositoryはIConnpassDatabaseRepositoryを実装している()
        {
            var target = new ConnpassDatabaseRepository();
            Assert.True(target is IConnpassDatabaseRepository);
        }
    }

    internal class ConnpassDatabaseRepository:IConnpassDatabaseRepository
    {
        public void SaveEventData(IEnumerable<ConnpassEventDataEntity> dummyData)
        {
            throw new System.NotImplementedException();
        }
    }
}