namespace TodoApi.DTOs;

public class GudangDetailResponseDto
{
    public int Id { get; set; }

    public string NamaGudang { get; set; } = string.Empty;

    public string Alamat { get; set; } = string.Empty;

    public List<ProdukGudangResponseDto> Produks { get; set; } = new();
}