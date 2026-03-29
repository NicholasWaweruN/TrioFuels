using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class MpesaTransactions_DateModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("5904b303-d852-4969-b659-67da3fbda4df"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("a989e807-5bea-4894-accb-7c698a05108c"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("a9f536be-0805-4c43-84a9-df2789e917dc"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("c449a262-ad99-405e-9cb5-1e53b40ca2d5"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("8700ef75-db3c-4843-a83f-928e3274db82"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("98b54821-6e5d-4a6a-8b7c-fb43a71dd4ab"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("9edcce50-28c9-40c1-b9b7-cb605d560b96"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("a1b2a598-9905-4f1d-962d-d34f78f49924"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "MpesaTransactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "154fb896-59e6-4f08-893c-a74a99193314", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1272), new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1271), new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1274), new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1265), "bfcad9ce-8823-4151-b7f5-5939cd31f0b9" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(238));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(245));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(249));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(253));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(257));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(261));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(266));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(271));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(274));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(278));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(281));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(285));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(290));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(293));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(317));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2346));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1825));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2251));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2256));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(825));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(829));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(834));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(837));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(840));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(843));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(846));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(849));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(851));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(854));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(857));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(860));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(863));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2032));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1944));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("18cad7c8-e800-4bb9-9f39-795a559c90c6"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1022), "" },
                    { new Guid("a50ba5f0-362f-4bf8-9598-b9835d93ea17"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1006), "" },
                    { new Guid("ccaac53e-e9ab-4dbb-bd8d-5a9809e5f399"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(994), "" },
                    { new Guid("f45bb2fe-77b6-41f5-a67e-436af2f170e9"), "02", "Bulk App", "", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1001), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2467));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2484));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1468));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2548));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2542));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2402), new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2404) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("6b1da1f6-def8-48ed-9696-a90385fe0eed"), "02", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1664), "99999" },
                    { new Guid("aa4b6e69-5f9d-4cc5-ac1a-276d7efc533a"), "03", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1627), "99999" },
                    { new Guid("cdc928c4-5c9a-426f-b86d-22a396662f79"), "04", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1648), "99999" },
                    { new Guid("d31e7152-df11-4ddf-85ca-8df7afa71351"), "01", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1657), "99999" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("18cad7c8-e800-4bb9-9f39-795a559c90c6"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("a50ba5f0-362f-4bf8-9598-b9835d93ea17"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("ccaac53e-e9ab-4dbb-bd8d-5a9809e5f399"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("f45bb2fe-77b6-41f5-a67e-436af2f170e9"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("6b1da1f6-def8-48ed-9696-a90385fe0eed"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("aa4b6e69-5f9d-4cc5-ac1a-276d7efc533a"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("cdc928c4-5c9a-426f-b86d-22a396662f79"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("d31e7152-df11-4ddf-85ca-8df7afa71351"));

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "MpesaTransactions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "3ef3d5ed-03eb-4313-8227-e34fc980729c", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5892), new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5891), new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5894), new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5885), "bcfcb4ef-b551-466e-afc4-0245032cfd2c" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4834));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4841));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4845));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4849));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4853));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4856));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4860));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4863));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4866));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4870));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4873));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4876));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4882));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4885));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4898));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6280));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6075));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6228));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6232));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5354));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5358));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5360));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5364));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5368));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5371));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5375));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5377));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5380));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5382));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5384));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5387));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5390));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6180));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6136));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("5904b303-d852-4969-b659-67da3fbda4df"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5646), "" },
                    { new Guid("a989e807-5bea-4894-accb-7c698a05108c"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5662), "" },
                    { new Guid("a9f536be-0805-4c43-84a9-df2789e917dc"), "02", "Bulk App", "", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5641), "" },
                    { new Guid("c449a262-ad99-405e-9cb5-1e53b40ca2d5"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5636), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6373));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6387));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5952));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6444));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6438));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6320), new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6321) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("8700ef75-db3c-4843-a83f-928e3274db82"), "04", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6012), "99999" },
                    { new Guid("98b54821-6e5d-4a6a-8b7c-fb43a71dd4ab"), "01", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6023), "99999" },
                    { new Guid("9edcce50-28c9-40c1-b9b7-cb605d560b96"), "02", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6028), "99999" },
                    { new Guid("a1b2a598-9905-4f1d-962d-d34f78f49924"), "03", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6007), "99999" }
                });
        }
    }
}
