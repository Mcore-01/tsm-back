using Microsoft.EntityFrameworkCore;
using tsm_back.Data;
using tsm_back.Models;

namespace tsm_back.Repositories
{
    public class ColumnRepository
    {
        private readonly TSMContext _context;

        public ColumnRepository(TSMContext context)
        {
            _context = context;
        }
        public IEnumerable<Column> GetAllColumns(int ownerBoardID)
        {
            return _context.Columns
                .Where(p => p.OwnerBoard.Id == ownerBoardID)
                .Include(p => p.OwnerBoard);  
        }
        public async Task<Column> GetColumn(int columnID)
        {
            var column = await _context.Columns
                .Include(p => p.OwnerBoard)
                .FirstOrDefaultAsync(p => p.Id == columnID);

            return column is null ? throw new Exception("Колонка не найдена!") : column;
        }
        public async Task AddColumn(Column column)
        {
            column.OwnerBoard = _context.Boards.First(p => p.Id == column.OwnerBoard.Id);
            _context.Columns.Add(column);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteColumn(int columnID)
        {
            await _context.Columns.Where(p => p.Id == columnID).ExecuteDeleteAsync();
        }
        public async Task UpdateColumn(Column column)
        {
            column.OwnerBoard = _context.Boards.First(p => p.Id == column.OwnerBoard.Id);
            _context.Columns.Update(column);
            await _context.SaveChangesAsync();
        }
    }
}
