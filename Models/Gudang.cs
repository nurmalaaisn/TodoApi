using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

public class Gudang
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string NamaGudang { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Alamat { get; set; } = string.Empty;

    public ICollection<Produk> Produks { get; set;}
        = new List<Produk>();

}