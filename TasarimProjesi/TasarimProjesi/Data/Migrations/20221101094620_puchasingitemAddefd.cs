using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasarimProjesi.Data.Migrations
{
    public partial class puchasingitemAddefd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Purchasing");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Purchasing");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Purchasing");

            migrationBuilder.DropColumn(
                name: "Info",
                table: "Purchasing");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Purchasing");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Purchasing");

            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "Purchasing",
                newName: "PurchasingId");

            migrationBuilder.CreateTable(
                name: "PurchasingItem",
                columns: table => new
                {
                    PurchasingItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchasingId = table.Column<int>(type: "int", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasingItem", x => x.PurchasingItemId);
                    table.ForeignKey(
                        name: "FK_PurchasingItem_Purchasing_PurchasingId",
                        column: x => x.PurchasingId,
                        principalTable: "Purchasing",
                        principalColumn: "PurchasingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchasingItem_PurchasingId",
                table: "PurchasingItem",
                column: "PurchasingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchasingItem");

            migrationBuilder.RenameColumn(
                name: "PurchasingId",
                table: "Purchasing",
                newName: "PurchaseId");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Purchasing",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Purchasing",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Purchasing",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "Purchasing",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Purchasing",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Purchasing",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
