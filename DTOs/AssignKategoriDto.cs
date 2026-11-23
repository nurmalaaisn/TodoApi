using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs;

public class AssignKategoriDto
{
    [Required]
    [MinLength(1)]
    public List<int> KategoriIds { get; set; } = new();
}
