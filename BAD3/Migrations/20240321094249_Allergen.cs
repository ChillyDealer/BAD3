using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BAD3.Migrations
{
    /// <inheritdoc />
    public partial class Allergen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Allergens",
                table: "Ingredient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allergens",
                table: "Ingredient");
        }
    }
}
