using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class DispenserTillNumbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "ccf9a3cf-6c05-453f-bf89-e582d0692d61", new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3873), new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3872), new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3874), new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3868), "d73179ea-f1a9-47f4-aa24-7b897f88aa11" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3033));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3039));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3043));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3047));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3050));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3054));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3057));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3061));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3064));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3067));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3071));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3074));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3078));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3081));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3094));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(5627));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4286));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "TillNumber" },
                values: new object[] { new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4067), "5617668" });

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4229));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4234));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3475));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3478));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3481));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3484));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3487));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3489));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3492));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3495));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3497));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3500));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3502));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3505));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4175));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4543));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4547));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4550));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(5744));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(5749));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(5753));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4122));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4126));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4130));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4406));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4419));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4609));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4614));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4616));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4619));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3942));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4479));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4473));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(5685));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "DateCreated", "TillNumber" },
                values: new object[] { new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(5816), "078678" });

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

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6222));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6224));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6226));

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
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 9, 19, 6, 49, 942, DateTimeKind.Utc).AddTicks(6915));
        }
    }
}
