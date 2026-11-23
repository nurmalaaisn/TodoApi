namespace TodoApi.DTOs;

public class GudangResponseDto
{
    public int Id { get; set; }

    public string NamaGudang { get; set; } = string.Empty;

    public string Alamat { get; set; } = string.Empty;
}