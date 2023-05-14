using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrinkItUpWebApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedPrimaryKeyForDrinkIngredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DrinkIngredients_DrinkId",
                table: "DrinkIngredients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkIngredients",
                table: "DrinkIngredients",
                columns: new[] { "DrinkId", "IngredientId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkIngredients",
                table: "DrinkIngredients");

            migrationBuilder.CreateIndex(
                name: "IX_DrinkIngredients_DrinkId",
                table: "DrinkIngredients",
                column: "DrinkId");
        }
    }
}
