using System.Threading.Tasks;
using meetupsApi.Domain.Usecase;

namespace meetupsApi.Tests.Domain.Usecase
{
    public class RefreshSpecificConnpassEventData
    {
        private readonly IConnpassReadOnlyWebsiteDataRepository _connpassDataStore;
        private readonly IConnpassDatabaseRepository _connpassDatabaseRepository;

        public RefreshSpecificConnpassEventData(
            IConnpassReadOnlyWebsiteDataRepository connpassDataStore,
            IConnpassDatabaseRepository connpassDatabaseRepository
        )
        {
            _connpassDataStore = connpassDataStore;
            _connpassDatabaseRepository = connpassDatabaseRepository;
        }

        public async Task Execute(int startPosition = 1)
        {
            var loadResult = await _connpassDataStore.LoadSpecificConnpassDataAsync(startPosition);
            await _connpassDatabaseRepository.SaveEventData(loadResult);
        }
    }
}