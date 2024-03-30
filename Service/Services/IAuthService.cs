using Shared.Dto;
using Shared.Models;

namespace Service.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> SignIn(LoginDto loginDto);
        Task<User?> SignUp(RegisterDto registerDto);
    }
}
