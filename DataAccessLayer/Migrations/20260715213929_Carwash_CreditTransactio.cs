using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Carwash_CreditTransactio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionDate",
                table: "CarwashCreditTransactions",
                newName: "DateCreated");

            migrationBuilder.AddColumn<decimal>(
                name: "AmountDue",
                table: "CarWashTransactions",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "CarWashTransactions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "CarWashTransactions",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<long>(
                name: "SaleId",
                table: "CarwashCreditTransactions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "CarwashCreditTransactions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "e50af510-16a9-4916-82cb-6cd8db897f51", new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(2479), new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(2473), new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(2490), new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(2464), "1fea4400-7b3e-480d-95e1-c3c6e1bc0a3e" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(953));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(962));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(968));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(974));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(981));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(986));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(992));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(1040));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(1046));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(5210));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3158));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(2835));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3080));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3087));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(1788));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(1795));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(1822));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(1827));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(1831));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(1835));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(1877));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(1883));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(1888));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(1894));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(1899));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(1904));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(1908));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(2983));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(2995));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3002));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3009));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3740));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3747));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3751));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(5376));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(5383));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(5388));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(2908));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(2916));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(2920));

            migrationBuilder.UpdateData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000005"),
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(2106));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3515));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3530));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3841));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3848));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3852));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3856));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(2655));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3621));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(3613));

            migrationBuilder.UpdateData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000003"),
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(2770));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 16, 0, 39, 27, 670, DateTimeKind.Unspecified).AddTicks(5292));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountDue",
                table: "CarWashTransactions");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CarWashTransactions");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "CarWashTransactions");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "CarwashCreditTransactions",
                newName: "TransactionDate");

            migrationBuilder.AlterColumn<int>(
                name: "SaleId",
                table: "CarwashCreditTransactions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CarwashCreditTransactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "9a162f90-e37b-4442-b2e5-99035d6d4472", new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8007), new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8006), new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8010), new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8002), "aa3143be-6673-48ee-aff4-05ab1d99f027" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7295));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7301));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7306));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7310));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7314));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7318));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7321));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7337));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7342));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(9251));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8316));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8139));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8275));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8281));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7650));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7654));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7658));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7661));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7665));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7676));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7700));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7704));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7707));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7710));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7713));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7716));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7719));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8220));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8229));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8234));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8238));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8552));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8556));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8560));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(9343));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(9349));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(9353));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8178));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8182));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8185));

            migrationBuilder.UpdateData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000005"),
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(7834));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8440));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8453));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8606));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8610));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8613));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8617));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8058));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8503));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8497));

            migrationBuilder.UpdateData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000003"),
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(8109));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 16, 3, 38, 418, DateTimeKind.Unspecified).AddTicks(9298));
        }
    }
}
