using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisRestaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderPlusIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FoodSizeId",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrderAdditionalIngredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    OrderItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAdditionalIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderAdditionalIngredient_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderAdditionalIngredient_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_FoodSizeId",
                table: "OrderItems",
                column: "FoodSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAdditionalIngredient_IngredientId",
                table: "OrderAdditionalIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAdditionalIngredient_OrderItemId",
                table: "OrderAdditionalIngredient",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_FoodSizes_FoodSizeId",
                table: "OrderItems",
                column: "FoodSizeId",
                principalTable: "FoodSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_FoodSizes_FoodSizeId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "OrderAdditionalIngredient");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_FoodSizeId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "FoodSizeId",
                table: "OrderItems");
        }
    }
}
