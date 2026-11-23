
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TodoApi.Migrations
{
    public partial class AddUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdukKategori_Kategoris_KategorisId",
                table: "ProdukKategori");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(
                        type: "integer",
                        nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),

                    Nama = table.Column<string>(
                        type: "character varying(100)",
                        maxLength: 100,
                        nullable: false),

                    Email = table.Column<string>(
                        type: "character varying(150)",
                        maxLength: 150,
                        nullable: false),

                    PasswordHash = table.Column<string>(
                        type: "text",
                        nullable: false),

                    Role = table.Column<string>(
                        type: "text",
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProdukKategori_Kategoris_KategorisId",
                table: "ProdukKategori",
                column: "KategorisId",
                principalTable: "Kategoris",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdukKategori_Kategoris_KategorisId",
                table: "ProdukKategori");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdukKategori_Kategoris_KategorisId",
                table: "ProdukKategori",
                column: "KategorisId",
                principalTable: "Kategoris",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
