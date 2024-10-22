namespace JCB_Cinema.Application.Auth
{
    public class LoginResponse
    {
        public string JwtToken { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
