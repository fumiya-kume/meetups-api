using System;
using meetupsApi.Domain.Entity;
using meetupsApi.Domain.Usecase;
using meetupsApi.JsonEntity;
using meetupsApi.Tests.Domain.Usecase;
using meetupsApi.Tests.Repository;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
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
            var connpassDataRepository = new ConnpassReadOnlyWebsiteWebsiteDataRepository(dataStoreMoq.Object);
        }

        [Fact]
        void ConnpassDataRepositoryはIConnpassDataRepositoryを実装する()
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyWebsiteWebsiteDataRepository(dataStoreMoq.Object);
            var actual = connpassDataRepository as IConnpassReadOnlyWebsiteDataRepository;
            Assert.NotNull(actual);
        }

        [Fact]
        void データを更新するとデータストアからデータを取得してくる()
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var dummyConnpassData = new ConnpassMeetupJson();
            dataStoreMoq.Setup(obj => obj.LoadConnpassDataAsync(100,0)).ReturnsAsync(dummyConnpassData);
            var connpassDataRepository = new ConnpassReadOnlyWebsiteWebsiteDataRepository(dataStoreMoq.Object);
            connpassDataRepository.LoadConnpassData();
            dataStoreMoq.Verify(obj => obj.LoadConnpassDataAsync(100,0), Times.Once);
        }

        [Fact]
        void JsonDataをEntityに変換することができる()
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyWebsiteWebsiteDataRepository(dataStoreMoq.Object);
            var targetData = new ConnpassEvent();
            ConnpassEventDataEntity item = connpassDataRepository.convert(targetData);
        }

        [Fact]
        void 変換されたDomainEntityにはタイトルが保存されている()
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyWebsiteWebsiteDataRepository(dataStoreMoq.Object);
            var dummyTitle = "";
            var targetData = new ConnpassEvent
            {
                title = dummyTitle
            };
            ConnpassEventDataEntity item = connpassDataRepository.convert(targetData);
            Assert.Equal(dummyTitle, item.title);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        void タイトルが存在しない場合はから文字が入っている(string dummyTitle)
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyWebsiteWebsiteDataRepository(dataStoreMoq.Object);

            var targetData = new ConnpassEvent
            {
                title = dummyTitle
            };
            ConnpassEventDataEntity item = connpassDataRepository.convert(targetData);
            Assert.Equal("", item.title);
        }

        [Fact]
        void 変換されたEntityにはURLが保存されている()
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyWebsiteWebsiteDataRepository(dataStoreMoq.Object);
            var dummyEventUrl = "www.yahoo.co.jp";
            var targetData = new ConnpassEvent
            {
                event_url = dummyEventUrl
            };
            ConnpassEventDataEntity item = connpassDataRepository.convert(targetData);
            Assert.Equal(dummyEventUrl, item.event_url);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        void イベントURLが存在しない場合は空文字が入っている(string dummyEventUrl)
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyWebsiteWebsiteDataRepository(dataStoreMoq.Object);
            var targetData = new ConnpassEvent
            {
                title = dummyEventUrl
            };
            ConnpassEventDataEntity item = connpassDataRepository.convert(targetData);
            Assert.Equal("", item.title);
        }

        [Fact]
        void イベント詳細がEntityに含まれる()
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyWebsiteWebsiteDataRepository(dataStoreMoq.Object);
            var dummmyEventDescription = "Hello";
            var targetData = new ConnpassEvent
            {
                description = dummmyEventDescription
            };
            var item = connpassDataRepository.convert(targetData);
            Assert.Equal(dummmyEventDescription, item.description);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        void イベント詳細に不正な値が含まれているときはから文字が返ってくる(string dummmyEventDescription)
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyWebsiteWebsiteDataRepository(dataStoreMoq.Object);

            var targetData = new ConnpassEvent
            {
                description = dummmyEventDescription
            };
            var item = connpassDataRepository.convert(targetData);
            Assert.Equal("", item.description);
        }

        [Fact]
        void イベントの開催される場所がEntityに含まれる()
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyWebsiteWebsiteDataRepository(dataStoreMoq.Object);

            var targetData = new ConnpassEvent
            {
                lon = "123.2",
                lat = "456.4"
            };
            var item = connpassDataRepository.convert(targetData);
            Assert.Equal(targetData.lon, item.Lon.ToString());
            Assert.Equal(targetData.lat, item.Lat.ToString());
        }

        [Fact]
        void イベントIDがEntityに含まれている()
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyWebsiteWebsiteDataRepository(dataStoreMoq.Object);
            var dummyEventId = 123;
            var targetData = new ConnpassEvent
            {
                event_id = dummyEventId
            };
            var item = connpassDataRepository.convert(targetData);
            Assert.Equal(dummyEventId, item.Id);
        }
    }
}