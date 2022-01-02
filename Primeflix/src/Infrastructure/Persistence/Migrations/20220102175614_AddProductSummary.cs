using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Primeflix.Infrastructure.Persistence.Migrations
{
    public partial class AddProductSummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Products");
        }
    }
}
