using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;
using meetupsApi.Tests.Domain.Usecase;

namespace meetupsApi.Domain.Usecase
{
    public class LoadEventListUsecase : ILoadEventListUsecase
    {
        private readonly IConnpassDatabaseRepository _connpassDatabaseRepository;

        public LoadEventListUsecase(IConnpassDatabaseRepository connpassDatabaseRepository)
        {
            _connpassDatabaseRepository = connpassDatabaseRepository;
        }

        public async Task<List<ConnpassEventDataEntity>> Execute(int count = 300)
        {
            return (await _connpassDatabaseRepository.loadEventList()).ToList();
        }
    }
}