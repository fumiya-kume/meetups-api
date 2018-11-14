using System.Threading.Tasks;
using meetupsApi.JsonEntity;

namespace meetupsApi.Tests.Domain.Usecase
{
    public interface IConnpassReadOnlyDataRepository
    {
        Task<ConnpassMeetupJson> LoadConnpassData();
    }
}