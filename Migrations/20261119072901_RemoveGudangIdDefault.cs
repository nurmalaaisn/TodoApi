
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApi.Migrations
{
    public partial class RemoveGudangIdDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"Produks\" ALTER COLUMN \"GudangId\" DROP DEFAULT;"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"Produks\" ALTER COLUMN \"GudangId\" SET DEFAULT 0;"
            );
        }
    }
}
