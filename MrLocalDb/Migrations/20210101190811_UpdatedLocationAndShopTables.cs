using Microsoft.EntityFrameworkCore.Migrations;

namespace MrLocalDb.Migrations
{
    public partial class UpdatedLocationAndShopTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Location_LocationId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Shops_LocationId",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Shops");

            migrationBuilder.AddColumn<string>(
                name: "ShopId",
                table: "Location",
                type: "nvarchar(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Location_ShopId",
                table: "Location",
                column: "ShopId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Shops_ShopId",
                table: "Location",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "ShopId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Shops_ShopId",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_ShopId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Location");

            migrationBuilder.AddColumn<string>(
                name: "LocationId",
                table: "Shops",
                type: "nvarchar(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_LocationId",
                table: "Shops",
                column: "LocationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Location_LocationId",
                table: "Shops",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
