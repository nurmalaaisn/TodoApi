using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs;

public class CreateGudangDto
{
    [Required]
    [MaxLength(100)]
    public string NamaGudang { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Alamat { get; set; } = string.Empty;
}