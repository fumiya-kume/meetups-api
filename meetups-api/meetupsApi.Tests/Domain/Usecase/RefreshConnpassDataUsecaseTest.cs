using System;
using System.Diagnostics;
using Moq;
using Xunit;

namespace meetupsApi.Tests.Domain.Usecase
{
    public class RefreshConnpassDataUsecaseTest
    {
        [Fact]
        void ユースケースが実行されたらコンパスのデータを更新する()
        {
            Mock<IConnpassDataRepository> connpassDataRepositoryMoq = new Mock<IConnpassDataRepository>();
            connpassDataRepositoryMoq.Setup(obj => obj.RefreshData());
            var usecase = new RefreshConnpassDataUsecase(connpassDataRepositoryMoq.Object);
            usecase.execute();
            connpassDataRepositoryMoq.Verify(obj => obj.RefreshData(), Times.Once);
        }
    }
}