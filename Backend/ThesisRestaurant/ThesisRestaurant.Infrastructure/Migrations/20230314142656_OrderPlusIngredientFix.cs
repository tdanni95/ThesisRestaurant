using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisRestaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderPlusIngredientFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderAdditionalIngredient",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderAdditionalIngredient");
        }
    }
}
