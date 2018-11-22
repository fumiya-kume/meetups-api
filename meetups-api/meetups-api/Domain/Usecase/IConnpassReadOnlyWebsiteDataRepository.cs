using System.Collections.Generic;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;
using meetupsApi.JsonEntity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace meetupsApi.Tests.Domain.Usecase
{
    public interface IConnpassReadOnlyWebsiteDataRepository
    {
        Task<IEnumerable<ConnpassEventDataEntity>> LoadConnpassData(int page = 0);
    }
}