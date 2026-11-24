using TodoApi.DTOs;

namespace TodoApi.Services.Interfaces;

public interface IGudangService
{
    Task<List<GudangResponseDto>> GetAllAsync();
    Task<GudangResponseDto> GetByIdAsync(int id);
    Task<GudangDetailResponseDto> GetDetailAsync(int id);
    Task<GudangResponseDto> CreateAsync(CreateGudangDto dto);
    Task<GudangResponseDto> UpdateAsync(int id, UpdateGudangDto dto);
    Task DeleteAsync(int id);
}