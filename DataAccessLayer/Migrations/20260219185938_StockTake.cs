using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class StockTake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("2ea66a63-fe01-4bbf-976a-09da14f11d7c"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("5ab494ea-6b1f-4a4d-a721-a99aa052e0c9"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("b7ab0664-fce0-417b-b8db-5333c781551e"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("d1a55901-565f-4625-a336-30a6cc4fce7b"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("9281547b-a261-47ae-b46d-01d9f2eea150"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "69836f82-41a5-4ac6-8b7d-e8d70682e66f", new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4590), new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4589), new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4593), new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4582), "0eb01add-ef5c-4537-818a-64b78baa3173" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(3283));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(3290));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(3295));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(3299));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(3303));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(3307));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(3311));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(3314));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(3318));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(3322));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(3326));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(3330));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(3334));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(3338));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(5096));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4855));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(5026));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4076));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4080));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4083));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4086));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4090));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4093));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4096));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4099));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4102));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4105));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4949));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("14aea328-438d-4111-b448-f349be27252b"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4291), "" },
                    { new Guid("4dd22403-a041-4233-8ee5-0e4607be131d"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4258), "" },
                    { new Guid("c14a7630-6186-4ed6-bb0f-ef92b2ad304c"), "02", "Bulk App", "", new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4268), "" },
                    { new Guid("e9d351b3-408a-45c1-a2f0-393719535ca7"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4298), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(5230));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4693));

            migrationBuilder.InsertData(
                table: "StockTakes",
                columns: new[] { "Id", "ClosingReading", "DateCreated", "NozzleCode", "OpeningReading", "ShiftNumber", "TakeType", "UserCode" },
                values: new object[] { 1L, 0m, new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(5313), "N01", 0m, "", 99, "99999" });

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(5161), new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(5163) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[] { new Guid("3da28478-402a-4ee5-aba0-9d2e1532b118"), "03", new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4788), "99999" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("14aea328-438d-4111-b448-f349be27252b"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("4dd22403-a041-4233-8ee5-0e4607be131d"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("c14a7630-6186-4ed6-bb0f-ef92b2ad304c"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("e9d351b3-408a-45c1-a2f0-393719535ca7"));

            migrationBuilder.DeleteData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("3da28478-402a-4ee5-aba0-9d2e1532b118"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "0bc6c5cb-3ee5-407f-8c43-0c855a7e4f96", new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7388), new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7387), new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7389), new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7382), "a0f68cdd-6a0a-4116-87af-25b294a2358c" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6264));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6270));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6274));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6277));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6281));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6284));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6287));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6290));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6293));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6296));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6300));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6303));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6306));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6309));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7719));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7555));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7667));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6970));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6974));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6977));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6980));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6982));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6985));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6987));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6990));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6992));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6995));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(6997));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7612));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("2ea66a63-fe01-4bbf-976a-09da14f11d7c"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7156), "" },
                    { new Guid("5ab494ea-6b1f-4a4d-a721-a99aa052e0c9"), "02", "Bulk App", "", new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7132), "" },
                    { new Guid("b7ab0664-fce0-417b-b8db-5333c781551e"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7126), "" },
                    { new Guid("d1a55901-565f-4625-a336-30a6cc4fce7b"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7137), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7825));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7453));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7767), new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7767) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[] { new Guid("9281547b-a261-47ae-b46d-01d9f2eea150"), "03", new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7513), "99999" });
        }
    }
}
