using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Latest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "82ac803b-a343-451d-9d84-0bb6c509fe76", new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7979), new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7978), new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7981), new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7973), "ef6dfd0a-f9f8-40cf-9040-22cc7d22391b" });

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9974));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9978));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9981));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9983));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9985));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9987));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9989));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9991));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9993));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9995));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9998));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(2));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(4));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(6));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(8));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(10));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 18L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(12));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 19L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(14));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 20L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(17));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 21L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(19));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 22L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(21));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 23L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(31));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 24L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(33));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 25L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(36));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 26L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(38));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 27L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(40));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 28L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(43));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 29L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(45));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 30L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(47));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 31L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(49));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 32L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(51));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 33L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(53));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 34L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(55));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 35L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(58));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 36L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(61));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 37L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(63));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 38L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(65));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 39L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(67));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 40L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(69));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 41L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(74));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 42L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(77));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 43L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(79));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 44L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(81));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 45L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(83));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 46L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(85));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 47L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(88));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 48L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(90));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 49L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(92));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 50L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(94));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 51L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(96));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 52L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(98));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 53L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(100));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 54L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(102));

            migrationBuilder.InsertData(
                table: "CarWashProducts",
                columns: new[] { "Id", "DateCreated", "IsActive", "Name", "Price", "UserCode" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(198), true, "Base Wash", 300m, "" },
                    { 2L, new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(202), true, "Top Wash", 200m, "" },
                    { 3L, new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(205), true, "Engine Wash", 400m, "" },
                    { 4L, new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(208), true, "Under Wash", 400m, "" },
                    { 5L, new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(210), true, "Vacuum", 400m, "" },
                    { 6L, new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(213), true, "Wax Machine", 1000m, "" },
                    { 7L, new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(215), true, "Waxing", 400m, "" },
                    { 8L, new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(217), true, "Rim Wash", 1000m, "" },
                    { 9L, new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(220), true, "Buffing", 500m, "" }
                });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7183));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7191));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7196));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7202));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7206));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7214));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7232));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7237));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9741));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8336));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8133));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8287));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8293));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7598));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7603));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7607));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7610));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7614));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7617));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7665));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7668));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7672));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7675));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7678));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7682));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7685));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8228));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8238));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8242));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8246));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8547));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8552));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8555));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9851));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9857));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9861));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8179));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8184));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8188));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8444));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8459));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8602));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8606));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8610));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8613));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8032));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8507));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8501));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9918));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9922));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9924));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9912));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9915));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9798));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "6f93f1ee-06bb-4a49-8407-8db013822cf8", new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3112), new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3111), new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3114), new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3107), "cd209333-57a0-40a2-93ec-65684e89edf3" });

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4598));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4602));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4604));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4606));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4608));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4610));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4612));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4614));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4616));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4618));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4620));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4623));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4625));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4627));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4629));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4631));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4633));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 18L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4635));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 19L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4637));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 20L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4640));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 21L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4642));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 22L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4644));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 23L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4665));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 24L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4668));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 25L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4670));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 26L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4672));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 27L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4674));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 28L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4676));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 29L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4678));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 30L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4680));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 31L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4682));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 32L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4684));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 33L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4687));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 34L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4689));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 35L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4691));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 36L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4693));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 37L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4694));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 38L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4696));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 39L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4698));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 40L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4700));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 41L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4702));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 42L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4705));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 43L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4706));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 44L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4709));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 45L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4711));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 46L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4713));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 47L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 48L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4716));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 49L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4718));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 50L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4720));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 51L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4722));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 52L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4724));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 53L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4726));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 54L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4728));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2422));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2429));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2433));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2437));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2441));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2445));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2449));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2473));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2477));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4384));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3436));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3244));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3395));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3400));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2749));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2754));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2757));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2772));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2775));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2789));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2815));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2819));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2822));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2825));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2829));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2832));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(2836));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3335));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3344));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3349));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3353));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3644));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3659));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3662));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4477));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4485));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4488));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3294));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3299));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3302));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3534));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3546));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3701));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3705));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3709));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3712));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3162));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3596));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(3591));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4543));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4545));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4548));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4550));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4537));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4541));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 29, 51, 240, DateTimeKind.Unspecified).AddTicks(4431));
        }
    }
}
