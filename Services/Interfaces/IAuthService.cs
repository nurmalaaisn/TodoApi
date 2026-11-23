using TodoApi.DTOs;

namespace TodoApi.Services.Interfaces;

public interface IAuthService
{
    Task<UserResponseDto> RegisterAsync(RegisterDto dto);
    Task<LoginResponseDto> LoginAsync(LoginDto dto);
}