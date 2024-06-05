using tsm_back.Models;
using tsm_back.Repositories;
using tsm_back.ViewModels;

namespace tsm_back.Services
{
    public class TodoService
    {
        private readonly TodoRepository _repository;

        public TodoService(TodoRepository todoRepository)
        {
               _repository = todoRepository;
        }
        public List<TodoDTO> GetAllTodos(int ownerBoardID)
        {
            return _repository.GetAllTodos(ownerBoardID).Select(GetTodoDTO).ToList();
        }

        public async Task<int> AddTodo(TodoDTO todo)
        {
            var newTodo = new Todo() { OwnerColumn = new() { Id = todo.ColumnID }, CreatorNickname = todo.Creator, Description = todo.Description };
            await _repository.AddTodo(newTodo);
            return newTodo.Id;
        }

        public async Task UpdateTodo(TodoDTO todo)
        {
            await _repository.UpdateTodo(new() { Id = todo.Id, OwnerColumn = new() { Id = todo.ColumnID }, CreatorNickname = todo.Creator, Description = todo.Description });
        }

        public async Task DeleteTodo(int todoID)
        {
            await _repository.DeleteTodo(todoID);
        }

        private TodoDTO GetTodoDTO(Todo todo) 
        {
            return new TodoDTO
            {
                Id = todo.Id,
                ColumnID = todo.OwnerColumn.Id,
                Creator = todo.CreatorNickname,
                Description = todo.Description
            };
        }
    }
}
