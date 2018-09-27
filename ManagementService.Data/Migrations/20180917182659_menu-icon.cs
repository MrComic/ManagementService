using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementService.Data.Migrations
{
    public partial class menuicon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MenuIconName",
                table: "Menus",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuIconName",
                table: "Menus");
        }
    }
}
