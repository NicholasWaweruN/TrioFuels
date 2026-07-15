using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class CarwashUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "ae19e2c9-7ccc-4f6b-b3df-33fca6b64c00", new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4976), new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4974), new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4979), new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4969), "3e064430-a183-4acb-9036-d3437810e983" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4110));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4118));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4123));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4127));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4132));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4136));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4140));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4163));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4167));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(6381));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5350));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5127));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5284));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5291));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4566));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4571));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4575));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4579));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4583));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4602));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4636));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4640));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4644));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4648));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4651));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4655));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4658));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5223));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5230));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5235));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5240));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5580));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5585));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5589));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(6493));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(6499));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(6503));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5173));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5178));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5182));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[] { new Guid("11111111-0000-0000-0000-000000000005"), "05", "Car Wash App", "", new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4794), "" });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5460));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5473));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5641));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5646));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5651));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5655));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5031));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5524));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5516));

            migrationBuilder.UpdateData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000003"),
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5085));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(6436));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000005"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "9be8cc41-6fdf-499c-b83f-9baff8950a96", new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1175), new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1174), new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1176), new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1170), "8ffe2fec-95fa-499d-9c75-b774aae54d50" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(516));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(523));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(527));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(530));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(533));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(537));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(540));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(543));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(546));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(2291));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1452));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1291));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1413));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1419));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(889));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(892));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(895));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(898));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(901));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(904));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(906));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(909));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(912));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(915));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(918));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(921));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(923));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1368));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1374));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1378));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1382));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1644));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1648));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1651));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(2374));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(2379));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(2383));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1329));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1335));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1338));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1541));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1551));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1683));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1687));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1689));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1692));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1214));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1599));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1584));

            migrationBuilder.UpdateData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000003"),
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1264));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(2331));
        }
    }
}
