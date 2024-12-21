using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserIdsTypeInArchiveEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CheckoutArchives",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2b3c7f6a-d519-48eb-954a-24becee3050d", null, "Admin", "ADMIN" },
                    { "40bfb21a-b496-4103-b205-30162995b4e1", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("c63ecbd7-3280-455e-99dc-c7e79a5a48a3"), "Credit Card" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b3c7f6a-d519-48eb-954a-24becee3050d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40bfb21a-b496-4103-b205-30162995b4e1");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("c63ecbd7-3280-455e-99dc-c7e79a5a48a3"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CheckoutArchives",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
