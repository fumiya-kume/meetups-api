using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;
using meetupsApi.Tests.Domain.Usecase;

namespace meetupsApi.Domain.Usecase.RefreshConnpassDataUsecase
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

        public async Task Execute()
        {
            var data = new List<ConnpassEventDataEntity>();
            for (var i = 0; i < 9; i++)
            {
                var result = await _connpassReadOnlyWebsiteDataRepository.LoadConnpassData(i);
                result.ToList().ForEach(item => data.Add(item));
            }
            await _connpassDatabaseRepository.SaveEventData(data);
        }
    }
}