namespace Weather.Core.Requests
{
    public class RefreshTokenRequestDTO
    {
        [Required]
        public string refreshToken { get; set; }
    }
}