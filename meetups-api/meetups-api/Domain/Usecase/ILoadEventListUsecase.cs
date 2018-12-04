using System.Collections.Generic;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;

namespace meetupsApi.Domain.Usecase
{
    public interface ILoadEventListUsecase
    {
        Task<List<ConnpassEventDataEntity>> Execute(int count = 1500);
    }
}