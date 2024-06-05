using System.ComponentModel.DataAnnotations;

namespace tsm_back.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        public Column OwnerColumn { get; set; }
        public string Description { get; set; }
        public string CreatorNickname { get; set; }
        public DateTime CreatedDate { get; set;}

    }
}
