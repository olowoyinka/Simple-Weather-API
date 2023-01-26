namespace Weather.Domain.Interfaces
{
    public interface IUserService
    {
        Task<(HttpStatusCode status, dynamic response)> Register(RegisterRequestDTO model);
        Task<(HttpStatusCode status, dynamic response)> Login(LoginRequestDTO model);
        Task<(HttpStatusCode status, dynamic response)> GetUser(string getEmail);
        Task<(HttpStatusCode status, dynamic response)> RefreshToken(string getEmail, RefreshTokenRequestDTO model);
    }
}