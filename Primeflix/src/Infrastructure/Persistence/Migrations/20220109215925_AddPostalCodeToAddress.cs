using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Primeflix.Infrastructure.Persistence.Migrations
{
    public partial class AddPostalCodeToAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Addresses");
        }
    }
}
