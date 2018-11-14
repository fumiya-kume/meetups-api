using System.Threading.Tasks;
using meetupsApi.JsonEntity;
using meetupsApi.Tests.Domain.Usecase;
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
            var connpassDataRepository = new ConnpassReadOnlyDataRepository(dataStoreMoq.Object);
        }

        [Fact]
        void ConnpassDataRepositoryはIConnpassDataRepositoryを実装する()
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var connpassDataRepository = new ConnpassReadOnlyDataRepository(dataStoreMoq.Object);
            var actual = connpassDataRepository as IConnpassReadOnlyDataRepository;
            Assert.NotNull(actual);
        }

        [Fact]
        void データを更新するとデータストアからデータを取得してくる()
        {
            var dataStoreMoq = new Mock<IConnpassDataStore>();
            var dummyConnpassData = new ConnpassMeetupJson();
            dataStoreMoq.Setup(obj => obj.LoadConnpassDataAsync(100)).ReturnsAsync(dummyConnpassData);
            var connpassDataRepository = new ConnpassReadOnlyDataRepository(dataStoreMoq.Object);
            connpassDataRepository.RefreshData();
            dataStoreMoq.Verify(obj => obj.LoadConnpassDataAsync(100), Times.Once);
        }
    }

    public interface IConnpassDataStore
    {
        Task<ConnpassMeetupJson> LoadConnpassDataAsync(int capacity = 100);
    }

    public class ConnpassReadOnlyDataRepository : IConnpassReadOnlyDataRepository
    {
        private IConnpassDataStore _connpassDataStore;

        public ConnpassReadOnlyDataRepository(IConnpassDataStore dataStore)
        {
            _connpassDataStore = dataStore;
        }

        public void RefreshData()
        {
            _connpassDataStore.LoadConnpassDataAsync(100);
        }
    }
}