using System.Collections.Generic;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;

namespace meetupsApi.Domain.Usecase
{
    public interface ISearchEventListUsecase
    {
        Task<IList<ConnpassEventDataEntity>> Execute(string searchKeyword = "");
    }
}