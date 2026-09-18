using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplierProductExercise.Business.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SupplierProductExercise");

            migrationBuilder.CreateTable(
                name: "SupplierProduct",
                schema: "SupplierProductExercise",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SKU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDiscontinued = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierProduct", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SupplierServiceParameter",
                schema: "SupplierProductExercise",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Service = table.Column<int>(type: "int", nullable: false),
                    BearerToken = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierServiceParameter", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SupplierProductVariant",
                schema: "SupplierProductExercise",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierProductID = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    IsDiscontinued = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierProductVariant", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SupplierProductVariant_SupplierProduct_SupplierProductID",
                        column: x => x.SupplierProductID,
                        principalSchema: "SupplierProductExercise",
                        principalTable: "SupplierProduct",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierProductVariant_SupplierProductID",
                schema: "SupplierProductExercise",
                table: "SupplierProductVariant",
                column: "SupplierProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierProductVariant",
                schema: "SupplierProductExercise");

            migrationBuilder.DropTable(
                name: "SupplierServiceParameter",
                schema: "SupplierProductExercise");

            migrationBuilder.DropTable(
                name: "SupplierProduct",
                schema: "SupplierProductExercise");
        }
    }
}
