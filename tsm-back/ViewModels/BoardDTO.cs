using System.ComponentModel.DataAnnotations;
using tsm_back.Models;

namespace tsm_back.ViewModels
{
    public class BoardDTO
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public required string Title { get; set; }
    }
}
