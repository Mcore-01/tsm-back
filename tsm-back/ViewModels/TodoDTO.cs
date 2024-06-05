namespace tsm_back.ViewModels
{
    public class TodoDTO
    {
        public int Id { get; set; }
        public int ColumnID { get; set; }
        public required string Creator { get; set; }
        public required string Description { get; set; }
    }
}
