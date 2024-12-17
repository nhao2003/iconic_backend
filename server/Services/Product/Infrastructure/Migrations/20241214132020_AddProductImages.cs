using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "600c1fc2-4751-4eac-9bb2-ed22a08d1f3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "792dc9d3-c238-40fc-9117-c092f21a3a1c");

            migrationBuilder.AddColumn<string>(
                name: "ImageCoverUrls",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9e392741-6e39-4770-83f8-fe49e6f53733", null, "Customer", "CUSTOMER" },
                    { "d0c0f49e-5ebd-40f8-ba68-741003dfc0f4", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e392741-6e39-4770-83f8-fe49e6f53733");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0c0f49e-5ebd-40f8-ba68-741003dfc0f4");

            migrationBuilder.DropColumn(
                name: "ImageCoverUrls",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "600c1fc2-4751-4eac-9bb2-ed22a08d1f3f", null, "Admin", "ADMIN" },
                    { "792dc9d3-c238-40fc-9117-c092f21a3a1c", null, "Customer", "CUSTOMER" }
                });
        }
    }
}
