namespace Mumu_Server.Services
{
    public class TokenGenerationRequest
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
