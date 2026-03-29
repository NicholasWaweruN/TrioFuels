using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Added2ndNozzle1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("416e04bb-3da5-4085-8661-543cfea4938e"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("8e190461-76a7-480f-8b19-bba605a34662"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("c2be3dd7-0c5f-4a4b-82e4-b1ea02fc7344"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("f1a08308-673c-431d-a0c8-d84da08020ec"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("2ac54ba4-c394-4529-a08e-30f9e7378055"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "ced25ebc-7050-4842-b287-b88a56aaa006", new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7332), new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7331), new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7332), new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7327), "af7ec6d3-e8fc-4f83-8c1f-ef45f8d2f5b6" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(6812));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(6816));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(6818));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(6819));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(6821));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(6823));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(6825));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(6826));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(6828));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(6829));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(6831));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(6832));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(6835));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(6836));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7490));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7418));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7464));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateCreated", "NozzleName" },
                values: new object[] { new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7466), "N02" });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7079));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7082));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7083));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7085));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7096));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7097));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7100));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7101));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7102));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7103));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7439));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("115a48e2-2f91-401f-8501-8084cf7a76b8"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7168), "" },
                    { new Guid("8d61f5d8-74e0-414e-be7a-8832302fa0a9"), "02", "Bulk App", "", new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7172), "" },
                    { new Guid("e8e92bb8-44ab-43df-b68f-9340801dcdd8"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7187), "" },
                    { new Guid("eadeeab6-4582-42d7-b34d-1b61233ef8bb"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7184), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7535));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7545));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7366));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7573));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7571));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7510), new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7511) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[] { new Guid("e6b94eae-5807-4f6c-9f21-45cde34a13be"), "03", new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7394), "99999" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("115a48e2-2f91-401f-8501-8084cf7a76b8"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("8d61f5d8-74e0-414e-be7a-8832302fa0a9"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("e8e92bb8-44ab-43df-b68f-9340801dcdd8"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("eadeeab6-4582-42d7-b34d-1b61233ef8bb"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("e6b94eae-5807-4f6c-9f21-45cde34a13be"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "0e1e0e95-d575-4bf1-94f0-25fec53d8c64", new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8547), new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8546), new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8547), new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8542), "242b6129-e10b-4120-9e23-5f61022b07e9" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8040));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8044));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8046));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8048));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8050));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8051));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8053));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8055));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8056));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8058));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8059));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8061));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8063));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8064));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8691));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8622));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8666));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateCreated", "NozzleName" },
                values: new object[] { new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8668), "N01" });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8292));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8295));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8296));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8297));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8299));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8300));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8301));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8303));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8304));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8305));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8306));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8644));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("416e04bb-3da5-4085-8661-543cfea4938e"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8401), "" },
                    { new Guid("8e190461-76a7-480f-8b19-bba605a34662"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8395), "" },
                    { new Guid("c2be3dd7-0c5f-4a4b-82e4-b1ea02fc7344"), "02", "Bulk App", "", new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8399), "" },
                    { new Guid("f1a08308-673c-431d-a0c8-d84da08020ec"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8403), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8733));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8745));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8575));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8771));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8768));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8709), new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[] { new Guid("2ac54ba4-c394-4529-a08e-30f9e7378055"), "03", new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8605), "99999" });
        }
    }
}
