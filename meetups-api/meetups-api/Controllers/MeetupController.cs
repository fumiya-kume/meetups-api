using System;
using System.Threading.Tasks;
using meetupsApi.Domain.Usecase;
using Microsoft.AspNetCore.Mvc;

namespace meetups_api.Controllers
{
    public class MeetupController : ControllerBase
    {
        private readonly ILoadEventListUsecase _loadEventListUsecase;

        public MeetupController(ILoadEventListUsecase loadEventListUsecase)
        {
            _loadEventListUsecase = loadEventListUsecase;
        }

        [HttpGet("events")]
        public async Task<IActionResult> LoadEvent([FromQuery]int count = 100)
        {
            try
            {
                var result = await _loadEventListUsecase.Execute(count);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(503);
            }
        }
    }
}