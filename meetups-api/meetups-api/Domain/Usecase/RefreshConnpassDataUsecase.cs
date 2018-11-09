namespace meetupsApi.Tests.Domain.Usecase
{
    public class RefreshConnpassDataUsecase
    {
        private IConnpassDataRepository _connpassDataRepository;

        public RefreshConnpassDataUsecase(
            IConnpassDataRepository connpassDataRepository
        )
        {
            _connpassDataRepository = connpassDataRepository;
        }

        public void execute()
        {
            _connpassDataRepository.RefreshData();
        }
    }
}