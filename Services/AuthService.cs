using Microsoft.AspNetCore.Identity;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Repositories.Interfaces;
using TodoApi.Services.Interfaces;
using TodoApi.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TodoApi.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    public async Task<UserResponseDto> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);

        if (existingUser != null)
        {
            throw new ConflictException("Email sudah terdaftar.");
        }

        var user = new User
        {
            Nama = dto.Nama,
            Email = dto.Email,
            Role = "User"
        };

        user.PasswordHash = _passwordHasher.HashPassword(
            user,
            dto.Password
        );

        await _userRepository.CreateAsync(user);

        return new UserResponseDto
        {
            Id = user.Id,
            Nama = user.Nama,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);

        if (user == null)
        {
            throw new BadRequestException("Email atau password salah.");
        }

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            dto.Password
        );

        if (result == PasswordVerificationResult.Failed)
        {
            throw new BadRequestException("Email atau password salah");
        }

        var jwtSettings = _configuration.GetSection("Jwt");

        var secretKey = jwtSettings["Key"]!;
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        var expiresAt = DateTime.UtcNow.AddMinutes(
            double.Parse(jwtSettings["ExpiresInMinutes"]!)
        );

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Nama),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(secretKey)
    );

    var credentials = new SigningCredentials(
        key,
        SecurityAlgorithms.HmacSha256
    );

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires:expiresAt,
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponseDto
        {
            Token = tokenString,
            ExpiresAt = expiresAt
        };
    }
}