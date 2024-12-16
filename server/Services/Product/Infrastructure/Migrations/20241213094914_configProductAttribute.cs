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
                name: "FK_ProductAttributes_Products_ProductId",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeValueIndexes_Variants_VariantId",
                table: "ProductAttributeValueIndexes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_ProductId",
                table: "ProductAttributes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ee49e64-be72-44a4-aaa2-74f3f3282134");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ef672cc-b6e8-47e8-ac9d-95aee7531f5a");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductAttributes");

            migrationBuilder.CreateTable(
                name: "ProductAttributeNavigators",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductAttributeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeNavigators", x => new { x.ProductId, x.ProductAttributeId });
                    table.ForeignKey(
                        name: "FK_ProductAttributeNavigators_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributeNavigators_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "600c1fc2-4751-4eac-9bb2-ed22a08d1f3f", null, "Admin", "ADMIN" },
                    { "792dc9d3-c238-40fc-9117-c092f21a3a1c", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeNavigators_ProductAttributeId",
                table: "ProductAttributeNavigators",
                column: "ProductAttributeId");

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

            migrationBuilder.DropTable(
                name: "ProductAttributeNavigators");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "600c1fc2-4751-4eac-9bb2-ed22a08d1f3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "792dc9d3-c238-40fc-9117-c092f21a3a1c");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ee49e64-be72-44a4-aaa2-74f3f3282134", null, "Admin", "ADMIN" },
                    { "0ef672cc-b6e8-47e8-ac9d-95aee7531f5a", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ProductId",
                table: "ProductAttributes",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_Products_ProductId",
                table: "ProductAttributes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
