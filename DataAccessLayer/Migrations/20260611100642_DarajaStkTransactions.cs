using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class DarajaStkTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StkTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CheckoutRequestId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MerchantRequestId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    TillNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    AccountReference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    MpesaReceiptNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ResultCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ResultDescription = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateCompleted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StkTransactions", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "8508fc3f-5c58-494c-9c92-24be6fca6192", new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4760), new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4759), new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4761), new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4756), "f6f5ac75-4bda-4885-acae-453cc52f9e3d" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(3990));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(3997));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4003));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4005));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4008));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4011));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4014));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4016));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4019));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4022));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4024));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4027));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4030));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4043));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(6377));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5097));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4919));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5048));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5052));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4403));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4407));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4410));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4412));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4414));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4416));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4419));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4421));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4423));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4425));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4428));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4430));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5003));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5494));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5498));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5501));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(6489));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(6494));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(6498));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4961));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4965));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4968));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5206));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5217));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5557));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5562));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5564));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5566));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4819));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5434));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5427));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(6424));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StkTransactions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "ccf9a3cf-6c05-453f-bf89-e582d0692d61", new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3873), new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3872), new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3874), new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3868), "d73179ea-f1a9-47f4-aa24-7b897f88aa11" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3033));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3039));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3043));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3047));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3050));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3054));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3057));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3061));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3064));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3067));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3071));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3074));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3078));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3081));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3094));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(5627));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4286));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4067));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4229));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4234));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3475));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3478));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3481));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3484));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3487));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3489));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3492));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3495));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3497));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3500));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3502));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3505));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4175));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4543));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4547));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4550));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(5744));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(5749));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(5753));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4122));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4126));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4130));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4406));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4419));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4609));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4614));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4616));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4619));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(3942));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4479));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(4473));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 10, 7, 14, 19, 969, DateTimeKind.Utc).AddTicks(5685));
        }
    }
}
