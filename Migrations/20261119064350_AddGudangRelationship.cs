using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddGudangRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GudangId",
                table: "Produks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Gudang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NamaGudang = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Alamat = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gudang", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produks_GudangId",
                table: "Produks",
                column: "GudangId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produks_Gudang_GudangId",
                table: "Produks",
                column: "GudangId",
                principalTable: "Gudang",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produks_Gudang_GudangId",
                table: "Produks");

            migrationBuilder.DropTable(
                name: "Gudang");

            migrationBuilder.DropIndex(
                name: "IX_Produks_GudangId",
                table: "Produks");

            migrationBuilder.DropColumn(
                name: "GudangId",
                table: "Produks");
        }
    }
}
