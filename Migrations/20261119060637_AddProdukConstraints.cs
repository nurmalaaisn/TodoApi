using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddProdukConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Produks_NamaProduk",
                table: "Produks",
                column: "NamaProduk",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Produks_Harga_Positive",
                table: "Produks",
                sql: "\"Harga\" > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Produks_Stok_NonNegative",
                table: "Produks",
                sql: "\"Stok\" >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Produks_NamaProduk",
                table: "Produks");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Produks_Harga_Positive",
                table: "Produks");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Produks_Stok_NonNegative",
                table: "Produks");
        }
    }
}
