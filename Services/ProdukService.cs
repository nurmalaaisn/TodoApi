using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Exceptions;

namespace TodoApi.Services;

public class ProdukService : IProdukService
{
    private readonly AppDbContext _context;

    public ProdukService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaginationResponseDto<ProdukResponseDto>> GetAllAsync(
        int page,
        int pageSize,
        string? search,
        decimal? minHarga,
        decimal? maxHarga,
        int? minStok,
        string? sortBy,
        string? sortOrder
    )
    {
        var query = _context.Produks.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p =>
            p.NamaProduk.ToLower().Contains(search.ToLower())
            );
        }

        if (minHarga.HasValue)
        {
            query = query.Where(p => p.Harga >= minHarga.Value);
        }

        if (maxHarga.HasValue)
        {
            query = query.Where(p => p.Harga <= maxHarga.Value);
        }

        if (minStok.HasValue)
        {
            query = query.Where(p => p.Stok >= minStok.Value);
        }

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

        var produk = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProdukResponseDto
            {
                Id = p.Id,
                NamaProduk = p.NamaProduk,
                Harga = p.Harga,
                Stok = p.Stok
            })
            .ToListAsync();

        return new PaginationResponseDto<ProdukResponseDto>
        {
            Data = produk,
            CurrentPage = page,
            PageSize = pageSize,
            TotalData = totalData,
            TotalPages = (int)Math.Ceiling((double)totalData / pageSize)
        };
    }

    public async Task<ProdukResponseDto> GetByIdAsync(int id)
    {
        var produk = await _context.Produks.FindAsync(id);

        if (produk == null)
        {
            throw new NotFoundException("Produk tidak ditemukan.");
        }

        return new ProdukResponseDto
        {
            Id = produk.Id,
            NamaProduk = produk.NamaProduk,
            Harga = produk.Harga,
            Stok = produk.Stok
        };
    }

    public async Task<ProdukResponseDto> CreateAsync(CreateProdukDto dto)
    {
        var produkSudahAda = await _context.Produks
            .AnyAsync(p => p.NamaProduk.ToLower() == dto.NamaProduk.ToLower());

        if (produkSudahAda)
        {
            throw new ConflictException("Produk dengan nama tersebut sudah tersedia.");
        }

        var produk = new Produk
        {
            NamaProduk = dto.NamaProduk,
            Harga = dto.Harga,
            Stok = dto.Stok
        };

        _context.Produks.Add(produk);

        await _context.SaveChangesAsync();

        return new ProdukResponseDto
        {
            Id = produk.Id,
            NamaProduk = produk.NamaProduk,
            Harga = produk.Harga,
            Stok = produk.Stok
        };
    }

    public async Task UpdateAsync(int id, UpdateProdukDto dto)
    {
        var produk = await _context.Produks.FindAsync(id);

        if (produk == null)
        {
            throw new NotFoundException("Produk yang ingin diperbarui tidak ditemukan.");
        }

        var produkSudahAda = await _context.Produks
            .AnyAsync(p => p.NamaProduk.ToLower() == dto.NamaProduk.ToLower() && p.Id != id);

        if (produkSudahAda)
        {
            throw new ConflictException("Nama produk telah digunakan oleh produk lain."
            );
        }

        produk.NamaProduk = dto.NamaProduk;
        produk.Harga = dto.Harga;
        produk.Stok = dto.Stok;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var produk = await _context.Produks.FindAsync(id);

        if (produk == null)
            {
                throw new NotFoundException("Produk yang ingin dihapus tidak ditemukan.");
            }

        _context.Produks.Remove(produk);

        await _context.SaveChangesAsync();
    }
}
