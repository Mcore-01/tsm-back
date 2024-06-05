using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tsm_back.Services;
using tsm_back.ViewModels;

namespace tsm_back.Controllers
{
    [Route("TSM/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly BoardService _boardService;
        public BoardsController(BoardService boardService)
        {
            _boardService = boardService;
        }
        [HttpGet("boards")]
        [Authorize]
        public IActionResult GetAllBoards([FromHeader] int userID)
        {
            var list = _boardService.GetAllBoards(userID);
            return Ok(list);
        }

        [HttpGet("board/{id}")]
        [Authorize]
        public async Task<IActionResult> GetBoardAsync([FromHeader] int userID, int id)
        {
            try
            {
                var board = await _boardService.GetBoardAsync(userID, id);
                return Ok(board);
            }
            catch(Exception e)
            {
                return NotFound(new { Message = e.Message});
            }
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> AddBoard([FromBody] BoardDTO board)
        {
            try
            {
                int boardID = await _boardService.AddBoard(board);
                return Ok(new {id = boardID});
            }
            catch
            {
                return BadRequest(new { Message = "Ошибка добавления!" });
            }
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateBoard([FromBody] BoardDTO board)
        {
            try
            {
                await _boardService.UpdateBoard(board);
                return Ok();
            }
            catch
            {
                return BadRequest(new { Message = "Ошибка обновления названия доски!" });
            }
        }

        [HttpDelete("delete/{boardID}")]
        [Authorize]
        public async Task<IActionResult> DeleteBoard(int boardID)
        {
            try
            {
                await _boardService.DeleteBoard(boardID);
                return Ok();
            }
            catch
            {
                return BadRequest(new { Message = "Доски уже нет, либо она была удалена!" });
            }
        }
    }
}
