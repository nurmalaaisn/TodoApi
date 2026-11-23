using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Produk> Produks => Set<Produk>();
    public DbSet<Gudang> Gudang => Set<Gudang>();

    public DbSet<Kategori> Kategoris => Set<Kategori>();

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Produk>(entity =>
        {
            entity.HasIndex(p => p.NamaProduk)
            .IsUnique();
            entity.ToTable( table =>
            {
                table.HasCheckConstraint(
                    "CK_Produks_Harga_Positive",
                    "\"Harga\" > 0"
                );

                table.HasCheckConstraint(
                    "CK_Produks_Stok_NonNegative",
                    "\"Stok\" >= 0"
                );
            });
        });
        modelBuilder.Entity<Gudang>()
            .HasMany(g => g.Produks)
            .WithOne(p => p.Gudang)
            .HasForeignKey(p => p.GudangId);

        modelBuilder.Entity<Produk>()
            .HasMany(p => p.Kategoris)
            .WithMany(k => k.Produks)
            .UsingEntity(j => j.ToTable("ProdukKategori"));
    }
}
