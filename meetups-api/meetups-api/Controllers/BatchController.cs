using meetupsApi.Tests.Domain.Usecase;
using Microsoft.AspNetCore.Mvc;

namespace meetups_api.Controllers
{
    [Route("batch")]
    [ApiController]
    public class BatchController
    {
        private IRefreshConnpassDataUsecase _usecase;

        BatchController(
            IRefreshConnpassDataUsecase refreshConnpassDataUsecase)
        {
            _usecase = refreshConnpassDataUsecase;
        }
        
        [HttpGet("/RefreshEventData")]
        void RefreshEventData()
        {
            _usecase.execute();
        }
    }
}