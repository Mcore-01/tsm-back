using System.ComponentModel.DataAnnotations;

namespace tsm_back.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
