using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class MakeGudangidRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produks_Gudang_GudangId",
                table: "Produks");

            migrationBuilder.AlterColumn<int>(
                name: "GudangId",
                table: "Produks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Produks_Gudang_GudangId",
                table: "Produks",
                column: "GudangId",
                principalTable: "Gudang",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produks_Gudang_GudangId",
                table: "Produks");

            migrationBuilder.AlterColumn<int>(
                name: "GudangId",
                table: "Produks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Produks_Gudang_GudangId",
                table: "Produks",
                column: "GudangId",
                principalTable: "Gudang",
                principalColumn: "Id");
        }
    }
}
