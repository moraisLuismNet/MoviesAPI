using MoviesAPI.DTOs;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public interface IUserService
    {
        public List<string> Errors { get; }
        Task<IEnumerable<UserDTO>> GetUserService();
        Task<User?> GetByEmailUserService(string email);
        bool ValidateUserService(UserRegistrationDTO dto);
        bool VerifyPasswordUserService(string password, User user);
        Task<UserDTO> AddUserService(UserRegistrationDTO userInsertDTO);
        Task<bool> ChangePasswordUserService(string email, string oldPassword, string newPassword);
        Task<UserDTO> DeleteUserService(string email);
    }
}
