using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class forgotPassword_Otp_Email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Otps",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "320e5cd1-c2c4-42a0-8526-7a1200738fd9", new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1515), new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1515), new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1517), new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1512), "3589fb9e-1de0-4aad-91cf-40eedc0e0072" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(824));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(832));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(835));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(837));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(840));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(842));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(844));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(847));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(849));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(3144));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1818));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1652));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1773));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1779));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1134));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1137));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1140));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1141));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1143));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1145));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1147));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1150));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1152));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1154));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1157));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1159));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(2015));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(2018));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(2020));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(3235));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(3239));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(3242));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1693));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1696));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1698));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1912));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1923));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(2060));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(2063));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(2065));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(2067));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1567));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1963));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(1960));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 16, 7, 36, 17, 709, DateTimeKind.Utc).AddTicks(3186));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Otps");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "bf6a374b-7b0b-452a-81ba-b891f2ce13e0", new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7580), new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7579), new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7581), new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7576), "2bd7713c-ce60-490f-b136-2dd69d457ad7" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6968));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6971));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6973));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6975));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6977));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6978));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6980));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6981));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6983));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8793));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8105));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7810));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8075));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8078));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7338));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7341));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7342));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7344));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7346));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7347));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7349));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7351));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7352));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7354));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7355));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7357));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8050));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8262));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8265));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8267));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8988));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8991));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8993));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7839));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7840));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7842));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8183));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8194));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8293));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8296));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8297));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8299));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7613));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8227));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8222));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8958));
        }
    }
}
