using meetupsApi.JsonEntity;
using meetupsApi.Tests.Domain.Repository;
using Moq;
using Xunit;

namespace meetupsApi.Tests.Domain.Usecase
{
    public class RefreshSpecificConnpassEventDataTests
    {
        [Fact]
        void Connpassからデータを読み込むクラスとデータベースに保存するクラスを必要とする()
        {
            var ConnpassDatastoreMoq = new Mock<IConnpassDataStore>();
            var ConnpassDatabaseRepositoryMoq = new Mock<IConnpassDatabaseRepository>();
            
            var usecase = new RefreshSpecificConnpassEventData(ConnpassDatastoreMoq.Object,ConnpassDatabaseRepositoryMoq.Object);
        }

        [Fact]
        void 指定されないとポジション１から１００個のイベントが読み込まれる()
        {
            var ConnpassDatastoreMoq = new Mock<IConnpassDataStore>();
            var ConnpassDatabaseRepositoryMoq = new Mock<IConnpassDatabaseRepository>();
            
            var usecase = new RefreshSpecificConnpassEventData(ConnpassDatastoreMoq.Object,ConnpassDatabaseRepositoryMoq.Object);

            ConnpassDatastoreMoq.Setup(obj => obj.LoadSpecificConnpassDataAsync(1)).ReturnsAsync(new ConnpassMeetupJson());

            usecase.Execute();
            
            ConnpassDatastoreMoq.Verify(obj => obj.LoadSpecificConnpassDataAsync(1),Times.Once);
        }
    }

    class RefreshSpecificConnpassEventData
    {
        private readonly IConnpassDataStore _connpassDataStore;
        private readonly IConnpassDatabaseRepository _connpassDatabaseRepository;

        public RefreshSpecificConnpassEventData(
            IConnpassDataStore connpassDataStore, 
            IConnpassDatabaseRepository connpassDatabaseRepository
            )
        {
            _connpassDataStore = connpassDataStore;
            _connpassDatabaseRepository = connpassDatabaseRepository;
        }

        public void Execute()
        {
            _connpassDataStore.LoadSpecificConnpassDataAsync();
        }
    }
}