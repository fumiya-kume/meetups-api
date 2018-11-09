using System.Diagnostics;
using Xunit;

namespace meetupsApi.Tests.Domain.Usecase
{
    public class RefreshConnpassDataUsecaseTest
    {
        [Fact]
        void コンパスのデータを更新取得するユースケースが存在する()
        {
            var usecase = new RefreshConnpassDataUsecase();
            Assert.NotNull(usecase);
        }
    }

    class RefreshConnpassDataUsecase{}
}