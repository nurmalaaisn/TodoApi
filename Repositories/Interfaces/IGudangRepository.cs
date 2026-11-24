
using TodoApi.Models;

namespace TodoApi.Repositories.Interfaces;

public interface IGudangRepository
{
    Task<List<Gudang>> GetAllAsync();

    Task<Gudang?> GetByIdAsync(int id);

    Task<Gudang?> GetByIdWithProductsAsync(int id);

    Task<Gudang> CreateAsync(Gudang gudang);

    Task<Gudang> UpdateAsync(Gudang gudang);

    Task<bool> DeleteAsync(Gudang gudang);

    Task<bool> ExistsByNameAsync(string namaGudang);

    Task<bool> HasProductsAsync(int gudangId);
}