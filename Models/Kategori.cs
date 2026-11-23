using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

public class Kategori
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string NamaKategori { get; set; } = string.Empty;

    public ICollection<Produk> Produks { get; set; } = new List<Produk>();
}