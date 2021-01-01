using Microsoft.EntityFrameworkCore.Migrations;

namespace MrLocalDb.Migrations
{
    public partial class AddedShopColumnToLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shops_LocationId",
                table: "Shops");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_LocationId",
                table: "Shops",
                column: "LocationId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shops_LocationId",
                table: "Shops");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_LocationId",
                table: "Shops",
                column: "LocationId");
        }
    }
}
