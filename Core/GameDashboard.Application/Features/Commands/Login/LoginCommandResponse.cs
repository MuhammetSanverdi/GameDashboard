namespace GameDashboard.Application.Features.Commands.Login
{
    public class LoginCommandResponse
    {
        public string AccessToken { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
