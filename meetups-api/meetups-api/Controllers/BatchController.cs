using System;
using System.Threading.Tasks;
using meetupsApi.Tests.Domain.Usecase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace meetups_api.Controllers
{
    [Route("batch")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private IRefreshConnpassDataUsecase _usecase;
        private readonly ILogger _logger;

        public BatchController(
            IRefreshConnpassDataUsecase refreshConnpassDataUsecase,
            ILogger logger
        )
        {
            _usecase = refreshConnpassDataUsecase;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> RefreshEventData()
        {
            try
            {
                await _usecase.execute();
            }
            catch (Exception e)
            {
                return StatusCode(503);
            }

            return StatusCode(201);
        }
    }
}