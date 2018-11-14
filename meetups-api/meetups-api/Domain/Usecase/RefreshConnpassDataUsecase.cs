namespace meetupsApi.Tests.Domain.Usecase
{
    public class RefreshConnpassDataUsecase
    {
        private IConnpassReadOnlyDataRepository _connpassReadOnlyDataRepository;

        public RefreshConnpassDataUsecase(
            IConnpassReadOnlyDataRepository connpassReadOnlyDataRepository
        )
        {
            _connpassReadOnlyDataRepository = connpassReadOnlyDataRepository;
        }

        public void execute()
        {
            _connpassReadOnlyDataRepository.RefreshData();
        }
    }
}