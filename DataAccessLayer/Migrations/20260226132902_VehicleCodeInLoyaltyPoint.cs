using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class VehicleCodeInLoyaltyPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("5074523e-0334-48c0-9f92-f998e4f58fdf"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("584fa610-ff1b-4627-ad8b-128283e02378"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("79025205-fb83-4b36-bf25-18402c8963e2"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("867fb851-2cf4-4f3a-bde5-5a3ee90dcb91"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("04d35bc8-1b85-4c4d-9031-18f9bd29c9bf"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("1eca0ef1-8608-4d42-a2d3-40021d24336d"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("909e6cc2-543c-4c73-98d2-cdd5355b76b7"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("f850d99d-20b5-41ca-8ae6-9ea240596a31"));

            migrationBuilder.AddColumn<string>(
                name: "VehicleCode",
                table: "RoyaltyPoints",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "d6530d7b-89da-4ae3-89bb-939fea5ced54", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9489), new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9488), new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9490), new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9483), "1a6af186-ae1b-4f5b-9a0f-6f79207744f6" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8734));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8740));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8743));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8747));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8750));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8753));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8756));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8758));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8761));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8764));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8767));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8769));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8772));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8775));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8787));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9860));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9650));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9813));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9817));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9128));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9131));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9134));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9136));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9139));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9143));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9145));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9148));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9150));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9153));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9155));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9157));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9694));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("96ec4530-e9c4-4521-848e-bea2ad33c563"), "02", "Bulk App", "", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9284), "" },
                    { new Guid("b46898e9-8964-430d-ade7-18001880a974"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9288), "" },
                    { new Guid("bcb06588-c5b8-4555-b04c-f2c25a4b0748"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9292), "" },
                    { new Guid("d4d5d811-6153-48ff-a2b3-25017a1297e7"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9279), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9939));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9954));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9543));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 691, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9996));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9896), new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9897) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("85e7f4cd-6df9-44bd-b9a9-db6c81458b46"), "03", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9591), "99999" },
                    { new Guid("c5b90c68-34c9-4467-b721-d841b459c0dc"), "04", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9596), "99999" },
                    { new Guid("f0da02fc-2bfb-4a93-91d5-817f5bec873f"), "02", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9608), "99999" },
                    { new Guid("fdd15f4f-c4be-4e7d-8a89-cab31decc65e"), "01", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9604), "99999" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("96ec4530-e9c4-4521-848e-bea2ad33c563"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("b46898e9-8964-430d-ade7-18001880a974"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("bcb06588-c5b8-4555-b04c-f2c25a4b0748"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("d4d5d811-6153-48ff-a2b3-25017a1297e7"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("85e7f4cd-6df9-44bd-b9a9-db6c81458b46"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("c5b90c68-34c9-4467-b721-d841b459c0dc"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("f0da02fc-2bfb-4a93-91d5-817f5bec873f"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("fdd15f4f-c4be-4e7d-8a89-cab31decc65e"));

            migrationBuilder.DropColumn(
                name: "VehicleCode",
                table: "RoyaltyPoints");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "38b4cc1e-ebda-43f0-a820-3b44e113bbc3", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6146), new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6145), new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6148), new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6139), "324a5974-d881-4d94-96ff-4b297fb2822f" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4758));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4767));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4772));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4776));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4780));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4784));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4787));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4791));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4795));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4799));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4802));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4806));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4810));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4814));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4840));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6731));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6478));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6644));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6650));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5500));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5506));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5510));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5513));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5516));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5519));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5522));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5525));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5528));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5531));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5533));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5536));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5539));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6565));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("5074523e-0334-48c0-9f92-f998e4f58fdf"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5718), "" },
                    { new Guid("584fa610-ff1b-4627-ad8b-128283e02378"), "02", "Bulk App", "", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5725), "" },
                    { new Guid("79025205-fb83-4b36-bf25-18402c8963e2"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5748), "" },
                    { new Guid("867fb851-2cf4-4f3a-bde5-5a3ee90dcb91"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5755), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6894));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6913));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6271));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(7000));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6994));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6806), new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6807) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("04d35bc8-1b85-4c4d-9031-18f9bd29c9bf"), "04", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6386), "99999" },
                    { new Guid("1eca0ef1-8608-4d42-a2d3-40021d24336d"), "01", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6393), "99999" },
                    { new Guid("909e6cc2-543c-4c73-98d2-cdd5355b76b7"), "02", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6399), "99999" },
                    { new Guid("f850d99d-20b5-41ca-8ae6-9ea240596a31"), "03", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6368), "99999" }
                });
        }
    }
}
