using System.Linq;
using System.Threading.Tasks;

namespace meetupsApi.Tests.Domain.Usecase
{
    public class RefreshConnpassDataUsecase : IRefreshConnpassDataUsecase
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

        public async Task execute()
        {
            var data = await _connpassReadOnlyDataRepository.LoadConnpassData();
            _connpassDatabaseRepository.SaveEventData(data);
        }
    }
}