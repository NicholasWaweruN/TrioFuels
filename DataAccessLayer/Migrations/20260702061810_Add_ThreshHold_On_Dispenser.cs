using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Add_ThreshHold_On_Dispenser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoreNumber",
                table: "UsageBalanceDto",
                newName: "TillNumber");

            migrationBuilder.AddColumn<decimal>(
                name: "ThreshHold",
                table: "Dispensers",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "dd7a50cd-6952-4fb5-8f5b-3d39dd002354", new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6798), new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6797), new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6800), new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6793), "eca8c5ec-707f-4fa1-bf4b-a898d648a7c8" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5854));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5862));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5866));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5870));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5873));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5877));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5881));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5885));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5888));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(8205));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7127));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "ThreshHold" },
                values: new object[] { new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6949), 0m });

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7084));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7090));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6465));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6470));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6474));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6477));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6481));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6484));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6487));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6491));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6494));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6497));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6501));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6504));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6507));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7030));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7037));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7043));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7047));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7348));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7355));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7358));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(8303));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(8308));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(8312));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6988));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6992));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6995));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7232));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7243));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7389));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7394));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7397));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7400));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6848));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7292));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7283));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(8246));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThreshHold",
                table: "Dispensers");

            migrationBuilder.RenameColumn(
                name: "TillNumber",
                table: "UsageBalanceDto",
                newName: "StoreNumber");

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
    }
}
