using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class CarwashUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "SalesReportRow",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PinFailedCount",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PinHash",
                table: "AspNetUsers",
                type: "character varying(200)",
                unicode: false,
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PinLockedUntil",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "PinFailedCount", "PinHash", "PinLockedUntil", "SecurityStamp" },
                values: new object[] { "9be8cc41-6fdf-499c-b83f-9baff8950a96", new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1175), new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1174), new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1176), new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1170), 0, null, null, "8ffe2fec-95fa-499d-9c75-b774aae54d50" });

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

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[] { new Guid("22222222-0000-0000-0000-000000000003"), "05", new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(1264), "99999" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 11, 34, 26, 356, DateTimeKind.Unspecified).AddTicks(2331));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000003"));

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "SalesReportRow");

            migrationBuilder.DropColumn(
                name: "PinFailedCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PinHash",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PinLockedUntil",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "d0e62e79-3f27-4430-847e-12207e75bbd8", new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8375), new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8373), new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8379), new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8367), "894452a0-86cf-4675-a4d4-969a894c4518" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7432));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7441));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7448));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7454));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7460));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7466));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7472));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7526));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 818, DateTimeKind.Unspecified).AddTicks(1215));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8874));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8582));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8802));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8811));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7877));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7883));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7889));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7894));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7899));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7904));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7948));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7953));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7958));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7963));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7968));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7973));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7978));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8717));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8726));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8734));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8740));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9235));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9242));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9247));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 818, DateTimeKind.Unspecified).AddTicks(1370));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 818, DateTimeKind.Unspecified).AddTicks(1379));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 818, DateTimeKind.Unspecified).AddTicks(1385));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8650));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8656));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8662));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9034));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9077));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9315));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9321));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9327));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9332));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8458));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9152));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9144));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 818, DateTimeKind.Unspecified).AddTicks(1291));
        }
    }
}
