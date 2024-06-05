namespace tsm_back.ViewModels
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public required string UserName { get; set; }
        public required string Token { get; set; }   
    }
}
