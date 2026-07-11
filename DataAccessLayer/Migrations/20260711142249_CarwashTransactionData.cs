using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class CarwashTransactionData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarWashTransactionItems_CarWashProducts_ProductId",
                table: "CarWashTransactionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CarWashTransactions_CarWashShifts_ShiftId",
                table: "CarWashTransactions");

            migrationBuilder.AlterColumn<string>(
                name: "TillNumber",
                table: "StkTransactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "StkTransactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ResultDescription",
                table: "StkTransactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ResultCode",
                table: "StkTransactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "StkTransactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MpesaReceiptNumber",
                table: "StkTransactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "MerchantRequestId",
                table: "StkTransactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CheckoutRequestId",
                table: "StkTransactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "AccountReference",
                table: "StkTransactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "de97bb89-8172-4d97-8f85-98e22c30603e", new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2741), new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2740), new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2743), new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2736), "0295d7a5-4b4b-45c6-9f3a-49fb4d620ef5" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(1940));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(1948));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(1953));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(1956));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(1960));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(1964));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(1968));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(1990));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(1995));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(4199));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3114));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2894));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3057));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3063));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2359));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2366));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2371));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2375));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2379));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2384));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2410));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2414));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2418));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2421));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2425));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2429));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2432));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2995));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3001));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3007));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3011));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3391));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3396));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3399));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(4323));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(4329));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(4334));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2944));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2948));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2952));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3259));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3272));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3449));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3453));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3456));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3460));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(2802));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3326));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(3320));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 17, 22, 47, 854, DateTimeKind.Unspecified).AddTicks(4258));

            migrationBuilder.AddForeignKey(
                name: "FK_CarWashTransactionItems_CarWashProducts_ProductId",
                table: "CarWashTransactionItems",
                column: "ProductId",
                principalTable: "CarWashProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarWashTransactions_CarWashShifts_ShiftId",
                table: "CarWashTransactions",
                column: "ShiftId",
                principalTable: "CarWashShifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarWashTransactionItems_CarWashProducts_ProductId",
                table: "CarWashTransactionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CarWashTransactions_CarWashShifts_ShiftId",
                table: "CarWashTransactions");

            migrationBuilder.AlterColumn<string>(
                name: "TillNumber",
                table: "StkTransactions",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "StkTransactions",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ResultDescription",
                table: "StkTransactions",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ResultCode",
                table: "StkTransactions",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "StkTransactions",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "MpesaReceiptNumber",
                table: "StkTransactions",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "MerchantRequestId",
                table: "StkTransactions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CheckoutRequestId",
                table: "StkTransactions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AccountReference",
                table: "StkTransactions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "86049a1e-3d40-4953-b336-9be986165283", new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2755), new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2753), new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2758), new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2745), "d167bdb6-58ac-4f7a-995b-ca247ad10b0c" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(1475));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(1487));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(1492));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(1498));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(1503));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(1508));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(1513));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(1535));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(1541));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(5500));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(3365));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2998));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(3280));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(3286));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2232));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2238));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2243));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2248));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2253));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2269));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2311));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2316));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2321));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2330));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2335));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2340));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(3182));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(3192));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(3198));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(3204));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(4135));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(4142));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(4147));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(5688));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(5697));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(5703));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(3102));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(3108));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(3113));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(3564));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(3583));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(4229));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(4237));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(4242));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(4247));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(2853));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(4035));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(4022));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 11, 15, 18, 59, 359, DateTimeKind.Unspecified).AddTicks(5596));

            migrationBuilder.AddForeignKey(
                name: "FK_CarWashTransactionItems_CarWashProducts_ProductId",
                table: "CarWashTransactionItems",
                column: "ProductId",
                principalTable: "CarWashProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarWashTransactions_CarWashShifts_ShiftId",
                table: "CarWashTransactions",
                column: "ShiftId",
                principalTable: "CarWashShifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
