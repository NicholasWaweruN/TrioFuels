using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class PdaDevices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "a479edc9-b1f5-4fc3-99f7-cc20e3889a14", new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6687), new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6686), new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6688), new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6682), "1413feeb-73a2-4468-9ad8-3b27b0a4024d" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6121));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6136));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6140));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6142));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6148));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6151));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6153));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6156));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6159));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 13, 43, 54, 981, DateTimeKind.Utc).AddTicks(7911));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6979));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6801));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6926));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6929));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6412));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6415));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6417));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6419));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6421));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6423));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6425));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6427));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6429));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6431));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6433));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6435));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6437));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6877));

            migrationBuilder.InsertData(
                table: "PdaDevices",
                columns: new[] { "Id", "DateCreated", "DeviceCode", "DeviceIMEI", "DeviceMacAddress", "DeviceModel", "DeviceName", "DeviceSerialNumber", "DispenserCode", "IsActive", "UserCode" },
                values: new object[,]
                {
                    { 2L, new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6882), "1234567890", "1234567890", "1234567890", "1234567890", "Test PDA", "1234567890", "D02", true, "99999" },
                    { 3L, new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6885), "1234567890", "1234567890", "1234567890", "1234567890", "Test PDA", "1234567890", "D04", true, "99999" },
                    { 4L, new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6888), "1234567890", "1234567890", "1234567890", "1234567890", "Test PDA", "1234567890", "D07", true, "99999" }
                });

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(7184));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(7187));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(7189));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(8003));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(8009));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(8012));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6840));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6842));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6845));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(7078));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(7089));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(7225));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(7229));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(7231));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(7233));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6730));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(7130));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(7126));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 13, 43, 54, 981, DateTimeKind.Utc).AddTicks(7955));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "f711b26c-f3da-4bb9-a1d9-0cbe76d4f5d7", new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3121), new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3120), new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3122), new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3117), "d8690c48-6c13-467f-a795-08eedb01feb2" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2440));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2444));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2448));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2450));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2453));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2456));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2458));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2461));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2464));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(4568));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3422));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3253));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3377));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3382));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2806));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2810));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2812));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2816));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2830));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2833));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2835));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2837));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2839));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2841));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2844));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2846));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3337));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3647));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3651));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3656));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(4658));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(4663));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(4666));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3298));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3301));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3303));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3534));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3546));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3705));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3708));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3710));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3712));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3173));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3592));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(3584));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(4606));
        }
    }
}
