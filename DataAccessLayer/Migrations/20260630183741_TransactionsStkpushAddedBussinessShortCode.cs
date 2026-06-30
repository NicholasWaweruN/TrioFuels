using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class TransactionsStkpushAddedBussinessShortCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessShortCode",
                table: "StkTransactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "8cc67fef-e516-46b2-9ae1-4ebf86c27c1f", new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1101), new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1098), new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1104), new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1094), "01806432-8b56-4091-ba7a-a9c3ed9c0393" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(25));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(35));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(41));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(47));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(53));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(58));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(64));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(95));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(102));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(4175));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1579));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1307));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1517));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1524));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(616));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(624));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(630));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(635));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(641));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(646));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(679));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(684));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(689));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(695));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(713));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(718));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(723));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1433));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1442));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1449));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1456));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1931));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1940));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1947));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(4320));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(4329));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(4335));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1371));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1377));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1382));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1735));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1755));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(2067));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(2114));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(2122));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(2128));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1178));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1825));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(1818));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 21, 37, 38, 78, DateTimeKind.Unspecified).AddTicks(4243));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessShortCode",
                table: "StkTransactions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "2e1ccb53-ee1e-4d40-ac5f-314a77b1b1b4", new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(4), new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(1), new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(6), new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(9995), "44c153fa-1797-4f7c-a94a-5dded2d4775d" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(8919));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(8929));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(8934));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(8939));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(8943));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(8949));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(8954));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(8983));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(8988));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(3580));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(750));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(221));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(654));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(663));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(9529));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(9534));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(9538));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(9543));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(9555));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(9559));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(9601));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(9606));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(9610));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(9615));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(9619));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(9623));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 554, DateTimeKind.Unspecified).AddTicks(9627));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(532));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(543));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(550));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(557));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(1246));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(1254));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(1261));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(3763));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(3773));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(362));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(371));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(377));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(1002));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(1024));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(1362));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(1368));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(1374));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(1380));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(80));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(1126));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(1116));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 30, 14, 20, 46, 555, DateTimeKind.Unspecified).AddTicks(3659));
        }
    }
}
