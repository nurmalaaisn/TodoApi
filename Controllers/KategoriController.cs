using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("{id:int}/detail")]
    public async Task<ActionResult<KategoriDetailResponseDto>> GetDetail(int id)
    {
        var kategori = await _kategoriService.GetDetailAsync(id);

        return Ok(kategori);
    }

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
    int id,
    [FromBody] UpdateKategoriDto dto)
    {
        var kategori = await _kategoriService.UpdateAsync(id, dto);

        return Ok(new
        {
            message = "Kategori berhasil diperbarui.",
            data = kategori
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _kategoriService.DeleteAsync(id);

        return Ok(new
        {
            message = "Kategori berhasil dihapus."
        });
    }

}