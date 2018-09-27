using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementService.Data.Migrations
{
    public partial class access : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackingState",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "MenuAccess",
                columns: table => new
                {
                    MenuId = table.Column<long>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuAccess", x => new { x.MenuId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_MenuAccess_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuAccess_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuAccess_RoleId",
                table: "MenuAccess",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuAccess");

            migrationBuilder.AddColumn<int>(
                name: "TrackingState",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
