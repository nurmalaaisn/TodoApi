using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs;

public class CreateKategoriDto
{
    [Required]
    [MaxLength(100)]
    public string NamaKategori { get; set; } = string.Empty;
}