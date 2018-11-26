using System.Collections.Generic;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;
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

        public async Task<IList<ConnpassEventDataEntity>> Execute(string searchKeyword = "")
        {
            return await _connpassDatabaseRepository.SearchEvent(searchKeyword);
        }
    }
}