
using TodoApi.Models;

namespace TodoApi.Repositories.Interfaces;

public interface IGudangRepository
{
    Task<List<Gudang>> GetAllAsync();

    Task<Gudang?> GetByIdAsync(int id);

    Task<Gudang?> GetByIdWithProductsAsync(int id);

    Task<Gudang> CreateAsync(Gudang gudang);

    Task<bool> ExistsByNameAsync(string namaGudang);
}
