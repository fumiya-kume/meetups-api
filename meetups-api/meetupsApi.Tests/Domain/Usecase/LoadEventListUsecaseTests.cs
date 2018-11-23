using System.Collections.Generic;
using meetupsApi.Domain.Entity;
using meetupsApi.Domain.Usecase;
using Moq;
using Xunit;

namespace meetupsApi.Tests.Domain.Usecase
{
    public class LoadEventListUsecaseTests
    {
        [Fact]
        void 指定した個数でイベントを読み込もうとする()
        {
            var connpassDatabseRepositoryMock = new Mock<IConnpassDatabaseRepository>();
            var loadEventListUsecase = new LoadEventListUsecase(connpassDatabseRepositoryMock.Object);
            connpassDatabseRepositoryMock.Setup(obj => obj.loadEventList(300))
                .ReturnsAsync(new List<ConnpassEventDataEntity>());
            
            loadEventListUsecase.Execute(100);
            
            connpassDatabseRepositoryMock.Verify(obj => obj.loadEventList(300));
        }
    }
}