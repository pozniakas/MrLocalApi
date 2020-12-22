using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MrLocalDb.Migrations
{
    public partial class ProductsShopsSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "ShopId", "City", "CreatedAt", "DeletedAt", "Description", "Name", "Status", "TypeOfShop", "UpdatedAt" },
                values: new object[] { "a52cc615-8b33-4bdc-b1b3-f8a9c934e94b", "Kaunas", new DateTime(2020, 12, 14, 14, 2, 30, 713, DateTimeKind.Local).AddTicks(5481), null, "Pas mus parduodamos pačios pigiausios ir skaniausios braškės. Nepamiršk užsukti!", "Pigiausios braškės", "Not Active", "Berries", new DateTime(2020, 12, 22, 14, 2, 30, 713, DateTimeKind.Local).AddTicks(5498) });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "ShopId", "City", "CreatedAt", "DeletedAt", "Description", "Name", "Status", "TypeOfShop", "UpdatedAt" },
                values: new object[] { "cc52dc72-5d75-43ae-b8fb-d31c7db83f2e", "Klaipėda", new DateTime(2020, 10, 27, 14, 2, 30, 714, DateTimeKind.Local).AddTicks(243), null, "Klaipėda yra žuvų sostinė, o pas mus jos pačios skaniausios! Kontaktinis telefono numeris:\n+37065569003", "Skaniausia žuvis Klaipėdoje", "Active", "Seafood", new DateTime(2020, 12, 22, 14, 2, 30, 714, DateTimeKind.Local).AddTicks(257) });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "ShopId", "City", "CreatedAt", "DeletedAt", "Description", "Name", "Status", "TypeOfShop", "UpdatedAt" },
                values: new object[] { "20dd1101-08e1-4c30-9773-f48fc585a899", "Šiauliai", new DateTime(2020, 12, 22, 14, 2, 30, 714, DateTimeKind.Local).AddTicks(263), null, "Autentiški tradiciniai dirbiniai, kurie suteiks jūsų virtuvei daugiau grožio. Užsukite!\nJei kils kokių nors klausimų, rašykit man į el. paštą: askietamociute@gmail.com", "Močiutės keramikos dirbiniai", "Paused", "Handmade", new DateTime(2020, 12, 22, 14, 2, 30, 714, DateTimeKind.Local).AddTicks(265) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CreatedAt", "DeletedAt", "Description", "Name", "Price", "PriceType", "ShopId", "UpdatedAt" },
                values: new object[,]
                {
                    { "71a9530e-4040-41f9-b7cf-f423982d2eed", new DateTime(2020, 12, 14, 14, 2, 30, 712, DateTimeKind.Local).AddTicks(3843), null, "Geriau jau pirkit lietuviškas, neprasidėkit su lenkiškom.", "Lenkiškos braškės", 5.50m, "KILOGRAMS", "a52cc615-8b33-4bdc-b1b3-f8a9c934e94b", new DateTime(2020, 12, 22, 14, 2, 30, 712, DateTimeKind.Local).AddTicks(3901) },
                    { "e96f9d32-35a7-4994-b651-961b35de64b0", new DateTime(2020, 12, 14, 14, 2, 30, 712, DateTimeKind.Local).AddTicks(5091), null, "Va šitos tai labai skanios.", "Lietuviškos braškės", 7.50m, "KILOGRAMS", "a52cc615-8b33-4bdc-b1b3-f8a9c934e94b", new DateTime(2020, 12, 22, 14, 2, 30, 712, DateTimeKind.Local).AddTicks(5103) },
                    { "64160404-d39e-4294-ae89-785c7f5270b8", new DateTime(2020, 10, 27, 14, 2, 30, 712, DateTimeKind.Local).AddTicks(5108), null, "Pagauta šiandien ryte", "Lašiša", 9.99m, "KILOGRAMS", "cc52dc72-5d75-43ae-b8fb-d31c7db83f2e", new DateTime(2020, 12, 22, 14, 2, 30, 712, DateTimeKind.Local).AddTicks(5110) },
                    { "ceaf1918-fd96-413e-bd7d-101446d0d80f", new DateTime(2020, 10, 27, 14, 2, 30, 712, DateTimeKind.Local).AddTicks(5117), null, "Pagauta šiandien ryte", "Karpis", 11.20m, "KILOGRAMS", "cc52dc72-5d75-43ae-b8fb-d31c7db83f2e", new DateTime(2020, 12, 22, 14, 2, 30, 712, DateTimeKind.Local).AddTicks(5119) },
                    { "588564a6-46af-4b60-b408-282d3ffdb1a4", new DateTime(2020, 10, 27, 14, 2, 30, 712, DateTimeKind.Local).AddTicks(5124), null, "Pagauta šiandien ryte", "Jūros ešerys", 14.50m, "KILOGRAMS", "cc52dc72-5d75-43ae-b8fb-d31c7db83f2e", new DateTime(2020, 12, 22, 14, 2, 30, 712, DateTimeKind.Local).AddTicks(5126) },
                    { "3ebe4536-7da5-4353-861b-d63b569a1a3b", new DateTime(2020, 12, 22, 14, 2, 30, 712, DateTimeKind.Local).AddTicks(5141), null, "Nulipdytas iš molio", "Arbatinis puodelis", 5m, "UNIT", "20dd1101-08e1-4c30-9773-f48fc585a899", new DateTime(2020, 12, 22, 14, 2, 30, 712, DateTimeKind.Local).AddTicks(5143) },
                    { "3dd6fb2c-a25d-4cb7-b81f-61deaf211a98", new DateTime(2020, 12, 22, 14, 2, 30, 712, DateTimeKind.Local).AddTicks(5147), null, "Nulipdytas iš molio", "Vaza", 8.50m, "UNIT", "20dd1101-08e1-4c30-9773-f48fc585a899", new DateTime(2020, 12, 22, 14, 2, 30, 712, DateTimeKind.Local).AddTicks(5149) }
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "3dd6fb2c-a25d-4cb7-b81f-61deaf211a98");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "3ebe4536-7da5-4353-861b-d63b569a1a3b");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "588564a6-46af-4b60-b408-282d3ffdb1a4");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "64160404-d39e-4294-ae89-785c7f5270b8");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "71a9530e-4040-41f9-b7cf-f423982d2eed");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "ceaf1918-fd96-413e-bd7d-101446d0d80f");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "e96f9d32-35a7-4994-b651-961b35de64b0");

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "ShopId",
                keyValue: "20dd1101-08e1-4c30-9773-f48fc585a899");

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "ShopId",
                keyValue: "a52cc615-8b33-4bdc-b1b3-f8a9c934e94b");

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "ShopId",
                keyValue: "cc52dc72-5d75-43ae-b8fb-d31c7db83f2e");
        }
    }
}
