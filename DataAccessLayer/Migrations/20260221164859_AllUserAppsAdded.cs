using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AllUserAppsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "cc8dda27-9b67-432a-aed4-072bc16c793e", new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1375), new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1374), new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1377), new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1369), "cc0a686c-b181-497e-96d0-6392d12361c0" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(92));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(98));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(102));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(105));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(109));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(112));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(115));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(118));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(121));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(124));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(128));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(131));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(133));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(136));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1697));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1558));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1651));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1655));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(610));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(615));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(617));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(620));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(622));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(624));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(627));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(629));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(631));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(634));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(636));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1604));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("3d8cf9d1-775d-4143-9272-be84acd35b44"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1127), "" },
                    { new Guid("855d984c-4ed1-4b1f-a070-543334387a8d"), "02", "Bulk App", "", new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1117), "" },
                    { new Guid("e1f97e29-1e1a-404f-9e74-d0c965b67b94"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1122), "" },
                    { new Guid("f416ea1b-1b0c-4055-8f56-5e3160515250"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1110), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1776));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1791));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1436));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1843));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1839));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1734), new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1735) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("50c97c6e-e455-4e14-8143-f2fd6e3b7c6b"), "02", new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1515), "99999" },
                    { new Guid("90ede7c9-6512-4868-8535-4757d7daa754"), "04", new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1500), "99999" },
                    { new Guid("a8060196-bce6-4239-945f-a271019b86d7"), "03", new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1495), "99999" },
                    { new Guid("bb713895-962a-49dc-9b23-c0143c1900ab"), "01", new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1511), "99999" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("3d8cf9d1-775d-4143-9272-be84acd35b44"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("855d984c-4ed1-4b1f-a070-543334387a8d"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("e1f97e29-1e1a-404f-9e74-d0c965b67b94"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("f416ea1b-1b0c-4055-8f56-5e3160515250"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("50c97c6e-e455-4e14-8143-f2fd6e3b7c6b"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("90ede7c9-6512-4868-8535-4757d7daa754"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("a8060196-bce6-4239-945f-a271019b86d7"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("bb713895-962a-49dc-9b23-c0143c1900ab"));

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
    }
}
