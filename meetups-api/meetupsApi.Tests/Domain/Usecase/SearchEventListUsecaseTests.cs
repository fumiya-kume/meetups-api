using Xunit;

namespace meetupsApi.Tests.Domain.Usecase
{
    public class SearchEventListUsecaseTests
    {
        [Fact]
        void Usecaseはexecute関数を一つ持っている()
        {
            var usecase = new SearchEventListUsecase();
            usecase.Execute();
        }
    }

    class SearchEventListUsecase
    {
        public void Execute()
        {
            
        }
    }
}