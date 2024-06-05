using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tsm_back.Models;
using tsm_back.Services;
using tsm_back.ViewModels;

namespace tsm_back.Controllers
{
    [Route("TSM/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodosController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet("todos/{columnID}")]
        [Authorize]
        public IActionResult GetAllTodos(int columnID)
        {
            return Ok(_todoService.GetAllTodos(columnID));
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> AddTodo([FromBody] TodoDTO todo)
        {
            try
            {
                int newTodoID = await _todoService.AddTodo(todo);
                await FillHeader(typeof(Todo), newTodoID, EntityState.Added);
                return Ok(new {todoID = newTodoID});
            }
            catch
            {
                return BadRequest(new { Message = "Ошибка добавления!" });
            }
        }

        

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateTodo([FromBody] TodoDTO todo)
        {
            try
            {
                await _todoService.UpdateTodo(todo);
                await FillHeader(typeof(Todo), todo.Id, EntityState.Modified);
                return Ok();
            }
            catch
            {
                return BadRequest(new { Message = "Ошибка обновления названия задачи!" });
            }
        }

        [HttpDelete("delete/{todoID}")]
        [Authorize]
        public async Task<IActionResult> DeleteTodo(int todoID)
        {
            try
            {
                await _todoService.DeleteTodo(todoID);
                await FillHeader(typeof(Todo), todoID, EntityState.Deleted);
                return Ok();
            }
            catch
            {
                return BadRequest(new { Message = "Задачи уже нет, либо она была удалена!" });
            }
        }

        private async Task FillHeader(Type entityType, int entityID, EntityState operation)
        {
            await Task.Run(() =>
            {
                var headers = HttpContext.Response.Headers;
                headers.Append("entity", entityType.Name);
                headers.Append("entityID", entityID.ToString());
                headers.Append("operation", operation.ToString());
            });
        }
    }
}
