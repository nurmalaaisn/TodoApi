
using TodoApi.DTOs;
using TodoApi.Exceptions;
using TodoApi.Models;
using TodoApi.Repositories.Interfaces;
using TodoApi.Services.Interfaces;

namespace TodoApi.Services;

public class GudangService : IGudangService
{
    private readonly IGudangRepository _gudangRepository;

    public GudangService(IGudangRepository gudangRepository)
    {
        _gudangRepository = gudangRepository;
    }

    public async Task<List<GudangResponseDto>> GetAllAsync()
    {
        var gudangs = await _gudangRepository.GetAllAsync();

        return gudangs.Select(g => new GudangResponseDto
        {
            Id = g.Id,
            NamaGudang = g.NamaGudang,
            Alamat = g.Alamat
        }).ToList();
    }

    public async Task<GudangResponseDto> GetByIdAsync(int id)
    {
        var gudang = await _gudangRepository.GetByIdAsync(id);

        if (gudang == null)
        {
            throw new NotFoundException(
                $"Gudang dengan ID {id} tidak ditemukan."
            );
        }

        return new GudangResponseDto
        {
            Id = gudang.Id,
            NamaGudang = gudang.NamaGudang,
            Alamat = gudang.Alamat
        };
    }

    public async Task<GudangResponseDto> CreateAsync(CreateGudangDto dto)
    {
        var exists = await _gudangRepository.ExistsByNameAsync(
            dto.NamaGudang
        );

        if (exists)
        {
            throw new ConflictException("Nama gudang sudah digunakan.");
        }

        var gudang = new Gudang
        {
            NamaGudang = dto.NamaGudang,
            Alamat = dto.Alamat
        };

        var createdGudang = await _gudangRepository.CreateAsync(gudang);

        return new GudangResponseDto
        {
            Id = createdGudang.Id,
            NamaGudang = createdGudang.NamaGudang,
            Alamat = createdGudang.Alamat
        };
    }

    public async Task<GudangDetailResponseDto> GetDetailAsync(int id)
    {
        var gudang = await _gudangRepository.GetByIdWithProductsAsync(id);

        if (gudang == null)
        {
            throw new NotFoundException(
                $"Gudang dengan ID {id} tidak ditemukan."
            );
        }

        return new GudangDetailResponseDto
        {
            Id = gudang.Id,
            NamaGudang = gudang.NamaGudang,
            Alamat = gudang.Alamat,

            Produks = gudang.Produks.Select(p => new ProdukGudangResponseDto
            {
                Id = p.Id,
                NamaProduk = p.NamaProduk,
                Harga = p.Harga,
                Stok = p.Stok
            }).ToList()
        };
    }
}