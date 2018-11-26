using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;

namespace meetupsApi.Tests.Domain.Usecase
{
    public interface IConnpassDatabaseRepository
    {
        Task SaveEventData(IEnumerable<ConnpassEventDataEntity> eventDataList);
        Task<IList<ConnpassEventDataEntity>> loadEventList(int count = 300);
        Task<IList<ConnpassEventDataEntity>> SearchEvent(string searchKeyword);
    }
}