using meetupsApi.Tests.Domain.Usecase;
using Xunit;

namespace meetupsApi.Tests.Repository.Data
{
    public class ConnpassDatabaseRepositoryTests
    {
        [Fact]
        void ConnpassDatabaseRepositoryというクラスが存在する()
        {
            var target = new ConnpassDatabaseRepository();
            Assert.NotNull(target);
        }
    }

    internal class ConnpassDatabaseRepository
    {
    }
}