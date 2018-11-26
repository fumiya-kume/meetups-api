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

        [Fact]
        void キーワード実行するとDatabaseRepositoryの検索するメソッドを実行する()
        {
            var connpassDatabaseRepositoryMoq = new Mock<IConnpassDatabaseRepository>();
            var searchKeyword = "hoge";
            connpassDatabaseRepositoryMoq.Setup(obj => obj.SearchEvent(searchKeyword));

            var usecase = new SearchEventListUsecase(connpassDatabaseRepositoryMoq.Object);

            usecase.Execute(searchKeyword);

            connpassDatabaseRepositoryMoq.Verify(obj => obj.SearchEvent(searchKeyword), Times.Once);
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

        public void Execute(string searchKeyword = "")
        {
            _connpassDatabaseRepository.SearchEvent(searchKeyword);
        }
    }
}