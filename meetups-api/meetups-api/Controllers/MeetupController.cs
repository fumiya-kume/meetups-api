using System;
using System.Threading.Tasks;
using meetupsApi.Domain.Usecase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace meetups_api.Controllers
{
    public class MeetupController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ILoadEventListUsecase _loadEventListUsecase;

        public MeetupController(
            ILoadEventListUsecase loadEventListUsecase,
            ILogger<MeetupController> logger
        )
        {
            _logger = logger;
            _loadEventListUsecase = loadEventListUsecase;
        }

        [HttpGet("events")]
        public async Task<IActionResult> LoadEvent([FromQuery] int count = 100)
        {
            try
            {
                var result = await _loadEventListUsecase.Execute(count);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(503);
            }
        }
    }
}