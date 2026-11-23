
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Services.Interfaces;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KategoriController : ControllerBase
{
    private readonly IKategoriService _kategoriService;

    public KategoriController(IKategoriService kategoriService)
    {
        _kategoriService = kategoriService;
    }

    [HttpGet]
    public async Task<ActionResult<List<KategoriResponseDto>>> GetAll()
    {
        var kategoris = await _kategoriService.GetAllAsync();

        return Ok(kategoris);
    }

    [HttpPost]
    public async Task<ActionResult<KategoriResponseDto>> Create(
        CreateKategoriDto dto)
    {
        var kategori = await _kategoriService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetDetail),
            new { id = kategori.Id },
            kategori
        );
    }

    [HttpGet("{id:int}/detail")]
    public async Task<ActionResult<KategoriDetailResponseDto>> GetDetail(int id)
    {
        var kategori = await _kategoriService.GetDetailAsync(id);

        return Ok(kategori);
    }
}
