using tsm_back.Models;

namespace tsm_back.ViewModels
{
    public class ColumnDTO
    {
        public int Id { get; set; }
        public int BoardID { get; set; }
        public required string Title { get; set; }
    }
}
