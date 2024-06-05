using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using tsm_back.Data;
using tsm_back.Models;

namespace tsm_back.Repositories
{
    public class BoardRepository
    {
        private readonly TSMContext _context;

        public BoardRepository(TSMContext context)
        {
            _context = context;
        }
        public IEnumerable<Board> GetAllBoards(int userID)
        {
            return _context.Boards
                .Where(p => p.Owner.Id == userID);
        }

        public async Task<Board> GetBoardAsync(int userID, int id)
        {
            var board = await _context.Boards
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (board is null || board.Owner.Id != userID)
                throw new Exception("Доска не найдена!");

            return board;
        }
        public async Task AddBoard(Board board)
        {
            board.Owner = await _context.Users.FirstAsync(p => p.Id == board.Owner.Id);
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteBoard(int boardID)
        {
            await _context.Boards.Where(p => p.Id == boardID).ExecuteDeleteAsync();
        }
        public async Task UpdateBoard(int boardID, string title)
        {
            var board = _context.Boards.FirstOrDefault(p => p.Id == boardID);
            if (board is null)
                return;
            board.Title = title;
            await _context.SaveChangesAsync();
        }
    }
}
