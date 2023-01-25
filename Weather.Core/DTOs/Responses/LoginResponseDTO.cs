namespace Weather.Core.Responses
{
    public class LoginResponseDTO
    {
        public string token { get; set; }
        public string expiryTime { get; set; }
        public string refreshToken { get; set; }
    }
}