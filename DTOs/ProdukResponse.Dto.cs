namespace TodoApi.DTOs;

public class ProdukResponseDto
{
    public int Id {get; set; }
    public string NamaProduk { get; set; } = string.Empty;
    public decimal Harga { get; set; }
    public int Stok { get; set; }
}