using System;
using System.Collections.Generic;
using System.Diagnostics;
using meetupsApi.Domain.Entity;
using meetupsApi.JsonEntity;
using Moq;
using Xunit;

namespace meetupsApi.Tests.Domain.Usecase
{
    public class RefreshConnpassDataUsecaseTest
    {
        [Fact]
        void 実行時にコンパスからデータを取得する()
        {
            var connpassDataRepositoryMoq = new Mock<IConnpassReadOnlyWebsiteDataRepository>();
            var connpassDatabaseRepositoryMoq = new Mock<IConnpassDatabaseRepository>();
            var usecase = new RefreshConnpassDataUsecase(
                connpassDataRepositoryMoq.Object,
                connpassDatabaseRepositoryMoq.Object
            );

            connpassDataRepositoryMoq.Setup(obj => obj.LoadConnpassData(0));

            usecase.execute();

            connpassDataRepositoryMoq.Verify(obj => obj.LoadConnpassData(0), Times.Once);
        }

        [Fact]
        void コンパスから読み込まれたデータをDBに保存する()
        {
            var connpassDataRepositoryMoq = new Mock<IConnpassReadOnlyWebsiteDataRepository>();
            var connpassDatabaseRepositoryMoq = new Mock<IConnpassDatabaseRepository>();
            var usecase = new RefreshConnpassDataUsecase(
                connpassDataRepositoryMoq.Object,
                connpassDatabaseRepositoryMoq.Object
            );

            var dummyData = new List<ConnpassEventDataEntity>();
            var dummyEntity = new ConnpassEventDataEntity
            {
                Id = 0,
                title = "タイトル",
                event_url = "www.yahoo.co.jp",
                Lat = 1.1,
                Lon = 1.1,
                description = "詳細"
            };
            dummyData.Add(dummyEntity);
            connpassDataRepositoryMoq.Setup(obj => obj.LoadConnpassData(0)).ReturnsAsync(dummyData);

            usecase.execute();

            connpassDatabaseRepositoryMoq.Verify(obj => obj.SaveEventData(dummyData), Times.Once);
        }
    }
}