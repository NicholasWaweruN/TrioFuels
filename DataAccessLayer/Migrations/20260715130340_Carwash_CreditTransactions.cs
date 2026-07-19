using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Carwash_CreditTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarwashCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    IsCreditCustomer = table.Column<bool>(type: "boolean", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsDiscountCustomer = table.Column<bool>(type: "boolean", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarwashCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarwashCreditTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CarwashCustomerId = table.Column<int>(type: "integer", nullable: false),
                    Debit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Credit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    RunningBalance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    SaleId = table.Column<int>(type: "integer", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarwashCreditTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarwashCreditTransactions_CarwashCustomers_CarwashCustomerId",
                        column: x => x.CarwashCustomerId,
                        principalTable: "CarwashCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CarwashCreditTransactions_CarwashCustomerId",
                table: "CarwashCreditTransactions",
                column: "CarwashCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CarwashCustomers_PhoneNumber",
                table: "CarwashCustomers",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarwashCreditTransactions");

            migrationBuilder.DropTable(
                name: "CarwashCustomers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "ae19e2c9-7ccc-4f6b-b3df-33fca6b64c00", new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4976), new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4974), new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4979), new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4969), "3e064430-a183-4acb-9036-d3437810e983" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4110));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4118));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4123));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4127));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4132));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4136));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4140));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4163));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4167));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(6381));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5350));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5127));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5284));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5291));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4566));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4571));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4575));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4579));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4583));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4602));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4636));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4640));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4644));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4648));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4651));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4655));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4658));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5223));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5230));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5235));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5240));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5580));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5585));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5589));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(6493));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(6499));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(6503));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5173));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5178));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5182));

            migrationBuilder.UpdateData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000005"),
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(4794));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5460));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5473));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5641));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5646));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5651));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5655));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5031));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5524));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5516));

            migrationBuilder.UpdateData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000003"),
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(5085));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 15, 12, 5, 22, 391, DateTimeKind.Unspecified).AddTicks(6436));
        }
    }
}
