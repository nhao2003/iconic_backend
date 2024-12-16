using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class configProductAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeValueIndexes_Variants_VariantId",
                table: "ProductAttributeValueIndexes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ee49e64-be72-44a4-aaa2-74f3f3282134");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ef672cc-b6e8-47e8-ac9d-95aee7531f5a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3baa1865-9057-4f28-9362-e49e7c5fd3b1", null, "Customer", "CUSTOMER" },
                    { "83628309-95ad-411f-83ac-3e5e73c7c945", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeValueIndexes_Variants_VariantId",
                table: "ProductAttributeValueIndexes",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeValueIndexes_Variants_VariantId",
                table: "ProductAttributeValueIndexes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3baa1865-9057-4f28-9362-e49e7c5fd3b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83628309-95ad-411f-83ac-3e5e73c7c945");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ee49e64-be72-44a4-aaa2-74f3f3282134", null, "Admin", "ADMIN" },
                    { "0ef672cc-b6e8-47e8-ac9d-95aee7531f5a", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeValueIndexes_Variants_VariantId",
                table: "ProductAttributeValueIndexes",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
