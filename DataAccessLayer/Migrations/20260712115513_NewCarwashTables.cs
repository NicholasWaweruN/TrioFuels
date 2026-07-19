using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class NewCarwashTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarWashTransactionItems_CarWashProducts_ProductId",
                table: "CarWashTransactionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CarWashTransactionItems_CarWashTransactions_TransactionId",
                table: "CarWashTransactionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarWashTransactionItems",
                table: "CarWashTransactionItems");

            migrationBuilder.DropIndex(
                name: "IX_CarWashShifts_UserCode_Status",
                table: "CarWashShifts");

            migrationBuilder.RenameTable(
                name: "CarWashTransactionItems",
                newName: "carWashTransactionItems");

            migrationBuilder.RenameIndex(
                name: "IX_CarWashTransactionItems_TransactionId",
                table: "carWashTransactionItems",
                newName: "IX_carWashTransactionItems_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_CarWashTransactionItems_ProductId",
                table: "carWashTransactionItems",
                newName: "IX_carWashTransactionItems_ProductId");

            migrationBuilder.AddColumn<long>(
                name: "VehicleTypeId",
                table: "CarWashTransactions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "carWashTransactionItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldPrecision: 12,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExpectedCash",
                table: "CarWashShifts",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldPrecision: 12,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Difference",
                table: "CarWashShifts",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldPrecision: 12,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualCashCounted",
                table: "CarWashShifts",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldPrecision: 12,
                oldScale: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_carWashTransactionItems",
                table: "carWashTransactionItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarWashProductPrices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    VehicleTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarWashProductPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarWashProductPrices_CarWashProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "CarWashProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarWashProductPrices_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "29189ed4-3be0-4b57-80f5-930746afb707", new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7605), new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7604), new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7608), new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7600), "fca513f1-d238-4480-823e-c1db58cf1b09" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6123));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6130));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6134));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6140));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6144));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6148));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6152));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6180));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6184));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 763, DateTimeKind.Unspecified).AddTicks(108));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7892));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7735));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7856));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7861));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6590));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6594));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6597));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6601));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6604));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6607));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6634));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6638));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6642));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6645));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6648));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6650));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6653));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7810));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7816));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7820));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7823));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8108));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8112));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8115));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 763, DateTimeKind.Unspecified).AddTicks(211));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 763, DateTimeKind.Unspecified).AddTicks(216));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 763, DateTimeKind.Unspecified).AddTicks(220));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7771));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7775));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7778));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7989));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8010));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8157));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8163));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8166));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8168));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7656));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8056));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8051));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 763, DateTimeKind.Unspecified).AddTicks(158));

            migrationBuilder.CreateIndex(
                name: "IX_CarWashTransactions_VehicleTypeId",
                table: "CarWashTransactions",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CarWashProductPrices_ProductId",
                table: "CarWashProductPrices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CarWashProductPrices_VehicleTypeId",
                table: "CarWashProductPrices",
                column: "VehicleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_carWashTransactionItems_CarWashProducts_ProductId",
                table: "carWashTransactionItems",
                column: "ProductId",
                principalTable: "CarWashProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_carWashTransactionItems_CarWashTransactions_TransactionId",
                table: "carWashTransactionItems",
                column: "TransactionId",
                principalTable: "CarWashTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarWashTransactions_VehicleTypes_VehicleTypeId",
                table: "CarWashTransactions",
                column: "VehicleTypeId",
                principalTable: "VehicleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carWashTransactionItems_CarWashProducts_ProductId",
                table: "carWashTransactionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_carWashTransactionItems_CarWashTransactions_TransactionId",
                table: "carWashTransactionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CarWashTransactions_VehicleTypes_VehicleTypeId",
                table: "CarWashTransactions");

            migrationBuilder.DropTable(
                name: "CarWashProductPrices");

            migrationBuilder.DropTable(
                name: "VehicleTypes");

            migrationBuilder.DropIndex(
                name: "IX_CarWashTransactions_VehicleTypeId",
                table: "CarWashTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_carWashTransactionItems",
                table: "carWashTransactionItems");

            migrationBuilder.DropColumn(
                name: "VehicleTypeId",
                table: "CarWashTransactions");

            migrationBuilder.RenameTable(
                name: "carWashTransactionItems",
                newName: "CarWashTransactionItems");

            migrationBuilder.RenameIndex(
                name: "IX_carWashTransactionItems_TransactionId",
                table: "CarWashTransactionItems",
                newName: "IX_CarWashTransactionItems_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_carWashTransactionItems_ProductId",
                table: "CarWashTransactionItems",
                newName: "IX_CarWashTransactionItems_ProductId");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "CarWashTransactionItems",
                type: "numeric(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExpectedCash",
                table: "CarWashShifts",
                type: "numeric(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Difference",
                table: "CarWashShifts",
                type: "numeric(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualCashCounted",
                table: "CarWashShifts",
                type: "numeric(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarWashTransactionItems",
                table: "CarWashTransactionItems",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_CarWashShifts_UserCode_Status",
                table: "CarWashShifts",
                columns: new[] { "UserCode", "Status" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarWashTransactionItems_CarWashProducts_ProductId",
                table: "CarWashTransactionItems",
                column: "ProductId",
                principalTable: "CarWashProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarWashTransactionItems_CarWashTransactions_TransactionId",
                table: "CarWashTransactionItems",
                column: "TransactionId",
                principalTable: "CarWashTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
