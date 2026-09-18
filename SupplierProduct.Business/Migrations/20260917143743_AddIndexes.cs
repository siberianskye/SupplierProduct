using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplierProductExercise.Business.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SupplierServiceParameter_Service",
                schema: "SupplierProductExercise",
                table: "SupplierServiceParameter",
                column: "Service",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierProduct_SKU",
                schema: "SupplierProductExercise",
                table: "SupplierProduct",
                column: "SKU",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SupplierServiceParameter_Service",
                schema: "SupplierProductExercise",
                table: "SupplierServiceParameter");

            migrationBuilder.DropIndex(
                name: "IX_SupplierProduct_SKU",
                schema: "SupplierProductExercise",
                table: "SupplierProduct");
        }
    }
}
