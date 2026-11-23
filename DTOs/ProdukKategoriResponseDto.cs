namespace TodoApi.DTOs;

public class ProdukKategoriResponseDto
{
    public int Id { get; set; }
    public string NamaProduk { get; set; } = string.Empty;
    public decimal Harga { get; set; }
    public int Stok { get; set; }
    public List<KategoriResponseDto> Kategoris { get; set; } = new();
}