using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Exceptions;
using TodoApi.Services.Interfaces;

namespace TodoApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProdukController : ControllerBase
{
    private readonly IProdukService _produkService;

    public ProdukController(IProdukService produkService)
    {
        _produkService = produkService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] String? search = null,
        [FromQuery] decimal? minHarga = null,
        [FromQuery] decimal? maxHarga = null,
        [FromQuery] int? minStok = null,
        [FromQuery] String? sortBy = null,
        [FromQuery] String? sortOrder = "asc"
    )
    {
        if (page < 1 || pageSize < 1 || pageSize > 100)
        {
            return BadRequest(new
            {
                message = "Page minimal 1 dan PageSize harus antara 1 sampai 100."
            });
        }

        if (minHarga < 0 || maxHarga < 0 || minStok < 0)
        {
            return BadRequest(new
            {
                message = "Filter harga dan stok tidak boleh negatif."
            });
        }

        if (minHarga.HasValue && maxHarga.HasValue && minHarga.Value > maxHarga.Value)
        {
            return BadRequest(new
            {
                message = "Harga minimum tidak boleh melebihi harga maximum."
            });
        }


        var produk = await _produkService.GetAllAsync(
            page,
            pageSize,
            search,
            minHarga,
            maxHarga,
            minStok,
            sortBy,
            sortOrder
            );

        return Ok(produk);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var produk = await _produkService.GetByIdAsync(id);
        return Ok(produk);
    }

    [HttpGet("{id:int}/kategori")]
    public async Task<IActionResult> GetByIdWithCategories(int id)
    {
        var result = await _produkService.GetByIdWithCategoriesAsync(id);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        throw new Exception("Simulasi kesalahan server.");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("test-not-found")]
    public IActionResult TestNotFound()
    {
        throw new NotFoundException("Produk tidak ditemukan.");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("test-bad-request")]
    public IActionResult TestBadRequest()
    {
        throw new BadRequestException("Data yang dikirim tidak valid.");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("test-conflict")]
    public IActionResult TestConflict()
    {
        throw new ConflictException("Data yang dikirim konflik dengan data yang sudah ada.");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateProdukDto dto)
    {
        var produk = await _produkService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = produk.Id },
            produk
        );
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProdukDto dto)
    {
        await _produkService.UpdateAsync(id, dto);

        return Ok(new
        {
            message = "Produk berhasil diperbarui."
        });
    }


    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}/kategori")]
    public async Task<IActionResult> AssignKategori(
        int id,
        AssignKategoriDto dto)
    {
        await _produkService.AssignKategoriAsync(id, dto);

        return Ok(new
        {
            message = "Kategori berhasil dihubungkan dengan produk."
        });
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _produkService.DeleteAsync(id);

        return Ok(new
        {
            message = "Produk berhasil dihapus."
        });
    }
}
