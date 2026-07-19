using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class CarwashTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarWashProducts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarWashProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarWashShifts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpectedCash = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    ActualCashCounted = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    Difference = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    VarianceReason = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarWashShifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarWashTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShiftId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiptNumber = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    AmountReceived = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    Change = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", unicode: false, maxLength: 15, nullable: true),
                    MpesaReference = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true),
                    IsReversed = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarWashTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarWashTransactions_CarWashShifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "CarWashShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarWashTransactionItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarWashTransactionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarWashTransactionItems_CarWashProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "CarWashProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarWashTransactionItems_CarWashTransactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "CarWashTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CarWashShifts_UserCode_Status",
                table: "CarWashShifts",
                columns: new[] { "UserCode", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_CarWashTransactionItems_ProductId",
                table: "CarWashTransactionItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CarWashTransactionItems_TransactionId",
                table: "CarWashTransactionItems",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_CarWashTransactions_ReceiptNumber",
                table: "CarWashTransactions",
                column: "ReceiptNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarWashTransactions_ShiftId",
                table: "CarWashTransactions",
                column: "ShiftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarWashTransactionItems");

            migrationBuilder.DropTable(
                name: "CarWashProducts");

            migrationBuilder.DropTable(
                name: "CarWashTransactions");

            migrationBuilder.DropTable(
                name: "CarWashShifts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "dd7a50cd-6952-4fb5-8f5b-3d39dd002354", new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6798), new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6797), new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6800), new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6793), "eca8c5ec-707f-4fa1-bf4b-a898d648a7c8" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5854));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5862));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5866));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5870));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5873));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5877));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5881));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5885));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(5888));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(8205));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7127));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6949));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7084));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7090));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6465));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6470));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6474));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6477));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6481));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6484));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6487));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6491));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6494));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6497));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6501));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6504));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6507));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7030));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7037));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7043));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7047));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7348));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7355));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7358));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(8303));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(8308));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(8312));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6988));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6992));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6995));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7232));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7243));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7389));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7394));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7397));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7400));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(6848));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7292));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(7283));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 2, 9, 18, 8, 512, DateTimeKind.Unspecified).AddTicks(8246));
        }
    }
}
