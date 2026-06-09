using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Pulltransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "aaef5107-ed5f-4e90-b35a-3f460540e659", new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5683), new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5683), new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5685), new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5680), "0aba56bd-a178-43dd-905b-e818523852ad" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5088));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5093));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5096));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5099));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5103));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5106));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5108));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5111));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5113));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5115));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5118));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5120));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5123));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5125));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5136));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6871));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5974));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5816));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5932));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5935));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5386));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5389));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5392));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5394));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5396));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5398));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5400));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5402));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5404));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5406));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5408));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5410));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5897));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6164));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6167));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6169));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6962));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6966));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6969));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5857));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5860));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5862));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6060));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6072));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6219));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "DateCreated", "RoleCode", "RoleName", "UserCode" },
                values: new object[,]
                {
                    { 2L, new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6222), "002", "SuperVisor", "99999" },
                    { 3L, new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6224), "003", "Attendant", "99999" },
                    { 4L, new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6226), "004", "Accountant", "99999" }
                });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5731));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6114));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6108));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch", "StoreNumber", "TillName", "TillNumber" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "5617668", "Till 1", "5617668" });

            migrationBuilder.InsertData(
                table: "Tills",
                columns: new[] { "Id", "DateCreated", "IsActive", "LastFetch", "OffsetValue", "StoreNumber", "TillName", "TillNumber", "UserCode" },
                values: new object[,]
                {
                    { 2L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "5617666", "Till 2", "5617666", "99999" },
                    { 3L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "5617664", "Till 3", "5617664", "99999" },
                    { 4L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "5617662", "Till 4", "5617662", "99999" }
                });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6915));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "d797978d-6db2-4ee9-8dd1-023a25153af1", new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8412), new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8411), new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8413), new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8408), "146b1b65-ecc0-47de-99d3-ca3586f3178b" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(7982));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(7988));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(7990));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(7992));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(7994));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(7995));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(7997));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(7999));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8000));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8002));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8012));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8014));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8016));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8018));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8020));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(9273));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8605));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8500));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8576));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8581));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8179));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8181));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8183));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8184));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8186));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8188));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8189));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8193));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8195));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8199));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8201));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8554));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8744));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8747));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8749));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(9342));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(9345));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(9348));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8527));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8530));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8531));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8653));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8661));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8771));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8446));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8709));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8706));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch", "StoreNumber", "TillName", "TillNumber" },
                values: new object[] { new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8629), new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(8630), "078678", "Test Till", "078678" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 9, 14, 20, 270, DateTimeKind.Utc).AddTicks(9303));
        }
    }
}
