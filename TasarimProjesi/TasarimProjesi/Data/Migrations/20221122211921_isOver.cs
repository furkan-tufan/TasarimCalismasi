using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasarimProjesi.Data.Migrations
{
    public partial class isOver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOver",
                table: "Request",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOver",
                table: "Request");
        }
    }
}
