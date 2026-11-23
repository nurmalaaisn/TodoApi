
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToManyProdukKategori : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoris",
                columns: table => new
                {
                    Id = table.Column<int>(
                        type: "integer",
                        nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),

                    NamaKategori = table.Column<string>(
                        type: "character varying(100)",
                        maxLength: 100,
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoris", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProdukKategori",
                columns: table => new
                {
                    KategorisId = table.Column<int>(
                        type: "integer",
                        nullable: false),

                    ProduksId = table.Column<int>(
                        type: "integer",
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_ProdukKategori",
                        x => new { x.KategorisId, x.ProduksId });

                    table.ForeignKey(
                        name: "FK_ProdukKategori_Kategoris_KategorisId",
                        column: x => x.KategorisId,
                        principalTable: "Kategoris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_ProdukKategori_Produks_ProduksId",
                        column: x => x.ProduksId,
                        principalTable: "Produks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdukKategori_ProduksId",
                table: "ProdukKategori",
                column: "ProduksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdukKategori");

            migrationBuilder.DropTable(
                name: "Kategoris");
        }
    }
}
