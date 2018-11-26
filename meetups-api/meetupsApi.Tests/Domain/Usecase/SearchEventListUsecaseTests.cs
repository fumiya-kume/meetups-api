using System.Runtime.InteropServices;
using Moq;
using Xunit;

namespace meetupsApi.Tests.Domain.Usecase
{
    public class SearchEventListUsecaseTests
    {
        [Fact]
        void Usecaseはexecute関数を一つ持っている()
        {
            var connpassDatabaseRepositoryMoq = new Mock<IConnpassDatabaseRepository>();
            var usecase = new SearchEventListUsecase(connpassDatabaseRepositoryMoq.Object);
            usecase.Execute();
        }

        [Fact]
        void DarabaseRepositoryのインスタンスを利用することができる()
        {
            var connpassDatabaseRepositoryMoq = new Mock<IConnpassDatabaseRepository>();
            var usecase = new SearchEventListUsecase(connpassDatabaseRepositoryMoq.Object);
        }
    }

    class SearchEventListUsecase
    {
        private readonly IConnpassDatabaseRepository _connpassDatabaseRepository;
        public SearchEventListUsecase(
            IConnpassDatabaseRepository connpassDatabaseRepository
            )
        {
            _connpassDatabaseRepository = connpassDatabaseRepository;
        }

        public void Execute()
        {
            
        }
    }
}