using System.Threading.Tasks;
using meetupsApi.JsonEntity;

namespace meetupsApi.Tests.Domain.Repository
{
    public interface IConnpassDataStore
    {
        Task<ConnpassMeetupJson> LoadConnpassDataAsync(int capacity = 100, int page = 0);
    }
}