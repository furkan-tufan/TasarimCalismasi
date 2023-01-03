using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasarimProjesi.Data.Migrations
{
    public partial class invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Uploaded",
                table: "PurchasingItem",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Purchasing",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchasingId",
                table: "FileModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchasingItemId",
                table: "FileModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileModel_PurchasingId",
                table: "FileModel",
                column: "PurchasingId");

            migrationBuilder.CreateIndex(
                name: "IX_FileModel_PurchasingItemId",
                table: "FileModel",
                column: "PurchasingItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModel_Purchasing_PurchasingId",
                table: "FileModel",
                column: "PurchasingId",
                principalTable: "Purchasing",
                principalColumn: "PurchasingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModel_PurchasingItem_PurchasingItemId",
                table: "FileModel",
                column: "PurchasingItemId",
                principalTable: "PurchasingItem",
                principalColumn: "PurchasingItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModel_Purchasing_PurchasingId",
                table: "FileModel");

            migrationBuilder.DropForeignKey(
                name: "FK_FileModel_PurchasingItem_PurchasingItemId",
                table: "FileModel");

            migrationBuilder.DropIndex(
                name: "IX_FileModel_PurchasingId",
                table: "FileModel");

            migrationBuilder.DropIndex(
                name: "IX_FileModel_PurchasingItemId",
                table: "FileModel");

            migrationBuilder.DropColumn(
                name: "Uploaded",
                table: "PurchasingItem");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Purchasing");

            migrationBuilder.DropColumn(
                name: "PurchasingId",
                table: "FileModel");

            migrationBuilder.DropColumn(
                name: "PurchasingItemId",
                table: "FileModel");
        }
    }
}
