using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs;

public class CreateProdukDto
{
    [Required]
    [MaxLength(100)]
    public string NamaProduk { get; set; } = string.Empty;

    [Range(0.01, 999999999)]
    public decimal Harga { get; set; }

    [Range(0, int.MaxValue)]
    public int Stok { get; set; }

    [Range(1, int.MaxValue)]
    public int GudangId { get; set; }
}