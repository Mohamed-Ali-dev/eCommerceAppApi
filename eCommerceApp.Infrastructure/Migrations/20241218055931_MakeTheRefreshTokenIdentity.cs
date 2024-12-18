using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeTheRefreshTokenIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bed77eea-5e4d-41d6-9d22-37678a41ac1f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d60bb100-d973-47e1-902e-68de334cffb7");

            migrationBuilder.DropColumn(
                name: "RevokedOn",
                table: "refreshTokens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d8af805-07f6-49ec-97f6-1cfc9745f194", null, "Admin", "ADMIN" },
                    { "4e381e27-9409-4240-9ffd-f9667376fdaa", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d8af805-07f6-49ec-97f6-1cfc9745f194");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e381e27-9409-4240-9ffd-f9667376fdaa");

            migrationBuilder.AddColumn<DateTime>(
                name: "RevokedOn",
                table: "refreshTokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bed77eea-5e4d-41d6-9d22-37678a41ac1f", null, "User", "USER" },
                    { "d60bb100-d973-47e1-902e-68de334cffb7", null, "Admin", "ADMIN" }
                });
        }
    }
}
