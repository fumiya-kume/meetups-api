using System;
using System.Diagnostics;
using Moq;
using Xunit;

namespace meetupsApi.Tests.Domain.Usecase
{
    public class RefreshConnpassDataUsecaseTest
    {
        [Fact]
        void 実行時にコンパスからデータを取得する()
        {
            Mock<IConnpassReadOnlyDataRepository> connpassDataRepositoryMoq = new Mock<IConnpassReadOnlyDataRepository>();
            connpassDataRepositoryMoq.Setup(obj => obj.LoadConnpassData());
            var usecase = new RefreshConnpassDataUsecase(connpassDataRepositoryMoq.Object);
            usecase.execute();
            connpassDataRepositoryMoq.Verify(obj => obj.LoadConnpassData(), Times.Once);
        }
    }
}