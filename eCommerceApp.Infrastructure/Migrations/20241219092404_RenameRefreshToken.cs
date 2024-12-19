using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_refreshTokens",
                table: "refreshTokens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a119317e-e07b-4f5e-84ec-4cbacc45065f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d1542d-d908-436c-9cee-4f4714e0e0ae");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("b1c2adc7-04fc-43bb-a4b1-4eb08547d462"));

            migrationBuilder.RenameTable(
                name: "refreshTokens",
                newName: "RefreshTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7651f9d0-1f9e-4f6b-9025-4bb82963cd4c", null, "Admin", "ADMIN" },
                    { "8934405d-ac0f-4039-bbdd-b1c4df871a8d", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("d40e694e-2895-4ce6-9b79-6318ce42f871"), "Credit Card" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7651f9d0-1f9e-4f6b-9025-4bb82963cd4c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8934405d-ac0f-4039-bbdd-b1c4df871a8d");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("d40e694e-2895-4ce6-9b79-6318ce42f871"));

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "refreshTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_refreshTokens",
                table: "refreshTokens",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a119317e-e07b-4f5e-84ec-4cbacc45065f", null, "Admin", "ADMIN" },
                    { "f6d1542d-d908-436c-9cee-4f4714e0e0ae", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("b1c2adc7-04fc-43bb-a4b1-4eb08547d462"), "Credit Card" });
        }
    }
}
