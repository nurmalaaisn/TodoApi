
using TodoApi.DTOs;
using TodoApi.Exceptions;
using TodoApi.Models;
using TodoApi.Repositories.Interfaces;
using TodoApi.Services.Interfaces;

namespace TodoApi.Services;

public class KategoriService : IKategoriService
{
    private readonly IKategoriRepository _kategoriRepository;

    public KategoriService(IKategoriRepository kategoriRepository)
    {
        _kategoriRepository = kategoriRepository;
    }

    public async Task<List<KategoriResponseDto>> GetAllAsync()
    {
        var kategoris = await _kategoriRepository.GetAllAsync();

        return kategoris.Select(k => new KategoriResponseDto
        {
            Id = k.Id,
            NamaKategori = k.NamaKategori
        }).ToList();
    }

    public async Task<KategoriResponseDto> CreateAsync(CreateKategoriDto dto)
    {
        var exists = await _kategoriRepository
            .ExistsByNameAsync(dto.NamaKategori);

        if (exists)
            throw new ConflictException("Nama kategori sudah digunakan.");

        var kategori = new Kategori
        {
            NamaKategori = dto.NamaKategori
        };

        var result = await _kategoriRepository.CreateAsync(kategori);

        return new KategoriResponseDto
        {
            Id = result.Id,
            NamaKategori = result.NamaKategori
        };
    }

    public async Task<KategoriDetailResponseDto> GetDetailAsync(int id)
    {
        var kategori = await _kategoriRepository
            .GetByIdWithProductsAsync(id);

        if (kategori == null)
            throw new NotFoundException("Kategori tidak ditemukan.");

        return new KategoriDetailResponseDto
        {
            Id = kategori.Id,
            NamaKategori = kategori.NamaKategori,
            Produks = kategori.Produks.Select(p => new ProdukGudangResponseDto
            {
                Id = p.Id,
                NamaProduk = p.NamaProduk,
                Harga = p.Harga,
                Stok = p.Stok
            }).ToList()
        };
    }


    public async Task<KategoriResponseDto> UpdateAsync(
        int id,
        UpdateKategoriDto dto)
    {
        var kategori = await _kategoriRepository.GetByIdAsync(id);

        if (kategori == null)
        {
            throw new NotFoundException(
                $"Kategori dengan ID {id} tidak ditemukan."
            );
        }

        var exists = await _kategoriRepository.ExistsByNameAsync(
            dto.NamaKategori,
            id
        );

        if (exists)
        {
            throw new ConflictException(
                "Nama kategori sudah digunakan."
            );
        }

        kategori.NamaKategori = dto.NamaKategori;

        var updatedKategori = await _kategoriRepository.UpdateAsync(kategori);

        return new KategoriResponseDto
        {
            Id = updatedKategori.Id,
            NamaKategori = updatedKategori.NamaKategori
        };
    }


    public async Task DeleteAsync(int id)
    {
        var kategori = await _kategoriRepository.GetByIdAsync(id);

        if (kategori == null)
        {
            throw new NotFoundException(
                $"Kategori dengan ID {id} tidak ditemukan."
            );
        }


        await _kategoriRepository.DeleteAsync(kategori);
    }

}
