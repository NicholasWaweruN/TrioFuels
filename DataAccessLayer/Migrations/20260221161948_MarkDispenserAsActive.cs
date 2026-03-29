using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class MarkDispenserAsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "01755a77-a915-4b8e-b900-6693b19d8236", new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(7194), new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(7193), new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(7196), new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(7186), "684a1d26-119b-4c88-8e4f-6e40d94d6305" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(5753));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(5761));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(5765));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(5771));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(5774));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(5777));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(5781));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(5784));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(5788));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(5791));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(5794));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(5798));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(5801));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(5805));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(7676));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(7455));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(7597));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(7604));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(6557));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(6563));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(6566));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(6569));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(6572));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(6574));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(6577));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(6580));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(6583));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(6586));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(6588));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(7530));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("4b90aa88-596d-4176-85b8-892c1e884d29"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(6772), "" },
                    { new Guid("737aeab3-3e19-44b3-9f81-d826e6430b9a"), "02", "Bulk App", "", new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(6766), "" },
                    { new Guid("ac271dc9-91be-42fb-a170-f4ea73c20168"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(6758), "" },
                    { new Guid("f99023de-1464-4774-b64d-610259294a87"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(6778), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(7944));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(7963));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(7302));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(8055));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(8048));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(7855), new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(7856) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[] { new Guid("56952a37-d309-408e-9746-9e429f9d2c6c"), "03", new DateTime(2026, 2, 21, 16, 19, 46, 469, DateTimeKind.Utc).AddTicks(7390), "99999" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("4b90aa88-596d-4176-85b8-892c1e884d29"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("737aeab3-3e19-44b3-9f81-d826e6430b9a"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("ac271dc9-91be-42fb-a170-f4ea73c20168"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("f99023de-1464-4774-b64d-610259294a87"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("56952a37-d309-408e-9746-9e429f9d2c6c"));

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
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 3, 13, 55, DateTimeKind.Utc).AddTicks(7466));

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
    }
}
