namespace TodoApi.DTOs;

public class UserResponseDto
{
    public int Id { get; set; }
    public string Nama { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}