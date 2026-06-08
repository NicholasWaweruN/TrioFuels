using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Product_And_Prices_Configurations_In_The_Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "b0060030-7c56-4cd3-9e87-00bcf636bc4c", new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5428), new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5427), new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5430), new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5424), "8e0b9378-b0f1-413f-8446-fd665bdad54c" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(4857));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(4861));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(4864));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(4867));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(4869));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(4872));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(4876));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(4879));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(4881));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(4883));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(4885));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(4888));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(4890));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(4892));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(6550));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5691));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5548));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5656));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5659));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5154));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5157));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5159));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5160));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5162));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5164));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5166));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5168));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5169));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5171));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5173));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5175));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5622));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5834));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5837));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5839));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateCreated", "IsActive", "ProductCode", "ProductName", "UserCode" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5583), true, "02", "Diesel", "000001" },
                    { 2L, new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5588), true, "01", "Petrol", "000001" },
                    { 3L, new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5590), true, "03", "Autogas", "000001" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5753));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5761));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5872));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5472));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5794));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5790));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5723), new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(5724) });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 34, 31, 18, DateTimeKind.Utc).AddTicks(6587));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "39d7aef3-02ea-4c64-a059-7a0c3bcba707", new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3775), new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3774), new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3776), new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3772), "efd5812a-47c1-406b-8c3e-1bc6939f2f62" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3188));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3192));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3194));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3196));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3198));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3200));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3202));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3204));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3206));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3208));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3209));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3211));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3213));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3215));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3227));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4902));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4018));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3888));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3982));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3984));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3492));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3494));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3496));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3498));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3499));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3501));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3502));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3504));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3506));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3507));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3509));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3511));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4191));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4194));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4195));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateCreated", "IsActive", "ProductCode", "ProductName", "UserCode" },
                values: new object[] { -1L, new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3922), true, "02", "Diesel", "000001" });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4083));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4091));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4227));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3815));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4129));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4126));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4054), new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4054) });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4939));
        }
    }
}
