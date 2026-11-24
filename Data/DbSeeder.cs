
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var context = scope.ServiceProvider
            .GetRequiredService<AppDbContext>();

        var passwordHasher = scope.ServiceProvider
            .GetRequiredService<IPasswordHasher<User>>();

        var configuration = scope.ServiceProvider
            .GetRequiredService<IConfiguration>();

        var seedUsers = new[]
        {
            new
            {
                Nama = configuration["SeedUsers:Admin:Nama"],
                Email = configuration["SeedUsers:Admin:Email"],
                Password = configuration["SeedUsers:Admin:Password"],
                Role = "Admin"
            },
            new
            {
                Nama = configuration["SeedUsers:User:Nama"],
                Email = configuration["SeedUsers:User:Email"],
                Password = configuration["SeedUsers:User:Password"],
                Role = "User"
            }
        };

        foreach (var seed in seedUsers)
        {
            if (string.IsNullOrWhiteSpace(seed.Nama) ||
                string.IsNullOrWhiteSpace(seed.Email) ||
                string.IsNullOrWhiteSpace(seed.Password))
            {
                throw new InvalidOperationException(
                    $"Konfigurasi seed untuk role {seed.Role} belum lengkap."
                );
            }

            var existingUser = await context.Set<User>()
                .AnyAsync(u => u.Email == seed.Email);

            if (existingUser)
                continue;

            var user = new User
            {
                Nama = seed.Nama,
                Email = seed.Email,
                Role = seed.Role
            };

            user.PasswordHash = passwordHasher.HashPassword(
                user,
                seed.Password
            );

            context.Set<User>().Add(user);
        }

        await context.SaveChangesAsync();
    }
}
