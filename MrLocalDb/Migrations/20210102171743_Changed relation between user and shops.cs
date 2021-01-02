using Microsoft.EntityFrameworkCore.Migrations;

namespace MrLocalDb.Migrations
{
    public partial class Changedrelationbetweenuserandshops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shops_UserId",
                table: "Shops");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_UserId",
                table: "Shops",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shops_UserId",
                table: "Shops");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_UserId",
                table: "Shops",
                column: "UserId",
                unique: true);
        }
    }
}
