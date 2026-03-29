using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Real_Payment_Methods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("14ebbbaa-119c-4388-9c92-6a365df6c585"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("35bda58b-b5f8-407d-9e93-03f314e60a4b"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("48ef101d-ed69-42ef-ae35-7d7b5449907d"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("df392818-33b1-4e53-8c54-717a138e04e9"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("0cbce2d1-ca85-4d9e-8062-3135ed45852c"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("6ff0cc5c-8fa2-4f64-8f06-93ee6b3c4cbc"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("a875f6e4-394f-4fa3-aef1-471073ef4dcf"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("d10f8bb0-519a-4486-8887-131a68ba04a2"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "1570eb76-66fc-4bea-924a-ae06bbee70c1", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5113), new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5112), new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5115), new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5107), "8344e7cf-ff9a-45f0-9b93-b47abfbbd3a0" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4120));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4126));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4130));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4133));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4136));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4140));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4143));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4146));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4149));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4152));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4155));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4158));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4162));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4165));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4182));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5459));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5304));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5404));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5409));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4692));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4696));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4699));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4701));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4704));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4706));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4709));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4711));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4717));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4719));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "DateCreated", "IsAppUsed" },
                values: new object[] { new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4722), false });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4724));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5353));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("02a1bd80-8ec3-4b86-8acf-3e4d08636a6c"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4853), "" },
                    { new Guid("776c3c68-3374-4ac6-97c1-cc3ab0482a44"), "02", "Bulk App", "", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4872), "" },
                    { new Guid("cbfe2656-5e1e-4714-9301-1725f5f439e1"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4877), "" },
                    { new Guid("d4132851-9e14-4a26-9ede-e31204c4ed3b"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4882), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5573));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5589));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5176));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5653));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5647));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5516), new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5517) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("7a8518a5-b21f-4c46-a422-fab2316d30b3"), "03", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5233), "99999" },
                    { new Guid("b8b39660-219e-4a88-8724-b4e940311cd1"), "04", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5239), "99999" },
                    { new Guid("bde2c450-bd62-431a-9a51-b918bedc61d6"), "02", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5254), "99999" },
                    { new Guid("f1c747be-1bb2-49af-9751-54b7338212ec"), "01", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5244), "99999" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("02a1bd80-8ec3-4b86-8acf-3e4d08636a6c"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("776c3c68-3374-4ac6-97c1-cc3ab0482a44"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("cbfe2656-5e1e-4714-9301-1725f5f439e1"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("d4132851-9e14-4a26-9ede-e31204c4ed3b"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("7a8518a5-b21f-4c46-a422-fab2316d30b3"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("b8b39660-219e-4a88-8724-b4e940311cd1"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("bde2c450-bd62-431a-9a51-b918bedc61d6"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("f1c747be-1bb2-49af-9751-54b7338212ec"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "47a87fa0-b443-4111-90d2-2bcc750d3700", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8566), new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8565), new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8568), new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8558), "a8fc3619-de96-4c88-88cc-331ce367c912" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7244));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7252));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7257));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7261));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7265));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7269));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7273));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7276));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7280));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7283));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7286));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7290));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7312));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7316));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7319));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9118));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8871));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9015));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9021));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7992));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7998));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8002));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8005));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8008));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8011));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8014));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8017));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8020));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8023));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8026));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "DateCreated", "IsAppUsed" },
                values: new object[] { new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8029), true });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8032));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8942));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("14ebbbaa-119c-4388-9c92-6a365df6c585"), "02", "Bulk App", "", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8209), "" },
                    { new Guid("35bda58b-b5f8-407d-9e93-03f314e60a4b"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8200), "" },
                    { new Guid("48ef101d-ed69-42ef-ae35-7d7b5449907d"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8250), "" },
                    { new Guid("df392818-33b1-4e53-8c54-717a138e04e9"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8257), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9260));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9278));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8676));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9363));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9355));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9185), new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9186) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("0cbce2d1-ca85-4d9e-8062-3135ed45852c"), "01", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8792), "99999" },
                    { new Guid("6ff0cc5c-8fa2-4f64-8f06-93ee6b3c4cbc"), "02", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8798), "99999" },
                    { new Guid("a875f6e4-394f-4fa3-aef1-471073ef4dcf"), "03", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8768), "99999" },
                    { new Guid("d10f8bb0-519a-4486-8887-131a68ba04a2"), "04", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8786), "99999" }
                });
        }
    }
}
