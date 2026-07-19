using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixTillStoreNumbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: ["ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp"],
                values: new object[] { "ac5b0bba-ab17-47ad-8f3c-ccf82eb251ae", new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6029), new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6029), new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6030), new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6025), "b442579e-a81d-46c2-bcb0-7a7167925929" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5340));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5344));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5346));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5349));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5351));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5354));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5356));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5359));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5361));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(7506));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6325));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6166));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6288));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6293));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5747));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5749));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5752));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5754));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5756));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5758));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5760));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5761));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5763));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5765));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5767));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5769));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6251));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6507));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6509));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6511));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(7597));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(7601));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(7603));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6217));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6220));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6223));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6413));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6423));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6550));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6553));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6555));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6556));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6077));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6464));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6460));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "StoreNumber", "TillName" },
                values: new object[] { "5545198", "TRIO FUELS Till 1" });

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "StoreNumber", "TillName" },
                values: new object[] { "5545196", "TRIO FUELS Till 2" });

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "StoreNumber", "TillName" },
                values: new object[] { "5545194", "TRIO FUELS Till 3" });

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "StoreNumber", "TillName" },
                values: new object[] { "5545192", "TRIO FUELS Till 4" });

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "StoreNumber", "TillName" },
                values: new object[] { "5545190", "TRIO FUELS Till 5" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(7540));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "320e5cd1-c2c4-42a0-8526-7a1200738fd9", new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1515), new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1515), new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1517), new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1512), "3589fb9e-1de0-4aad-91cf-40eedc0e0072" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(824));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(832));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(835));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(837));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(840));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(842));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(844));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(847));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(849));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(3144));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1818));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1652));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1773));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1779));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1134));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1137));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1140));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1141));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1143));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1145));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1147));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1150));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1152));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1154));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1157));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1159));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(2015));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(2018));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(2020));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(3235));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(3239));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(3242));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1693));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1696));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1698));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1912));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1923));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(2060));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(2063));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(2065));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(2067));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1567));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1963));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1960));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "StoreNumber", "TillName" },
                values: new object[] { "5617668", "Till 1" });

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "StoreNumber", "TillName" },
                values: new object[] { "5617666", "Till 2" });

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "StoreNumber", "TillName" },
                values: new object[] { "5617664", "Till 3" });

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "StoreNumber", "TillName" },
                values: new object[] { "5617662", "Till 4" });

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "StoreNumber", "TillName" },
                values: new object[] { "5617660", "Till 5" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(3186));
        }
    }
}
