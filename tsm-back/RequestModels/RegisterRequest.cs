namespace tsm_back.RequestModels
{
    public class RegisterRequest
    {
        public required string NickName { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
