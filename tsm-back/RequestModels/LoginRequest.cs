namespace tsm_back.RequestModels
{
    public class LoginRequest
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
