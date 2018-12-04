using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;
using meetupsApi.Domain.Usecase;
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
            var ConnpassDatastoreMoq = new Mock<IConnpassReadOnlyWebsiteDataRepository>();
            var ConnpassDatabaseRepositoryMoq = new Mock<IConnpassDatabaseRepository>();

            var usecase =
                new RefreshSpecificConnpassEventData(ConnpassDatastoreMoq.Object, ConnpassDatabaseRepositoryMoq.Object);
        }

        [Fact]
        void 指定されないとポジション１から１００個のイベントが読み込まれる()
        {
            var ConnpassDatastoreMoq = new Mock<IConnpassReadOnlyWebsiteDataRepository>();
            var ConnpassDatabaseRepositoryMoq = new Mock<IConnpassDatabaseRepository>();

            var usecase =
                new RefreshSpecificConnpassEventData(ConnpassDatastoreMoq.Object, ConnpassDatabaseRepositoryMoq.Object);

            ConnpassDatastoreMoq.Setup(obj => obj.LoadSpecificConnpassDataAsync(1))
                .ReturnsAsync(new List<ConnpassEventDataEntity>());

            usecase.Execute();

            ConnpassDatastoreMoq.Verify(obj => obj.LoadSpecificConnpassDataAsync(1), Times.Once);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(100)]
        void 読み込み開始場所を指定することができる(int startPosition)
        {
            var ConnpassDatastoreMoq = new Mock<IConnpassReadOnlyWebsiteDataRepository>();
            var ConnpassDatabaseRepositoryMoq = new Mock<IConnpassDatabaseRepository>();

            var usecase =
                new RefreshSpecificConnpassEventData(ConnpassDatastoreMoq.Object, ConnpassDatabaseRepositoryMoq.Object);

            ConnpassDatastoreMoq.Setup(obj => obj.LoadSpecificConnpassDataAsync(startPosition))
                .ReturnsAsync(new List<ConnpassEventDataEntity>());

            usecase.Execute(startPosition);

            ConnpassDatastoreMoq.Verify(obj => obj.LoadSpecificConnpassDataAsync(startPosition), Times.Once);
        }

        [Fact]
        async Task Connpassから読み込んだデータをデータベースに保存している()
        {
            var ConnpassDatastoreMoq = new Mock<IConnpassReadOnlyWebsiteDataRepository>();
            var ConnpassDatabaseRepositoryMoq = new Mock<IConnpassDatabaseRepository>();

            var dummyDataList = new List<ConnpassEventDataEntity>();

            Enumerable.Range(0, 100).Select(i => new ConnpassEventDataEntity()).ToList()
                .ForEach(item => dummyDataList.Add(item));

            ConnpassDatastoreMoq.Setup(obj => obj.LoadSpecificConnpassDataAsync(1)).ReturnsAsync(dummyDataList);
            ConnpassDatabaseRepositoryMoq.Setup(obj => obj.SaveEventData(dummyDataList)).Returns(Task.CompletedTask);

            var usecase =
                new RefreshSpecificConnpassEventData(ConnpassDatastoreMoq.Object, ConnpassDatabaseRepositoryMoq.Object);

            await usecase.Execute();

            ConnpassDatabaseRepositoryMoq.Verify(obj => obj.SaveEventData(dummyDataList),Times.Once);
        }
    }

    class RefreshSpecificConnpassEventData
    {
        private readonly IConnpassReadOnlyWebsiteDataRepository _connpassDataStore;
        private readonly IConnpassDatabaseRepository _connpassDatabaseRepository;

        public RefreshSpecificConnpassEventData(
            IConnpassReadOnlyWebsiteDataRepository connpassDataStore,
            IConnpassDatabaseRepository connpassDatabaseRepository
        )
        {
            _connpassDataStore = connpassDataStore;
            _connpassDatabaseRepository = connpassDatabaseRepository;
        }

        public async Task Execute(int startPosition = 1)
        {
           var loadResult = await _connpassDataStore.LoadSpecificConnpassDataAsync(startPosition);
            await _connpassDatabaseRepository.SaveEventData(loadResult);
        }
    }
}