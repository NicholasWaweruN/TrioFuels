using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class INITIA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("05950d1b-a000-4337-8b34-85cf217b82ba"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("16306921-897d-45a4-996b-d283110db21f"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("1771a19b-ca7e-465e-a0d1-499c7d9c9545"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("791789b7-968a-497e-9ff0-d13c2661deeb"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("3c974271-daad-4b88-94fb-749e53682195"));

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

            migrationBuilder.InsertData(
                table: "QuantityTransactions",
                columns: new[] { "Id", "AmountCredit", "AmountDebit", "DateCreated", "Discount", "DispenserCode", "IsReversed", "NozzleCode", "OtpUsed", "PaymentTypeCode", "Price", "QuantityCredit", "QuantityDebit", "SaleId", "ShiftNumber", "StationCode", "UserCode", "Vat_Amount", "VehicleCode" },
                values: new object[] { 1L, 0m, 0m, new DateTime(2026, 2, 19, 18, 28, 50, 335, DateTimeKind.Utc).AddTicks(7825), 0m, "D01", false, "N01", "", 99, 0m, 0m, 0m, "", "", "S001", "99999", 0m, "" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("9281547b-a261-47ae-b46d-01d9f2eea150"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "ebf87ed3-16e4-4666-9f61-16a1cffd1d8f", new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5945), new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5944), new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5947), new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5936), "e8eab77c-ce9a-4a49-85f9-8c61e0d2ef4d" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(4211));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(4218));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(4527));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(4534));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(4538));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(4541));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(4545));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(4549));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(4552));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(4556));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(4560));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(4563));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(4568));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(4572));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(6579));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(6292));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(6484));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5357));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5364));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5367));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5371));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5374));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5377));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5381));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5384));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5387));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5390));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5393));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(6389));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("05950d1b-a000-4337-8b34-85cf217b82ba"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5594), "" },
                    { new Guid("16306921-897d-45a4-996b-d283110db21f"), "02", "Bulk App", "", new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5602), "" },
                    { new Guid("1771a19b-ca7e-465e-a0d1-499c7d9c9545"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5625), "" },
                    { new Guid("791789b7-968a-497e-9ff0-d13c2661deeb"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(5608), "" }
                });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(6093));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(6666), new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(6667) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[] { new Guid("3c974271-daad-4b88-94fb-749e53682195"), "03", new DateTime(2026, 2, 19, 18, 17, 33, 431, DateTimeKind.Utc).AddTicks(6210), "99999" });
        }
    }
}
