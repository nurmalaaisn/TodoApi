
using TodoApi.Models;

namespace TodoApi.Repositories.Interfaces;

public interface IKategoriRepository
{
    Task<List<Kategori>> GetAllAsync();

    Task<Kategori?> GetByIdAsync(int id);

    Task<Kategori?> GetByIdWithProductsAsync(int id);

    Task<Kategori> CreateAsync(Kategori kategori);

    Task<Kategori> UpdateAsync(Kategori kategori);

    Task<bool> DeleteAsync(Kategori kategori);

    Task<bool> ExistsByNameAsync(
        string namaKategori,
        int? excludeId = null
    );
}
