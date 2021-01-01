using Microsoft.EntityFrameworkCore.Migrations;

namespace MrLocalDb.Migrations
{
    public partial class AddedTableLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Shops");

            migrationBuilder.AddColumn<string>(
                name: "LocationId",
                table: "Shops",
                type: "nvarchar(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shops_LocationId",
                table: "Shops",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Location_LocationId",
                table: "Shops",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Location_LocationId",
                table: "Shops");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Shops_LocationId",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Shops");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Shops",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Shops",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");
        }
    }
}
