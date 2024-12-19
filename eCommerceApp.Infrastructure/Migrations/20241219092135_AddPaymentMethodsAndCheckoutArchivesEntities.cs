using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentMethodsAndCheckoutArchivesEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d8af805-07f6-49ec-97f6-1cfc9745f194");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e381e27-9409-4240-9ffd-f9667376fdaa");

            migrationBuilder.CreateTable(
                name: "CheckoutArchives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedData = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutArchives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckoutArchives");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a119317e-e07b-4f5e-84ec-4cbacc45065f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d1542d-d908-436c-9cee-4f4714e0e0ae");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d8af805-07f6-49ec-97f6-1cfc9745f194", null, "Admin", "ADMIN" },
                    { "4e381e27-9409-4240-9ffd-f9667376fdaa", null, "User", "USER" }
                });
        }
    }
}
