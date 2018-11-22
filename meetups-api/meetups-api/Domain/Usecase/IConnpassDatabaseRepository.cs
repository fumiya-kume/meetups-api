using System.Collections.Generic;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;

namespace meetupsApi.Tests.Domain.Usecase
{
    public interface IConnpassDatabaseRepository
    {
        Task SaveEventData(IEnumerable<ConnpassEventDataEntity> eventDataList);
    }
}