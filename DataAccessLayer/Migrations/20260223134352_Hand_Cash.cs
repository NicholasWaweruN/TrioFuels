using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Hand_Cash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "ea7373cd-c504-4102-99fa-4e1bb5fdb6bd", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4878), new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4877), new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4879), new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4873), "0dd79e78-f79c-4eae-85b8-ec345fe2907a" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8405));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8410));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8520));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8524));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8526));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8529));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8532));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8534));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8536));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8539));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8541));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8543));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8548));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5165));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5033));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5122));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5125));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4317));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4342));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4345));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4347));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4349));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4351));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4353));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4355));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4357));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4359));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "DateCreated", "IsAppUsed" },
                values: new object[] { new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4361), true });

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "UserCode" },
                values: new object[] { new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5079), "99999" });

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("63be1611-b048-495b-a224-cfb3ca203a6a"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4659), "" },
                    { new Guid("d4159556-faaa-4364-b22c-4c7bc3711209"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4681), "" },
                    { new Guid("fac05c59-9dec-45d4-98f1-49ca4b98106c"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4677), "" },
                    { new Guid("fc735bb6-eb9b-45d7-a845-1c6c74543ecb"), "02", "Bulk App", "", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4673), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5244));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5256));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4933));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5480));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5476));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5200), new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("39b0ca6f-2d67-44a6-ad14-53c182fced7d"), "01", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4990), "99999" },
                    { new Guid("9254d65d-de68-4b87-b4fc-69f36fcbffbb"), "04", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4987), "99999" },
                    { new Guid("a1e766dc-296e-43fe-9b01-99937ea234cb"), "02", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4994), "99999" },
                    { new Guid("b845469a-5909-454e-b831-a085130f8113"), "03", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4981), "99999" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("63be1611-b048-495b-a224-cfb3ca203a6a"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("d4159556-faaa-4364-b22c-4c7bc3711209"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("fac05c59-9dec-45d4-98f1-49ca4b98106c"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("fc735bb6-eb9b-45d7-a845-1c6c74543ecb"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("39b0ca6f-2d67-44a6-ad14-53c182fced7d"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("9254d65d-de68-4b87-b4fc-69f36fcbffbb"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("a1e766dc-296e-43fe-9b01-99937ea234cb"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("b845469a-5909-454e-b831-a085130f8113"));

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
                columns: new[] { "DateCreated", "IsAppUsed" },
                values: new object[] { new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(636), false });

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "UserCode" },
                values: new object[] { new DateTime(2026, 2, 21, 16, 48, 57, 513, DateTimeKind.Utc).AddTicks(1604), "00001" });

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
    }
}
