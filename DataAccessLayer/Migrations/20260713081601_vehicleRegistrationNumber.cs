using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class vehicleRegistrationNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VehicleRegistrationNumber",
                table: "CarWashTransactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "d0e62e79-3f27-4430-847e-12207e75bbd8", new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8375), new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8373), new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8379), new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8367), "894452a0-86cf-4675-a4d4-969a894c4518" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7432));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7441));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7448));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7454));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7460));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7466));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7472));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7526));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 818, DateTimeKind.Unspecified).AddTicks(1215));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8874));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8582));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8802));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8811));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7877));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7883));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7889));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7894));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7899));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7904));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7948));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7953));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7958));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7963));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7968));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7973));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(7978));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8717));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8726));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8734));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8740));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9235));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9242));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9247));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 818, DateTimeKind.Unspecified).AddTicks(1370));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 818, DateTimeKind.Unspecified).AddTicks(1379));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 818, DateTimeKind.Unspecified).AddTicks(1385));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8650));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8656));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8662));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9034));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9077));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9315));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9321));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9327));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9332));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(8458));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9152));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 817, DateTimeKind.Unspecified).AddTicks(9144));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 13, 11, 15, 56, 818, DateTimeKind.Unspecified).AddTicks(1291));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleRegistrationNumber",
                table: "CarWashTransactions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "90c94e7b-78f2-48c6-96de-6545ee2ef174", new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9639), new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9638), new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9641), new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9632), "6b1980da-4f39-4c5c-967d-757c8420e452" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(8345));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(8354));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(8358));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(8362));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(8367));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(8371));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(8376));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(8400));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(8404));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(1604));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(8));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9798));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9951));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9955));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9212));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9217));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9220));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9222));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9225));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9237));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9298));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9301));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9304));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9306));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9309));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9312));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9315));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9896));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9901));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9905));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9909));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(722));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(726));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(729));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(1714));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(1719));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(1723));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9849));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9853));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9856));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(166));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(177));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(787));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(791));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(794));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(797));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 216, DateTimeKind.Unspecified).AddTicks(9698));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(240));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(236));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 35, 38, 217, DateTimeKind.Unspecified).AddTicks(1658));
        }
    }
}
