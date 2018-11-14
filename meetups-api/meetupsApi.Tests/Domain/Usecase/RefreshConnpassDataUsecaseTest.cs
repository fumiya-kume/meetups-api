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
            var connpassDataRepositoryMoq = new Mock<IConnpassReadOnlyDataRepository>();
            var connpassDatabaseRepositoryMoq = new Mock<IConnpassDatabaseRepository>();
            var usecase = new RefreshConnpassDataUsecase(
                connpassDataRepositoryMoq.Object,
                connpassDatabaseRepositoryMoq.Object
            );

            connpassDataRepositoryMoq.Setup(obj => obj.LoadConnpassData());
            
            usecase.execute();
            
            connpassDataRepositoryMoq.Verify(obj => obj.LoadConnpassData(), Times.Once);
        }

        [Fact]
        void コンパスから読み込まれたデータをDBに保存する()
        {
            var connpassDataRepositoryMoq = new Mock<IConnpassReadOnlyDataRepository>();
            var connpassDatabaseRepositoryMoq = new Mock<IConnpassDatabaseRepository>();
            var usecase = new RefreshConnpassDataUsecase(
                connpassDataRepositoryMoq.Object,
                connpassDatabaseRepositoryMoq.Object
            );

            var dummyData = new List<ConnpassEventDataEntity>();
            var dummyEntity = new ConnpassEventDataEntity
            {
                Id = 0,
                EventTitle = "タイトル",
                EventUrl = "www.yahoo.co.jp",
                Lat = 1.1,
                Lon = 1.1,
                EventDescription = "詳細"
            };
            dummyData.Add(dummyEntity);
            connpassDataRepositoryMoq.Setup(obj => obj.LoadConnpassData()).ReturnsAsync(dummyData);
            
            usecase.execute();
            
            connpassDatabaseRepositoryMoq.Verify(obj => obj.SaveEventData(dummyData),Times.Once);
            
        }
    }
}