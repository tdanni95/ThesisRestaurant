using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisRestaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserAddresses_UserAddressId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserAddressId",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserAddressId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Orders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "UserAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                newName: "IX_Orders_UserAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserAddresses_UserAddressId",
                table: "Orders",
                column: "UserAddressId",
                principalTable: "UserAddresses",
                principalColumn: "Id");
        }
    }
}
