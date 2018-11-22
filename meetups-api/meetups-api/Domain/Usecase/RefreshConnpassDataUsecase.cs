using System.Linq;
using System.Threading.Tasks;

namespace meetupsApi.Tests.Domain.Usecase
{
    public class RefreshConnpassDataUsecase : IRefreshConnpassDataUsecase
    {
        private IConnpassReadOnlyWebsiteDataRepository _connpassReadOnlyWebsiteDataRepository;
        private readonly IConnpassDatabaseRepository _connpassDatabaseRepository;

        public RefreshConnpassDataUsecase(
            IConnpassReadOnlyWebsiteDataRepository connpassReadOnlyWebsiteDataRepository,
            IConnpassDatabaseRepository connpassDatabaseRepository
        )
        {
            _connpassReadOnlyWebsiteDataRepository = connpassReadOnlyWebsiteDataRepository;
            _connpassDatabaseRepository = connpassDatabaseRepository;
        }

        public async Task execute()
        {
            var data = await _connpassReadOnlyWebsiteDataRepository.LoadConnpassData();
            _connpassDatabaseRepository.SaveEventData(data);
        }
    }
}