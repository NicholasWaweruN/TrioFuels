using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Refactored_Sales_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("02a1bd80-8ec3-4b86-8acf-3e4d08636a6c"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("776c3c68-3374-4ac6-97c1-cc3ab0482a44"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("cbfe2656-5e1e-4714-9301-1725f5f439e1"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("d4132851-9e14-4a26-9ede-e31204c4ed3b"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("7a8518a5-b21f-4c46-a422-fab2316d30b3"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("b8b39660-219e-4a88-8724-b4e940311cd1"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("bde2c450-bd62-431a-9a51-b918bedc61d6"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("f1c747be-1bb2-49af-9751-54b7338212ec"));

            migrationBuilder.AddColumn<decimal>(
                name: "RoyaltyPointPerLitre",
                table: "Vehicles",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "RoyaltyPoints",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Litres = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PointsCredit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PointsDebit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SaleId = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoyaltyPoints", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "38b4cc1e-ebda-43f0-a820-3b44e113bbc3", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6146), new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6145), new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6148), new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6139), "324a5974-d881-4d94-96ff-4b297fb2822f" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4758));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4767));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4772));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4776));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4780));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4784));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4787));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4791));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4795));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4799));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4802));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4806));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4810));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4814));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(4840));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6731));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6478));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6644));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6650));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5500));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5506));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5510));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5513));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5516));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5519));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5522));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5525));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5528));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5531));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5533));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5536));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5539));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6565));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("5074523e-0334-48c0-9f92-f998e4f58fdf"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5718), "" },
                    { new Guid("584fa610-ff1b-4627-ad8b-128283e02378"), "02", "Bulk App", "", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5725), "" },
                    { new Guid("79025205-fb83-4b36-bf25-18402c8963e2"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5748), "" },
                    { new Guid("867fb851-2cf4-4f3a-bde5-5a3ee90dcb91"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(5755), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6894));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6913));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6271));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(7000));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6994));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6806), new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6807) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("04d35bc8-1b85-4c4d-9031-18f9bd29c9bf"), "04", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6386), "99999" },
                    { new Guid("1eca0ef1-8608-4d42-a2d3-40021d24336d"), "01", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6393), "99999" },
                    { new Guid("909e6cc2-543c-4c73-98d2-cdd5355b76b7"), "02", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6399), "99999" },
                    { new Guid("f850d99d-20b5-41ca-8ae6-9ea240596a31"), "03", new DateTime(2026, 2, 26, 12, 2, 50, 25, DateTimeKind.Utc).AddTicks(6368), "99999" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoyaltyPoints");

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("5074523e-0334-48c0-9f92-f998e4f58fdf"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("584fa610-ff1b-4627-ad8b-128283e02378"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("79025205-fb83-4b36-bf25-18402c8963e2"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("867fb851-2cf4-4f3a-bde5-5a3ee90dcb91"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("04d35bc8-1b85-4c4d-9031-18f9bd29c9bf"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("1eca0ef1-8608-4d42-a2d3-40021d24336d"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("909e6cc2-543c-4c73-98d2-cdd5355b76b7"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("f850d99d-20b5-41ca-8ae6-9ea240596a31"));

            migrationBuilder.DropColumn(
                name: "RoyaltyPointPerLitre",
                table: "Vehicles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "1570eb76-66fc-4bea-924a-ae06bbee70c1", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5113), new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5112), new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5115), new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5107), "8344e7cf-ff9a-45f0-9b93-b47abfbbd3a0" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4120));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4126));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4130));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4133));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4136));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4140));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4143));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4146));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4149));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4152));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4155));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4158));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4162));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4165));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4182));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5459));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5304));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5404));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5409));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4692));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4696));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4699));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4701));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4704));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4706));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4709));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4711));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4717));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4719));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4722));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4724));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5353));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("02a1bd80-8ec3-4b86-8acf-3e4d08636a6c"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4853), "" },
                    { new Guid("776c3c68-3374-4ac6-97c1-cc3ab0482a44"), "02", "Bulk App", "", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4872), "" },
                    { new Guid("cbfe2656-5e1e-4714-9301-1725f5f439e1"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4877), "" },
                    { new Guid("d4132851-9e14-4a26-9ede-e31204c4ed3b"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(4882), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5573));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5589));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5176));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5653));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5647));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5516), new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5517) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("7a8518a5-b21f-4c46-a422-fab2316d30b3"), "03", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5233), "99999" },
                    { new Guid("b8b39660-219e-4a88-8724-b4e940311cd1"), "04", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5239), "99999" },
                    { new Guid("bde2c450-bd62-431a-9a51-b918bedc61d6"), "02", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5254), "99999" },
                    { new Guid("f1c747be-1bb2-49af-9751-54b7338212ec"), "01", new DateTime(2026, 2, 24, 11, 1, 35, 130, DateTimeKind.Utc).AddTicks(5244), "99999" }
                });
        }
    }
}
