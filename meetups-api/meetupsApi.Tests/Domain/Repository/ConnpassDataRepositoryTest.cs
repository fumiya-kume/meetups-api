using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;
using meetupsApi.JsonEntity;
using meetupsApi.Tests.Domain.Usecase;
using meetupsApi.Tests.Repository;
using Moq;
using Xunit;

namespace meetupsApi.Tests.Domain.Repository
{
    public class ConnpassDataRepositoryTest
    {
        [Fact]
        void ConnpassDataRepositoryが存在する()
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyDataRepository(dataStoreMoq.Object);
        }

        [Fact]
        void ConnpassDataRepositoryはIConnpassDataRepositoryを実装する()
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyDataRepository(dataStoreMoq.Object);
            var actual = connpassDataRepository as IConnpassReadOnlyDataRepository;
            Assert.NotNull(actual);
        }

        [Fact]
        void データを更新するとデータストアからデータを取得してくる()
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var dummyConnpassData = new ConnpassMeetupJson();
            dataStoreMoq.Setup(obj => obj.LoadConnpassDataAsync(100)).ReturnsAsync(dummyConnpassData);
            var connpassDataRepository = new ConnpassReadOnlyDataRepository(dataStoreMoq.Object);
            connpassDataRepository.LoadConnpassData();
            dataStoreMoq.Verify(obj => obj.LoadConnpassDataAsync(100), Times.Once);
        }

        [Fact]
        void JsonDataをEntityに変換することができる()
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyDataRepository(dataStoreMoq.Object);
            var targetData = new ConnpassEvent();
            ConnpassEventDataEntity item = connpassDataRepository.convert(targetData);
        }

        [Fact]
        void 変換されたDomainEntityにはタイトルが保存されている()
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyDataRepository(dataStoreMoq.Object);
            var dummyTitle = "";
            var targetData = new ConnpassEvent
            {
                title = dummyTitle
            };
            ConnpassEventDataEntity item = connpassDataRepository.convert(targetData);
            Assert.Equal(dummyTitle, item.EventTitle);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        void タイトルが存在しない場合はから文字が入っている(string dummyTitle)
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyDataRepository(dataStoreMoq.Object);

            var targetData = new ConnpassEvent
            {
                title = dummyTitle
            };
            ConnpassEventDataEntity item = connpassDataRepository.convert(targetData);
            Assert.Equal("", item.EventTitle);
        }
    }

    public interface IConnpassDataStore
    {
        Task<ConnpassMeetupJson> LoadConnpassDataAsync(int capacity = 100);
    }


    public class ConnpassReadOnlyDataRepository : IConnpassReadOnlyDataRepository
    {
        private readonly IConnpassDataStore _connpassDatastore;

        public ConnpassReadOnlyDataRepository(IConnpassDataStore connpassDatastore)
        {
            _connpassDatastore = connpassDatastore;
        }

        public async Task<IEnumerable<ConnpassEventDataEntity>> LoadConnpassData()
        {
            var jsonData = await _connpassDatastore.LoadConnpassDataAsync(100);
            return jsonData.ConnpassEvents.Select(item => convert(item));
        }

        public ConnpassEventDataEntity convert(ConnpassEvent targetData)
        {
            var entity = new ConnpassEventDataEntity();
            entity.EventTitle = targetData.title ?? "";
            return entity;
        }
    }
}