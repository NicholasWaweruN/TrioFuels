using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Setup_Configurations_In_The_Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "0e141f5c-bda4-42d4-8bd2-e12e5f0a0dcb", new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8706), new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8705), new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8707), new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8702), "436ea0d3-1225-4683-9148-ce1b8077d5f4" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7879));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7887));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7890));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7893));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7895));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7898));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7901));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7903));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7906));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7908));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7911));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7914));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7916));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7919));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7931));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9238));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8865));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9197));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9200));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8347));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8362));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8365));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8367));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8369));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8372));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8374));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8376));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8378));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8381));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8383));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8385));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9157));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9429));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9432));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9434));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9114));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9318));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9484));

            migrationBuilder.InsertData(
                table: "Setup",
                columns: new[] { "Id", "App_VersionCode", "PasswordExpiryDays" },
                values: new object[] { 1L, "0.0.0.1", 30 });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8762));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9378));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9374));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9279), new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9280) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setup",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "b46fea7a-9b49-4296-aaa2-fe1282c11f50", new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5703), new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5703), new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5704), new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5699), "9972b826-93cb-4e3b-882d-f1a8fc5cada5" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(4995));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(4998));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5001));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5003));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5005));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5007));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5010));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5012));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5014));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5016));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5018));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5021));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5023));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5025));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5037));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6320));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6158));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6281));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6284));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5354));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5357));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5359));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5361));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5362));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5364));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5366));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5368));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5372));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5373));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5375));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6244));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6530));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6533));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6535));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6210));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6415));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6429));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6579));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(5873));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6474));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6469));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6360), new DateTime(2026, 6, 8, 5, 57, 7, 645, DateTimeKind.Utc).AddTicks(6361) });
        }
    }
}
