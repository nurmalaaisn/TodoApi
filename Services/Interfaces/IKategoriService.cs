
using TodoApi.DTOs;

namespace TodoApi.Services.Interfaces;

public interface IKategoriService
{
    Task<List<KategoriResponseDto>> GetAllAsync();

    Task<KategoriResponseDto> CreateAsync(CreateKategoriDto dto);

    Task<KategoriDetailResponseDto> GetDetailAsync(int id);
}
