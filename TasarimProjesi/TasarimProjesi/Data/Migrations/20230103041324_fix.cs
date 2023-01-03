using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasarimProjesi.Data.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModel_Purchasing_PurchasingId",
                table: "FileModel");

            migrationBuilder.DropIndex(
                name: "IX_FileModel_PurchasingId",
                table: "FileModel");

            migrationBuilder.DropColumn(
                name: "PurchasingId",
                table: "FileModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchasingId",
                table: "FileModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileModel_PurchasingId",
                table: "FileModel",
                column: "PurchasingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModel_Purchasing_PurchasingId",
                table: "FileModel",
                column: "PurchasingId",
                principalTable: "Purchasing",
                principalColumn: "PurchasingId");
        }
    }
}
