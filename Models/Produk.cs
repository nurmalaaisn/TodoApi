using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

public class Produk
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string NamaProduk { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "999999999")]
    public decimal Harga { get; set; }

    [Range(0, int.MaxValue)]
    public int Stok { get; set; }
}