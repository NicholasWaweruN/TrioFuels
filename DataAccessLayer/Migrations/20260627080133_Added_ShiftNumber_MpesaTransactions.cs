using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Added_ShiftNumber_MpesaTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShiftNumber",
                table: "MpesaTransactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "4a55d0a3-5914-43c1-abc7-d77c84f1d1bf", new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5511), new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5510), new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5513), new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5507), "0b6a794d-b9e5-4a08-a39d-e649021e36d0" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4738));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4747));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4751));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4754));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4757));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4760));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4763));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4780));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4783));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6746));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5826));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5654));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5787));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5791));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5067));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5071));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5074));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5076));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5079));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5094));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5127));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5130));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5133));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5135));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5138));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5140));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5143));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5736));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5741));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5745));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5748));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6034));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6037));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6040));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6837));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6841));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6845));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5693));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5699));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5702));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5938));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5949));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6082));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6085));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6088));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6090));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5564));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5993));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5988));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6786));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShiftNumber",
                table: "MpesaTransactions");

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

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6882));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6885));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 22, 16, 43, 54, 981, DateTimeKind.Utc).AddTicks(6888));

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
    }
}
