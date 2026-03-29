using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class RolesAdministrator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("147a3041-9102-49c8-8744-6c6c791f54cd"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("a3457135-b9fb-4a1f-bd19-910b4d9344d6"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("b43c62d1-5318-41cf-ae17-800a2ad6a4e3"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("b7f2197a-06f9-4138-add1-218eb4c5670a"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("831eec75-9f4b-457c-a99e-7a8871c7f0c3"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("cbb126d1-5819-4132-918a-8b2588d0a9b5"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("eb008a96-56fa-4754-8c1d-6dbd2e38804f"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("f6b603bb-0aeb-4536-bb7e-17b9c4a6878e"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "604ab914-46fa-42f2-8435-b0f8b8a0589b", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1701), new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1700), new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1704), new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1693), "f5d6e2c9-bd02-41c8-8904-902e9ed99299" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(418));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(429));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(436));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(442));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(448));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(453));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(459));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(464));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(470));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(476));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(481));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(487));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(493));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(498));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(519));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2338));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2011));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2251));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2257));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1082));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1088));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1094));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1099));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1105));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1109));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1114));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1118));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1123));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1128));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1132));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1136));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2176));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "PetroleumName" },
                values: new object[] { new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2718), "Autogas" });

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2723));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2727));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2108));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("11aedac2-0371-486c-bc10-580d9a76b9cf"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1322), "" },
                    { new Guid("3b4fe686-6c38-4e00-a71c-0744244e5158"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1367), "" },
                    { new Guid("4207b92e-734a-4dbd-befa-3e5bc4f0b9be"), "02", "Bulk App", "", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1350), "" },
                    { new Guid("6a1b4e98-c062-4e1d-a655-66004ffd338f"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1359), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2531));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2558));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "DateCreated", "RoleCode", "RoleName", "UserCode" },
                values: new object[] { -1L, new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2823), "001", "Administrator", "99999" });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1804));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2644));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2637));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2445), new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2446) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("17a61aa8-e29c-4b48-8328-b05c6ce23231"), "04", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1915), "99999" },
                    { new Guid("35a3a313-7e72-432b-8603-0f92f1075001"), "03", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1905), "99999" },
                    { new Guid("89f4a963-4c58-494d-a2f1-01f2c5e53d27"), "02", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1932), "99999" },
                    { new Guid("f420bbb2-e979-4c85-8b02-7fb3d4597072"), "01", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1924), "99999" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("11aedac2-0371-486c-bc10-580d9a76b9cf"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("3b4fe686-6c38-4e00-a71c-0744244e5158"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("4207b92e-734a-4dbd-befa-3e5bc4f0b9be"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("6a1b4e98-c062-4e1d-a655-66004ffd338f"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("17a61aa8-e29c-4b48-8328-b05c6ce23231"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("35a3a313-7e72-432b-8603-0f92f1075001"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("89f4a963-4c58-494d-a2f1-01f2c5e53d27"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("f420bbb2-e979-4c85-8b02-7fb3d4597072"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "abf827b5-ca7e-4bf1-af20-eb17a6003651", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8749), new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8746), new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8751), new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8740), "af7dda7b-4273-4f3e-bfa0-92cdf7e77170" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7601));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7609));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7615));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7619));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7624));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7628));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7632));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7637));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7641));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7646));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7650));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7654));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7659));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7663));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7681));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9269));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8996));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9198));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9205));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8165));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8171));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8178));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8185));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8189));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8192));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8196));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8199));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8203));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8210));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8213));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8217));

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "DateCreated", "HasValue", "IsAppUsed", "PaymentTypeId", "PaymentTypeName", "ProcessType", "UserCode" },
                values: new object[,]
                {
                    { 3L, new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8175), true, false, 2, "New_Conversions", "", "00001" },
                    { 5L, new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8181), true, false, 4, "Bank_Transfer", "", "00001" },
                    { 12L, new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8206), true, false, 11, "Personal_Wallet", "", "00001" }
                });

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9138));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "PetroleumName" },
                values: new object[] { new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9667), "LPG" });

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9671));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9675));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9077));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("147a3041-9102-49c8-8744-6c6c791f54cd"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8469), "" },
                    { new Guid("a3457135-b9fb-4a1f-bd19-910b4d9344d6"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8445), "" },
                    { new Guid("b43c62d1-5318-41cf-ae17-800a2ad6a4e3"), "02", "Bulk App", "", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8456), "" },
                    { new Guid("b7f2197a-06f9-4138-add1-218eb4c5670a"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8463), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9509));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9532));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8831));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9604));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9597));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9333), new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9334) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("831eec75-9f4b-457c-a99e-7a8871c7f0c3"), "01", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8930), "99999" },
                    { new Guid("cbb126d1-5819-4132-918a-8b2588d0a9b5"), "03", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8908), "99999" },
                    { new Guid("eb008a96-56fa-4754-8c1d-6dbd2e38804f"), "02", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8936), "99999" },
                    { new Guid("f6b603bb-0aeb-4536-bb7e-17b9c4a6878e"), "04", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8918), "99999" }
                });
        }
    }
}
