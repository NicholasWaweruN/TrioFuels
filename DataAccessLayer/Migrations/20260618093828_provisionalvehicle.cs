using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class provisionalvehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProvisionalCustomers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    NumberPlate = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvisionalCustomers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "9e807090-7b6b-400c-90a4-b0e96ea70237", new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8852), new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8851), new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8853), new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8847), "f4eea760-1854-4641-b6ac-56054410f93e" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7861));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7869));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7874));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7878));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7881));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7886));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7890));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7894));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(7898));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 601, DateTimeKind.Utc).AddTicks(748));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9325));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9070));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9262));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9266));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8393));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8399));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8403));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8407));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8434));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8438));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8442));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8445));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8449));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8452));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8456));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8459));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9202));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9659));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9664));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9668));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 601, DateTimeKind.Utc).AddTicks(914));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 601, DateTimeKind.Utc).AddTicks(920));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 601, DateTimeKind.Utc).AddTicks(924));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9136));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9144));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9496));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9509));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9736));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9740));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9743));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9746));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(8933));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9582));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 600, DateTimeKind.Utc).AddTicks(9577));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 18, 9, 38, 25, 601, DateTimeKind.Utc).AddTicks(835));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProvisionalCustomers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "1548cc7a-f22a-4d39-823f-42fc6f24df9e", new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3907), new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3906), new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3908), new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3903), "b9dec3fc-2727-44f5-8b0f-42462b811b25" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3077));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3083));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3087));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3090));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3093));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3096));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3100));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3103));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3106));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(5005));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4158));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4020));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4121));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3517));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3520));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3522));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3525));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3526));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3528));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3530));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3532));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3534));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3535));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3537));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3540));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3542));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4089));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4339));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4342));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4344));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(5086));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(5090));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(5092));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4056));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4059));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4061));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4248));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4258));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4379));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4381));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4383));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4385));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(3953));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4293));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(4290));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 17, 11, 53, 36, 593, DateTimeKind.Utc).AddTicks(5038));
        }
    }
}
