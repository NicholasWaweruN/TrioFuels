using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class PaymentsTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000004"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "1548cc7a-f22a-4d39-823f-42fc6f24df9e", new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3907), new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3906), new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3908), new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3903), "b9dec3fc-2727-44f5-8b0f-42462b811b25" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3077));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3083));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3087));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3090));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3093));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3096));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3100));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3103));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3106));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(5005));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4158));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4020));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4121));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3517));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateCreated", "IsAppUsed" },
                values: new object[] { new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3520), false });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3522));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3525));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3526));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3528));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3530));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3532));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3534));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3535));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3537));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3540));

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "DateCreated", "HasValue", "IsAppUsed", "PaymentTypeId", "PaymentTypeName", "ProcessType", "UserCode" },
                values: new object[] { 16L, new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3542), true, true, 15, "PDQ", "", "00001" });

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4089));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4339));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4342));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4344));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(5086));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(5090));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(5092));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4056));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4059));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4061));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4248));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4258));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4379));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4381));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4383));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4385));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3953));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4293));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4290));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(5038));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "ac5b0bba-ab17-47ad-8f3c-ccf82eb251ae", new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6029), new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6029), new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6030), new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6025), "b442579e-a81d-46c2-bcb0-7a7167925929" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5340));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5344));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5346));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5349));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5351));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5354));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5356));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5359));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5361));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(7506));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6325));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6166));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6288));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6293));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5747));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateCreated", "IsAppUsed" },
                values: new object[] { new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5749), true });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5752));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5754));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5756));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5758));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5760));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5761));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5763));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5765));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5767));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(5769));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6251));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6507));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6509));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6511));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(7597));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(7601));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(7603));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6217));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6220));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6223));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("11111111-0000-0000-0000-000000000001"), "01", "Bulk DashBoard", "", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "" },
                    { new Guid("11111111-0000-0000-0000-000000000002"), "02", "Bulk App", "", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6413));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6423));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6550));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6553));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6555));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6556));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6077));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6464));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(6460));

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("22222222-0000-0000-0000-000000000003"), "01", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "99999" },
                    { new Guid("22222222-0000-0000-0000-000000000004"), "02", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "99999" }
                });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 6, 4, 32, 457, DateTimeKind.Utc).AddTicks(7540));
        }
    }
}
