using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisRestaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthToken",
                table: "Users",
                newName: "RefreshToken_Token");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshToken_Created",
                table: "Users",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshToken_Expires",
                table: "Users",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "To",
                table: "FoodPrices",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "From",
                table: "FoodPrices",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken_Created",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshToken_Expires",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RefreshToken_Token",
                table: "Users",
                newName: "AuthToken");

            migrationBuilder.AlterColumn<DateTime>(
                name: "To",
                table: "FoodPrices",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "From",
                table: "FoodPrices",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }
    }
}
