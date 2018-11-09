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
    }

    public class ConnpassDataRepository
    {
    }
}