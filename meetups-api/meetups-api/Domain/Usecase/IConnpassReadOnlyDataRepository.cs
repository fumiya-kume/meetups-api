using System.Collections.Generic;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;
using meetupsApi.JsonEntity;

namespace meetupsApi.Tests.Domain.Usecase
{
    public interface IConnpassReadOnlyDataRepository
    {
        Task<IEnumerable<ConnpassEventDataEntity>> LoadConnpassData();
    }
}