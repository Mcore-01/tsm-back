using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tsm_back.Services;
using tsm_back.ViewModels;

namespace tsm_back.Controllers
{
    [Route("TSM/[controller]")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly ColumnService _columnService;
        public ColumnsController(ColumnService columnService)
        {
            _columnService = columnService;
        }
        [HttpGet("columns/{boardID}")]
        [Authorize]
        public IActionResult GetAllColumns(int boardID)
        {
            var result = _columnService.GetAllColumns(boardID);
            return Ok(result);
        }

        [HttpGet("column/{columnID}")]
        [Authorize]
        public async Task<IActionResult> GetColumn(int columnID)
        {
            var result = await _columnService.GetColumn(columnID);
            if (result is not null)
                return Ok(result);
            else 
                return BadRequest();
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> AddColumn([FromBody] ColumnDTO column)
        {
            try
            {
                int newColumnID = await _columnService.AddColumn(column);
                return Ok(new { columnID = newColumnID });
            }
            catch
            {
                return BadRequest(new { Message = "Ошибка добавления!" });
            }
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateTodo([FromBody] ColumnDTO column)
        {
            try
            {
                await _columnService.UpdateColumn(column);
                return Ok();
            }
            catch
            {
                return BadRequest(new { Message = "Ошибка обновления названия колонки!" });
            }
        }

        [HttpDelete("delete/{columnID}")]
        [Authorize]
        public async Task<IActionResult> DeleteColumn(int columnID)
        {
            try
            {
                await _columnService.DeleteColumn(columnID);
                return Ok();
            }
            catch
            {
                return BadRequest(new { Message = "Колонки уже нет, либо она была удалена!" });
            }
        }
    }
}
