namespace meetupsApi.Tests.Domain.Usecase
{
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