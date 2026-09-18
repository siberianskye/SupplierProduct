using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplierProductExercise.Business.Migrations
{
    /// <inheritdoc />
    public partial class addcodeindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SupplierProductVariant_Code",
                schema: "SupplierProductExercise",
                table: "SupplierProductVariant",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SupplierProductVariant_Code",
                schema: "SupplierProductExercise",
                table: "SupplierProductVariant");
        }
    }
}
