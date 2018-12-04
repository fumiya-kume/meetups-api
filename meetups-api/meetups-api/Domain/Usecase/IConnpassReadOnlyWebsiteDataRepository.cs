using System.Collections.Generic;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;

namespace meetupsApi.Domain.Usecase
{
    public interface IConnpassReadOnlyWebsiteDataRepository
    {
        Task<IEnumerable<ConnpassEventDataEntity>> LoadConnpassData(int page = 0);
        Task<IEnumerable<ConnpassEventDataEntity>> LoadSpecificConnpassDataAsync(int start = 1);
    }
}