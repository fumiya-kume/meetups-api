using meetupsApi.Tests.Domain.Usecase;
using Microsoft.AspNetCore.Mvc;

namespace meetups_api.Controllers
{
    [Route("batch")]
    [ApiController]
    public class BatchController
    {
        private IRefreshConnpassDataUsecase _usecase;
        
        [HttpGet("/RefreshEventData")]
        void RefreshEventData()
        {
            _usecase.execute();
        }
    }
}