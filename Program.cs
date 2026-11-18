using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Services;
using TodoApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string tidak ditemukan."
    );

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IProdukService, ProdukService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

// app.MapGet("/", () => "Hello World!");
// app.MapGet("/about", () => "Day One Prakerin di 3PM Solution.");

// app.MapGet("/hello/{nama}", (string nama) =>
// {
//     return $"Halo, {nama}! Selamat belajar ASP.NET Core.";
// });

// app.MapGet("/hitung", () =>
// {
//     int angka1 = 10;
//     int angka2 = 20;

//     int hasil = angka1 + angka2;

//     return $"Hasil Penjualan: {hasil}";
// });

// app.MapGet("/biodata", () =>
// {
//     string nama = "Mala";
//     string jurusan = "Rekayasa Perangkat Lunak";
//     string kelas = "XII";
//     string keahlian = "Backend Developer";

//     return new
//     {
//         Nama = nama,
//         Jurusan = jurusan,
//         Kelas = kelas,
//         Keahlian = keahlian
//     };
// });

// app.MapGet("/perkalian", () =>
// {
//     int angka1 = 15;
//     int angka2 = 5;

//     int hasil = angka1 * angka2;

//     return $"Hasil perkalian: {hasil}";
// });

// app.MapGet("/salam/{nama}", (string nama) =>
// {
//     return$"Selamat datang, {nama}! Semangat belajar C#.";
// });

// app.MapGet("/produk", (string nama, int harga) =>
// {
//     return new
//     {
//         NamaProduk = nama,
//         HargaProduk = harga
//     };
// });

// app.MapGet("/karyawan", (string nama, string divisi, int umur) =>
// {
//     return new
//     {
//         nama,
//         divisi,
//         umur
//     };
// });

// app.MapGet("/status", () =>
// {
//     return $"API berjalan dengan baik!";
// });

// app.MapPost("/karyawan", (Karyawan data) =>
// {
//     return new
//     {
//         Pesan = "Data karyawan berhasil ditambahkan!",
//         Nama = data.Nama,
//         Divisi = data.Divisi,
//         Umur = data.Umur
//     };
// });

// app.MapPost ("/produk", (Produk data) =>
// {
//     return new
//     {
//         Pesan = "Data produk berhasil ditambahkan!",
//         NamaProduk = data.NamaProduk,
//         Harga = data.Harga,
//         Stok = data.Stok
//     };
// });

// app.MapPut ("/karyawan/{id}", (int id, Karyawan data) =>
// {
//     return new
//     {
//         Pesan = "Data Karyawan berhasil diperbarui!",
//         Id = id,
//         Nama = data.Nama,
//         Divisi = data.Divisi,
//         Umur = data.Umur
//     };
// });

// app.MapPatch("/karyawan/{id}", (int id, string divisi) =>
// {
//     return new
//     {
//         Pesan = "Divisi berhasil diperbarui!",
//         Id = id,
//         DivisiBaru = divisi
//     };
// });

// app.MapDelete("/karyawan/{id}", (int id) =>
// {
//     return new
//     {
//         Pesan = "Data karyawan berhasil dihapus!",
//         Id = id
//     };
// });

// app.MapPut("/produk/{id}", (int id, Produk data) =>
// {
//     return new
//     {
//         Pesan = "Data produk berhasil diperbarui!",
//         Id = id,
//         NamaProduk = data.NamaProduk,
//         Harga = data.Harga
//     };
// });

// app.MapPatch("/produk/{id}", (int id, decimal harga) =>
// {
//     return new
//     {
//         Pesan = "Data Harga telah diperbarui!",
//         Id = id,
//         HargaBaru = harga
//     };
// });

// app.MapDelete("/produk/{id}", (int id) =>
// {
//     return new
//     {
//         Pesan = "Data telah dihapus!",
//         Id = id
//     };
// });

// app.MapGet("/total-produk", (decimal harga, int jumlah, IProdukService produkService) =>
// {
//     decimal total = produkService.HitungTotalHarga(harga, jumlah);

//     return new
//     {
//         Hrga = harga,
//         Jumlah = jumlah,
//         Total = total
//     };
// });

app.Run();

// public class Karyawan
// {
//     public string Nama { get; set; } = "";
//     public string Divisi { get; set; } = "";
//     public int Umur { get; set; }
// }

// public class Produk
// {
//     public string NamaProduk { get; set; } = "";
//     public decimal Harga { get; set; }
//     public int Stok { get; set; }
// }

// public interface IProdukService
// {
//     decimal HitungTotalHarga(decimal harga, int jumlah);
// }

// public class ProdukService : IProdukService
// {
//     public decimal HitungTotalHarga(decimal harga, int jumlah)
//     {
//         return harga * jumlah;
//     }
// }
