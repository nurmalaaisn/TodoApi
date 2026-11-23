using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Repositories.Interfaces;

public interface IProdukRepository
{
    Task<(List<Produk> Data, int TotalData)> GetAllAsync(
        int page,
        int pageSize,
        string? search,
        decimal? minHarga,
        decimal? maxHarga,
        int? minStok,
        string? sortBy,
        string? sortOrder
    );

    Task<Produk?> GetByIdAsync (int id);
    Task<Produk?> GetByIdWithCategoriesAsync(int id);

    Task<bool> ExistsByNameAsync(
        string namaProduk,
        int? excludId = null
    );

    Task AddAsync(Produk produk);

    void Update(Produk produk);

    void Delete(Produk produk);

    Task SaveChangesAsync();
}
