using System;
using System.Diagnostics;
using Moq;
using Xunit;

namespace meetupsApi.Tests.Domain.Usecase
{
    public class RefreshConnpassDataUsecaseTest
    {
        [Fact]
        void コンパスのデータRepositoryを受け取り処理を進める()
        {
            Mock<IConnpassDataRepository> connpassDataRepositoryMoq = new Mock<IConnpassDataRepository>();
            var usecase = new RefreshConnpassDataUsecase(connpassDataRepositoryMoq.Object);
        }
    }

    public interface IConnpassDataRepository
    {
        void RefreshData();
    }

    public class RefreshConnpassDataUsecase
    {
        private IConnpassDataRepository _connpassDataRepository;

        public RefreshConnpassDataUsecase(
            IConnpassDataRepository connpassDataRepository
        )
        {
            _connpassDataRepository = connpassDataRepository;
        }

        public void execute()
        {
        }
    }
}