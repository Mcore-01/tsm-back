using System.ComponentModel.DataAnnotations;

namespace tsm_back.Models
{
    public class Column
    {
        [Key]
        public int Id { get; set; }
        public Board OwnerBoard { get; set; }
        public string Title { get; set; }  
    }
}
