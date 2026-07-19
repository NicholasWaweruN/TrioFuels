using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddNozzle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "DateCreated", "TypeName" },
                values: new object[] { new DateTime(2026, 6, 22, 12, 50, 52, 46, DateTimeKind.Utc).AddTicks(2448), "NozzleCode" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "9e807090-7b6b-400c-90a4-b0e96ea70237", new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8852), new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8851), new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8853), new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8847), "f4eea760-1854-4641-b6ac-56054410f93e" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7861));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7869));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "DateCreated", "TypeName" },
                values: new object[] { new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7874), "Nozzlecode" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7878));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7881));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7886));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7890));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7894));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7898));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 601, DateTimeKind.Utc).AddTicks(748));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9325));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9070));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9262));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9266));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8393));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8399));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8403));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8407));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8434));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8438));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8442));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8445));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8449));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8452));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8456));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8459));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9202));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9659));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9664));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9668));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 601, DateTimeKind.Utc).AddTicks(914));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 601, DateTimeKind.Utc).AddTicks(920));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 601, DateTimeKind.Utc).AddTicks(924));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9136));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9144));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9496));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9509));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9736));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9740));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9743));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9746));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8933));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9582));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9577));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 601, DateTimeKind.Utc).AddTicks(835));
        }
    }
}
