using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrinkItUpWebApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedDrinkPhotoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DrinkPhotoId",
                table: "Drinks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrinkPhotoId",
                table: "Drinks");
        }
    }
}
