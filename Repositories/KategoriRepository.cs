
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Repositories.Interfaces;

namespace TodoApi.Repositories;

public class KategoriRepository : IKategoriRepository
{
    private readonly AppDbContext _context;

    public KategoriRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Kategori>> GetAllAsync()
    {
        return await _context.Kategoris.ToListAsync();
    }

    public async Task<Kategori?> GetByIdAsync(int id)
    {
        return await _context.Kategoris
            .FirstOrDefaultAsync(k => k.Id == id);
    }

    public async Task<Kategori?> GetByIdWithProductsAsync(int id)
    {
        return await _context.Kategoris
            .Include(k => k.Produks)
            .FirstOrDefaultAsync(k => k.Id == id);
    }

    public async Task<Kategori> CreateAsync(Kategori kategori)
    {
        await _context.Kategoris.AddAsync(kategori);
        await _context.SaveChangesAsync();

        return kategori;
    }

    public async Task<bool> ExistsByNameAsync(string namaKategori)
    {
        return await _context.Kategoris
            .AnyAsync(k => k.NamaKategori == namaKategori);
    }
}
