using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class SupervisorReconciliation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShiftSupervisorReconciliations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShiftNumber = table.Column<string>(type: "text", nullable: false),
                    MpesaReceived = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CashReceived = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreditReceived = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    LoyaltyPointsUsed = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PdqReceived = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SystemMpesaTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SystemCashTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SystemCreditTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SystemLoyaltyTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SystemPdqTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftSupervisorReconciliations", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "df7ff5f9-20b9-4867-b4f2-8c2f2daa3cf0", new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1659), new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1657), new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1660), new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1654), "d1dde17a-8ad3-4b4f-aab6-e8593c91f5f4" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1095));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1101));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1105));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1109));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1112));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1115));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1118));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1123));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1127));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2844));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1949));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1780));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1900));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1905));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1377));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1381));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1384));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1387));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1390));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1393));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1395));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1398));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1401));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1404));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1406));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1409));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1412));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1853));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1859));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1864));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1867));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2139));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2143));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2146));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2924));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2930));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2934));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1815));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1819));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1822));

            migrationBuilder.UpdateData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000005"),
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1515));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2036));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2048));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2178));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2181));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2183));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2186));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1703));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2091));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2086));

            migrationBuilder.UpdateData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000003"),
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1751));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2883));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftSupervisorReconciliations");

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
    }
}
