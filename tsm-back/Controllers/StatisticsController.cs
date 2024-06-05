using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tsm_back.Services;

namespace tsm_back.Controllers
{
    [Route("TSM/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsService _service;

        public StatisticsController(StatisticsService service)
        {
            _service = service;
        }

        [HttpGet("statistics")]
        [Authorize]
        public IActionResult GetAllLogs([FromHeader] int userID)
        {
            return Ok(_service.GetStatisticLogs(userID));
        }

        [HttpGet("excel")]
        [Authorize]
        public IActionResult ExportExcelStatistic([FromHeader] int userID)
        {
            var result = _service.ExportExcelUserLogs(userID);
            
            if (result is null)
                return NotFound();

            return File(result,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "logs.xlsx");
        }
    }
}
