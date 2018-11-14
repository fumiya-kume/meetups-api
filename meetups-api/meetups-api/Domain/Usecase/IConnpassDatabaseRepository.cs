using System.Collections.Generic;
using meetupsApi.Domain.Entity;

namespace meetupsApi.Tests.Domain.Usecase
{
    public interface IConnpassDatabaseRepository
    {
        void SaveEventData(IEnumerable<ConnpassEventDataEntity> dummyData);
    }
}