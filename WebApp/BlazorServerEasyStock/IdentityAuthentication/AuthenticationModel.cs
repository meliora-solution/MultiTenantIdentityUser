namespace BlazorServerEasyStock.IdentityAuthentication
{
    public class AuthenticationModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? Username { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
