using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisRestaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Food3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_FoodPictures_FoodPictureId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_FoodPictureId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "FoodPictureId",
                table: "Foods");

            migrationBuilder.AddColumn<int>(
                name: "FoodId",
                table: "FoodPictures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodPictures_FoodId",
                table: "FoodPictures",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPictures_Foods_FoodId",
                table: "FoodPictures",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodPictures_Foods_FoodId",
                table: "FoodPictures");

            migrationBuilder.DropIndex(
                name: "IX_FoodPictures_FoodId",
                table: "FoodPictures");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "FoodPictures");

            migrationBuilder.AddColumn<int>(
                name: "FoodPictureId",
                table: "Foods",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodPictureId",
                table: "Foods",
                column: "FoodPictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_FoodPictures_FoodPictureId",
                table: "Foods",
                column: "FoodPictureId",
                principalTable: "FoodPictures",
                principalColumn: "Id");
        }
    }
}
