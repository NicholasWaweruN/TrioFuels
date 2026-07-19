using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class NozzleAsProductBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetroleumCode",
                table: "Dispensers");

            migrationBuilder.AddColumn<string>(
                name: "PetroleumCode",
                table: "Nozzles",
                type: "character varying(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "be721f96-4ead-49fa-b095-d7b73c60603f", new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3875), new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3874), new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3876), new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3870), "eb2c65bd-894e-4959-8d04-f62e54b378f5" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3421));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3431));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3434));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3437));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3440));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3443));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3445));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3448));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3451));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4742));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4077));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3958));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "PetroleumCode" },
                values: new object[] { new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4049), "03" });

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateCreated", "PetroleumCode" },
                values: new object[] { new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4053), "01" });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3627));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3630));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3633));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3645));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3648));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3650));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3653));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3655));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3657));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3660));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3663));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3666));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3668));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4014));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4019));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4022));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4024));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4221));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4233));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4236));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4808));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4812));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4814));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3984));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3987));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3989));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4146));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4156));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4261));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4264));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4266));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4269));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(3905));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4186));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4182));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 29, 22, 27, 29, 552, DateTimeKind.Unspecified).AddTicks(4772));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetroleumCode",
                table: "Nozzles");

            migrationBuilder.AddColumn<string>(
                name: "PetroleumCode",
                table: "Dispensers",
                type: "character varying(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "4a55d0a3-5914-43c1-abc7-d77c84f1d1bf", new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5511), new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5510), new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5513), new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5507), "0b6a794d-b9e5-4a08-a39d-e649021e36d0" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4738));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4747));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4751));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4754));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4757));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4760));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4763));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4780));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(4783));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6746));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5826));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "PetroleumCode" },
                values: new object[] { new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5654), "01" });

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5787));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5791));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5067));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5071));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5074));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5076));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5079));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5094));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5127));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5130));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5133));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5135));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5138));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5140));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5143));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5736));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5741));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5745));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5748));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6034));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6037));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6040));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6837));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6841));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6845));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5693));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5699));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5702));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5938));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5949));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6082));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6085));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6088));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6090));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5564));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5993));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(5988));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 27, 11, 1, 32, 96, DateTimeKind.Unspecified).AddTicks(6786));
        }
    }
}
