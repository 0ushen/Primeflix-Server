using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Primeflix.Infrastructure.Persistence.Migrations
{
    public partial class RemoveProductPosterAddPictureSmallUrlAndBigUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "BigUrl",
                table: "Picture",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SmallUrl",
                table: "Picture",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BigUrl",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "SmallUrl",
                table: "Picture");

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
