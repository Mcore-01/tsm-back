using tsm_back.Models;
using tsm_back.Repositories;
using tsm_back.ViewModels;

namespace tsm_back.Services
{
    public class ColumnService
    {
        private readonly ColumnRepository _repository;
        public ColumnService(ColumnRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<ColumnDTO> GetAllColumns(int boardID)
        {
            return _repository.GetAllColumns(boardID)
                .Select(GetColumnDTO);
        }

        public async Task<ColumnDTO> GetColumn(int columnID)
        {
            return GetColumnDTO(await _repository.GetColumn(columnID));
        }

        public async Task<int> AddColumn(ColumnDTO columnDTO)
        {
            var column = new Column() { OwnerBoard = new Board() { Id = columnDTO.BoardID }, Title = columnDTO.Title };
            await _repository.AddColumn(column);
            return column.Id;
        }

        public async Task UpdateColumn(ColumnDTO columnDTO)
        {
            await _repository.UpdateColumn(new Column() { Id = columnDTO.Id, OwnerBoard = new Board() { Id = columnDTO.BoardID }, Title = columnDTO.Title });
        }

        public async Task DeleteColumn(int columnID)
        {
            await _repository.DeleteColumn(columnID);
        }

        private ColumnDTO GetColumnDTO(Column column) 
        {
            return new ColumnDTO
            {
                Id = column.Id,
                BoardID = column.OwnerBoard.Id,
                Title = column.Title,
            };
        }
    }
}
