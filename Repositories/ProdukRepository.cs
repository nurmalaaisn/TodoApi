using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Repositories.Interfaces;

namespace TodoApi.Repositories;

public class ProdukRepository : IProdukRepository
{
    private readonly AppDbContext _context;

    public ProdukRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(List<Produk> Data, int TotalData)> GetAllAsync(
        int page,
        int pageSize,
        string? search,
        decimal? minHarga,
        decimal? maxHarga,
        int? minStok,
        string? sortBy,
        string? sortOrder)
    {
        var query = _context.Produks.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p =>
                p.NamaProduk.ToLower().Contains(search.ToLower()));
        }

        if (minHarga.HasValue)
            query = query.Where(p => p.Harga >= minHarga.Value);

        if (maxHarga.HasValue)
            query = query.Where(p => p.Harga <= maxHarga.Value);

        if (minStok.HasValue)
            query = query.Where(p => p.Stok >= minStok.Value);

        query = sortBy?.ToLower() switch
        {
            "nama" => sortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(p => p.NamaProduk)
                : query.OrderBy(p => p.NamaProduk),

            "harga" => sortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(p => p.Harga)
                : query.OrderBy(p => p.Harga),

            "stok" => sortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(p => p.Stok)
                : query.OrderBy(p => p.Stok),

            _ => query.OrderBy(p => p.Id)
        };

        var totalData = await query.CountAsync();

        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, totalData);
    }

    public async Task<Produk?> GetByIdAsync(int id)
    {
        return await _context.Produks.FindAsync(id);
    }

    public async Task<Produk?> GetByIdWithCategoriesAsync(int id)
    {
        return await _context.Produks
            .Include(p => p.Kategoris)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(
        string namaProduk,
        int? excludeId = null)
    {
        var query = _context.Produks.Where(p =>
            p.NamaProduk.ToLower() == namaProduk.ToLower());

        if (excludeId.HasValue)
        {
            query = query.Where(p => p.Id != excludeId.Value);
        }

        return await query.AnyAsync();
    }

    public async Task AddAsync(Produk produk)
    {
        await _context.Produks.AddAsync(produk);
    }

    public void Update(Produk produk)
    {
        _context.Produks.Update(produk);
    }

    public void Delete(Produk produk)
    {
        _context.Produks.Remove(produk);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
