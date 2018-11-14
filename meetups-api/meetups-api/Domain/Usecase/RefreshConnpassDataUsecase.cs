namespace meetupsApi.Tests.Domain.Usecase
{
    public class RefreshConnpassDataUsecase
    {
        private IConnpassReadOnlyDataRepository _connpassReadOnlyDataRepository;
        private readonly IConnpassDatabaseRepository _connpassDatabaseRepository;

        public RefreshConnpassDataUsecase(
            IConnpassReadOnlyDataRepository connpassReadOnlyDataRepository,
            IConnpassDatabaseRepository connpassDatabaseRepository
        )
        {
            _connpassReadOnlyDataRepository = connpassReadOnlyDataRepository;
            _connpassDatabaseRepository = connpassDatabaseRepository;
        }

        public void execute()
        {
            _connpassReadOnlyDataRepository.LoadConnpassData();
        }
    }
}