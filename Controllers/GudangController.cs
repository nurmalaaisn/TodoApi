using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Services.Interfaces;

namespace TodoApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GudangController : ControllerBase
{
    private readonly IGudangService _gudangService;

    public GudangController(IGudangService gudangService)
    {
        _gudangService = gudangService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GudangResponseDto>>> GetAll()
    {
        var gudangs = await _gudangService.GetAllAsync();

        return Ok(gudangs);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GudangResponseDto>> GetById(int id)
    {
        var gudang = await _gudangService.GetByIdAsync(id);

        return Ok(gudang);
    }

    [HttpGet("{id:int}/detail")]
    public async Task<ActionResult<GudangDetailResponseDto>> GetDetail(int id)
    {
        var gudang = await _gudangService.GetDetailAsync(id);

        return Ok(gudang);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<GudangResponseDto>> Create(
        [FromBody] CreateGudangDto dto)
    {
        var gudang = await _gudangService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = gudang.Id },
            gudang
        );
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
    int id,
    [FromBody] UpdateGudangDto dto)
    {
        var gudang = await _gudangService.UpdateAsync(id, dto);

        return Ok(new
        {
            message = "Gudang berhasil diperbarui.",
            data = gudang
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _gudangService.DeleteAsync(id);

        return Ok(new
        {
            message = "Gudang berhasil dihapus."
        });
    }
}
