using tsm_back.Models;
using tsm_back.Repositories;
using tsm_back.ViewModels;

namespace tsm_back.Services
{
    public class BoardService
    {
        private readonly BoardRepository _repository;

        public BoardService(BoardRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<BoardDTO> GetAllBoards(int userID)
        {
            return _repository
                .GetAllBoards(userID)
                .Select(GetBoardDTO);
        }
        public async Task<BoardDTO> GetBoardAsync(int userID, int id)
        {
            return GetBoardDTO(await _repository.GetBoardAsync(userID, id));
        }
        public async Task<int> AddBoard(BoardDTO boardDTO) 
        {
            var board = new Board() { Owner = new User() { Id = boardDTO.UserID }, Title = boardDTO.Title };
            await _repository.AddBoard(board);

            return board.Id;    
        }
        public async Task UpdateBoard(BoardDTO board)
        {
            await _repository.UpdateBoard(board.Id, board.Title);
        }
        public async Task DeleteBoard(int boardID)
        {
            await _repository.DeleteBoard(boardID);
        }
        private BoardDTO GetBoardDTO(Board board) 
        {
            return new BoardDTO
            {
                Id = board.Id,
                Title = board.Title
            };
        }

    }
}
