using meetupsApi.Tests.Domain.Usecase;

namespace meetupsApi.Domain.Usecase
{
    public class SearchEventListUsecase : ISearchEventListUsecase
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