using MoviesAPI.DTOs;

namespace MoviesAPI.Services
{
    public interface ITokenService
    {
        UserLoginResponseDTO GenerateTokenService(UserLoginDTO user);
    }
}
