using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Services.Interfaces;

public interface IProdukService
{
    Task<PaginationResponseDto<ProdukResponseDto>> GetAllAsync(
        int page,
        int pageSize,
        string? search,
        decimal? minHarga,
        decimal? maxHarga,
        int? minStok,
        string? sortBy,
        string?sortOrder
    );
    Task<ProdukResponseDto> GetByIdAsync(int id);
    Task<ProdukResponseDto> CreateAsync(CreateProdukDto dto);
    Task UpdateAsync(int id, UpdateProdukDto dto);
    Task DeleteAsync(int id);
    Task AssignKategoriAsync(int produkId, AssignKategoriDto dto);
    Task<ProdukKategoriResponseDto> GetByIdWithCategoriesAsync(int id);
}