using System.ComponentModel.DataAnnotations;

namespace tsm_back.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }
        public User Owner { get; set; }
        public string Title { get; set; }
    }
}
