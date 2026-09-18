using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplierProductExercise.Business.Migrations
{
    /// <inheritdoc />
    public partial class updatecodeindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SupplierProductVariant_Code",
                schema: "SupplierProductExercise",
                table: "SupplierProductVariant");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierProductVariant_Code_SupplierProductID",
                schema: "SupplierProductExercise",
                table: "SupplierProductVariant",
                columns: new[] { "Code", "SupplierProductID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SupplierProductVariant_Code_SupplierProductID",
                schema: "SupplierProductExercise",
                table: "SupplierProductVariant");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierProductVariant_Code",
                schema: "SupplierProductExercise",
                table: "SupplierProductVariant",
                column: "Code",
                unique: true);
        }
    }
}
