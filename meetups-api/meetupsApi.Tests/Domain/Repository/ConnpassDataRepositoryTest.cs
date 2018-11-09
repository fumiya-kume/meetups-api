using meetupsApi.Tests.Domain.Usecase;
using Xunit;

namespace meetupsApi.Tests.Domain.Repository
{
    public class ConnpassDataRepositoryTest
    {
        [Fact]
        void ConnpassDataRepositoryが存在する()
        {
            ConnpassDataRepository connpassDataRepository = new ConnpassDataRepository();
        }

        [Fact]
        void ConnpassDataRepositoryはIConnpassDataRepositoryを実装する()
        {
            var connpassDataRepository = new ConnpassDataRepository();
            var actual = connpassDataRepository as IConnpassDataRepository;
            Assert.NotNull(actual);
        }
    }

    public class ConnpassDataRepository:IConnpassDataRepository
    {
        public void RefreshData()
        {
            throw new System.NotImplementedException();
        }
    }
}