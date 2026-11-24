
using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs;

public class UpdateGudangDto
{
    [Required]
    [MaxLength(100)]
    public string NamaGudang { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Alamat { get; set; } = string.Empty;
}
