using TodoApi.DTOs;
using TodoApi.Exceptions;
using TodoApi.Models;
using TodoApi.Repositories.Interfaces;
using TodoApi.Services.Interfaces;

namespace TodoApi.Services;

public class ProdukService : IProdukService
{
    private readonly IProdukRepository _produkRepository;
    private readonly IGudangRepository _gudangRepository;
    private readonly IKategoriRepository _kategoriRepository;

    public ProdukService(
        IProdukRepository produkRepository,
        IGudangRepository gudangRepository,
        IKategoriRepository kategoriRepository)
    {
        _produkRepository = produkRepository;
        _gudangRepository = gudangRepository;
        _kategoriRepository = kategoriRepository;
    }


    public async Task<PaginationResponseDto<ProdukResponseDto>> GetAllAsync(
        int page,
        int pageSize,
        string? search,
        decimal? minHarga,
        decimal? maxHarga,
        int? minStok,
        string? sortBy,
        string? sortOrder)
    {
        var (data, totalData) = await _produkRepository.GetAllAsync(
            page,
            pageSize,
            search,
            minHarga,
            maxHarga,
            minStok,
            sortBy,
            sortOrder
        );

        var produkResponse = data.Select(p => new ProdukResponseDto
        {
            Id = p.Id,
            NamaProduk = p.NamaProduk,
            Harga = p.Harga,
            Stok = p.Stok
        }).ToList();

        return new PaginationResponseDto<ProdukResponseDto>
        {
            Data = produkResponse,
            CurrentPage = page,
            PageSize = pageSize,
            TotalData = totalData,
            TotalPages = (int)Math.Ceiling((double)totalData / pageSize)
        };
    }

    public async Task<ProdukResponseDto> GetByIdAsync(int id)
    {
        var produk = await _produkRepository.GetByIdAsync(id);

        if (produk == null)
            throw new NotFoundException("Produk tidak ditemukan.");

        return MapToResponse(produk);
    }

    public async Task<ProdukKategoriResponseDto> GetByIdWithCategoriesAsync(int id)
{
    var produk = await _produkRepository.GetByIdWithCategoriesAsync(id);

    if (produk == null)
        throw new NotFoundException("Produk tidak ditemukan.");

    return new ProdukKategoriResponseDto
    {
        Id = produk.Id,
        NamaProduk = produk.NamaProduk,
        Harga = produk.Harga,
        Stok = produk.Stok,
        Kategoris = produk.Kategoris.Select(k => new KategoriResponseDto
        {
            Id = k.Id,
            NamaKategori = k.NamaKategori
        }).ToList()
    };
}

    public async Task<ProdukResponseDto> CreateAsync(CreateProdukDto dto)
    {
        var exists = await _produkRepository.ExistsByNameAsync(dto.NamaProduk);

        if (exists)
            throw new ConflictException(
                "Produk dengan nama tersebut sudah tersedia."
            );

        var produk = new Produk
        {
            NamaProduk = dto.NamaProduk,
            Harga = dto.Harga,
            Stok = dto.Stok
        };

        await _produkRepository.AddAsync(produk);
        await _produkRepository.SaveChangesAsync();

        return MapToResponse(produk);
    }


    public async Task UpdateAsync(int id, UpdateProdukDto dto)
    {
        var produk = await _produkRepository.GetByIdAsync(id);

        if (produk == null)
            throw new NotFoundException(
                "Produk yang ingin diperbarui tidak ditemukan."
            );

        var gudang = await _gudangRepository.GetByIdAsync(dto.GudangId);

        if (gudang == null)
            throw new NotFoundException(
                "Gudang tujuan tidak ditemukan."
            );

        var exists = await _produkRepository.ExistsByNameAsync(
            dto.NamaProduk,
            id
        );

        if (exists)
            throw new ConflictException(
                "Nama produk telah digunakan oleh produk lain."
            );

        produk.NamaProduk = dto.NamaProduk;
        produk.Harga = dto.Harga;
        produk.Stok = dto.Stok;
        produk.GudangId = dto.GudangId;

        _produkRepository.Update(produk);
        await _produkRepository.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var produk = await _produkRepository.GetByIdAsync(id);

        if (produk == null)
            throw new NotFoundException(
                "Produk yang ingin dihapus tidak ditemukan."
            );

        _produkRepository.Delete(produk);
        await _produkRepository.SaveChangesAsync();
    }

    private static ProdukResponseDto MapToResponse(Produk produk)
    {
        return new ProdukResponseDto
        {
            Id = produk.Id,
            NamaProduk = produk.NamaProduk,
            Harga = produk.Harga,
            Stok = produk.Stok
        };
    }
public async Task AssignKategoriAsync(
    int produkId,
    AssignKategoriDto dto)
{
    var produk = await _produkRepository
        .GetByIdWithCategoriesAsync(produkId);

    if (produk == null)
        throw new NotFoundException("Produk tidak ditemukan.");

    var kategoriIds = dto.KategoriIds.Distinct().ToList();

    foreach (var kategoriId in kategoriIds)
    {
        var kategori = await _kategoriRepository
            .GetByIdAsync(kategoriId);

        if (kategori == null)
            throw new NotFoundException(
                $"Kategori dengan ID {kategoriId} tidak ditemukan.");

        var alreadyAssigned = produk.Kategoris
            .Any(k => k.Id == kategoriId);

        if (!alreadyAssigned)
        {
            produk.Kategoris.Add(kategori);
        }
    }

    _produkRepository.Update(produk);
    await _produkRepository.SaveChangesAsync();
}

}
