using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class NozzleCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("3da28478-402a-4ee5-aba0-9d2e1532b118"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "bff3a690-313a-4e1f-966f-5db58b25966c", new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5814), new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5813), new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5815), new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5810), "7266118f-b584-4c92-ab23-a23097c6c96e" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5183));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5188));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5190));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5193));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5195));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5197));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5199));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5201));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5203));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5205));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5207));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5209));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5212));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5214));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(6010));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5915));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "NozzleCode" },
                values: new object[] { new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5978), "N01" });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5479));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5554));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5556));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5557));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5559));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5561));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5562));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5564));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5566));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5568));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5569));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5948));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("604b03d1-7ae4-407d-baa2-8f6cbdedfc30"), "02", "Bulk App", "", new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5657), "" },
                    { new Guid("88678da7-5b89-4652-a038-d2d98b840c70"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5661), "" },
                    { new Guid("937a110e-d245-42d2-b1d4-6a5e50e935b9"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5654), "" },
                    { new Guid("bd7c1a23-b2db-4762-b879-c65c2f2ffd64"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5674), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "QuantityCredit" },
                values: new object[] { new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(6070), 50m });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5851));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "OpeningReading" },
                values: new object[] { new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(6107), 50m });

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(6037), new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(6038) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[] { new Guid("f7a91d61-23f7-46c1-bfd5-bc102dbd35a7"), "03", new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5887), "99999" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("604b03d1-7ae4-407d-baa2-8f6cbdedfc30"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("88678da7-5b89-4652-a038-d2d98b840c70"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("937a110e-d245-42d2-b1d4-6a5e50e935b9"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("bd7c1a23-b2db-4762-b879-c65c2f2ffd64"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("f7a91d61-23f7-46c1-bfd5-bc102dbd35a7"));

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
                columns: new[] { "DateCreated", "NozzleCode" },
                values: new object[] { new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(5026), "S001" });

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
                columns: new[] { "DateCreated", "QuantityCredit" },
                values: new object[] { new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(5230), 0m });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(4693));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "OpeningReading" },
                values: new object[] { new DateTime(2026, 2, 19, 18, 59, 35, 36, DateTimeKind.Utc).AddTicks(5313), 0m });

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
    }
}
