using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Repositories.Interfaces;

namespace TodoApi.Repositories;

public class GudangRepository : IGudangRepository
{
    private readonly AppDbContext _context;

    public GudangRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Gudang>> GetAllAsync()
    {
        return await _context.Gudang
            .ToListAsync();
    }

    public async Task<Gudang?> GetByIdAsync(int id)
    {
        return await _context.Gudang
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Gudang?> GetByIdWithProductsAsync(int id)
    {
        return await _context.Gudang
            .Include(g => g.Produks)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Gudang> CreateAsync(Gudang gudang)
    {
        await _context.Gudang.AddAsync(gudang);

        await _context.SaveChangesAsync();

        return gudang;
    }

    public async Task<Gudang> UpdateAsync(Gudang gudang)
    {
        _context.Gudang.Update(gudang);

        await _context.SaveChangesAsync();

        return gudang;
    }

    public async Task<bool> DeleteAsync(Gudang gudang)
    {
        _context.Gudang.Remove(gudang);

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> ExistsByNameAsync(string namaGudang)
    {
        return await _context.Gudang
            .AnyAsync(g => g.NamaGudang == namaGudang);
    }

    public async Task<bool> HasProductsAsync(int gudangId)
    {
        return await _context.Produks
            .AnyAsync(p => p.GudangId == gudangId);
    }
}