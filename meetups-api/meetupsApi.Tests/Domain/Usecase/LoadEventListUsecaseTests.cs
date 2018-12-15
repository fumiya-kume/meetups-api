using System.Collections.Generic;
using meetupsApi.Domain.Entity;
using meetupsApi.Domain.Usecase;
using meetupsApi.Domain.Usecase.LoadEventList;
using Moq;
using Xunit;

namespace meetupsApi.Tests.Domain.Usecase
{
    public class LoadEventListUsecaseTests
    {
        [Theory]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(1000)]
        void 指定した個数でイベントを読み込もうとする(int count)
        {
            var connpassDatabseRepositoryMock = new Mock<IConnpassDatabaseRepository>();
            var loadEventListUsecase = new LoadEventListUsecase(connpassDatabseRepositoryMock.Object);
            connpassDatabseRepositoryMock.Setup(obj => obj.loadEventList(count))
                .ReturnsAsync(new List<ConnpassEventDataEntity>());

            loadEventListUsecase.Execute(count);

            connpassDatabseRepositoryMock.Verify(obj => obj.loadEventList(count), Times.Once);
        }
        
        [Fact]
        void 数を指定しない場合は1500個読み込まれる()
        {
            int count = 1500;
            var connpassDatabseRepositoryMock = new Mock<IConnpassDatabaseRepository>();
            var loadEventListUsecase = new LoadEventListUsecase(connpassDatabseRepositoryMock.Object);
            connpassDatabseRepositoryMock.Setup(obj => obj.loadEventList(count))
                .ReturnsAsync(new List<ConnpassEventDataEntity>());

            loadEventListUsecase.Execute();
            connpassDatabseRepositoryMock.Verify(obj => obj.loadEventList(count), Times.Once);
        }
       
    }
}