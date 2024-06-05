using Microsoft.EntityFrameworkCore;
using tsm_back.Data;
using tsm_back.Models;

namespace tsm_back.Repositories
{
    public class TodoRepository
    {
        private readonly TSMContext _context;

        public TodoRepository(TSMContext context)
        {
            _context = context;
        }
        public List<Todo> GetAllTodos(int ownerColumnID)
        {
            return _context.Todos
                .Where(p => p.OwnerColumn.Id == ownerColumnID)
                .Include(p => p.OwnerColumn)
                .ToList();
        }
        public async Task AddTodo(Todo todo)
        {
            todo.OwnerColumn = await _context.Columns.FirstAsync(p => p.Id == todo.OwnerColumn.Id);
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTodo(int todoID)
        {
            await _context.Todos
                .Where(p => p.Id == todoID)
                .ExecuteDeleteAsync(); 
        }
        public async Task UpdateTodo(Todo todo)
        {
            todo.OwnerColumn = await _context.Columns.FirstAsync(p => p.Id == todo.OwnerColumn.Id);
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
        }
    }
}
