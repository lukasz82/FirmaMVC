using Microsoft.EntityFrameworkCore.Migrations;

namespace FirmaMVC.Migrations
{
    public partial class Departamenty3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Workplace_EmplPositionId",
                table: "Workplace");

            migrationBuilder.CreateIndex(
                name: "IX_Workplace_EmplPositionId",
                table: "Workplace",
                column: "EmplPositionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Workplace_EmplPositionId",
                table: "Workplace");

            migrationBuilder.CreateIndex(
                name: "IX_Workplace_EmplPositionId",
                table: "Workplace",
                column: "EmplPositionId");
        }
    }
}
