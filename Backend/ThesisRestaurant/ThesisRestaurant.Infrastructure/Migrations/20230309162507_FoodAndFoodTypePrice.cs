using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisRestaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FoodAndFoodTypePrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "FoodTypes",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BasePrice",
                table: "Foods",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "FoodTypes");

            migrationBuilder.DropColumn(
                name: "BasePrice",
                table: "Foods");
        }
    }
}
