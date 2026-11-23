namespace TodoApi.DTOs;

public class KategoriDetailResponseDto
{
    public int Id { get; set; }
    public string NamaKategori { get; set; } = string. Empty;
    public List<ProdukGudangResponseDto> Produks { get; set; } = new();
}