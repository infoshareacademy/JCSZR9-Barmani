using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrinkItUpWebApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Drinks_DifficultyId",
                table: "Drinks");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_MainAlcoholId",
                table: "Drinks");

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_DifficultyId",
                table: "Drinks",
                column: "DifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_MainAlcoholId",
                table: "Drinks",
                column: "MainAlcoholId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Drinks_DifficultyId",
                table: "Drinks");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_MainAlcoholId",
                table: "Drinks");

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_DifficultyId",
                table: "Drinks",
                column: "DifficultyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_MainAlcoholId",
                table: "Drinks",
                column: "MainAlcoholId",
                unique: true);
        }
    }
}
