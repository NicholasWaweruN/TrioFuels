using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class loyaltyPointsPayment4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("10c5ebcd-51bb-4dbd-af74-5e4e2e7269b7"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("19cface2-03cd-4aee-bb4c-cf1de43d016d"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("b6ca3380-0792-43de-a67c-b68ef7361304"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("fc1e8941-be5f-4a7e-80e8-6e49eed55006"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("86e67802-4d26-4c6c-bf85-d488bdfe6c07"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("b7fb8162-25c1-4787-a8cf-0d1dd74bc4db"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("d24efc8b-0c70-4d74-9594-c72300f2e21d"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("ebc0957b-3fea-4e42-a204-658db616734e"));

            migrationBuilder.AddColumn<string>(
                name: "PetroleumCode",
                table: "Dispensers",
                type: "character varying(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PetroleumProducts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PetroleumCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    PetroleumName = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetroleumProducts", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "622869dd-2d59-48f7-b3db-00b0d94fb190", new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2249), new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2248), new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2251), new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2244), "3118c4db-6430-4c6d-913b-f3bd59b7a16b" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1210));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1216));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1220));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1222));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1225));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1229));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1288));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1292));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1296));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1299));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1302));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1306));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1310));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1313));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1325));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2651));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "PetroleumCode" },
                values: new object[] { new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2443), "01" });

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2595));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2598));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1834));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1839));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1842));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1845));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1847));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1850));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1853));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1855));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1858));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1860));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1862));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1865));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1867));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1870));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(1872));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2546));

            migrationBuilder.InsertData(
                table: "PetroleumProducts",
                columns: new[] { "Id", "DateCreated", "PetroleumCode", "PetroleumName", "UserCode" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2872), "01", "LPG", "99999" },
                    { 2L, new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2875), "02", "Petrol", "99999" },
                    { 3L, new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2878), "03", "Diesel", "99999" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2503));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("4288b02f-3f5c-4196-b320-42d9d405cb43"), "02", "Bulk App", "", new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2023), "" },
                    { new Guid("5cd10186-97bc-40c2-a8d6-2c844de29678"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2027), "" },
                    { new Guid("81dea499-b06a-4fd2-ba5a-89324198c3fb"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2002), "" },
                    { new Guid("9188c8d8-6564-4777-a284-7ae2c4e2ea46"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2032), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2752));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2770));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2322));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2825));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2818));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2700), new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2701) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("31f99a81-aa88-4839-ab1c-41854d92b9f1"), "01", new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2389), "99999" },
                    { new Guid("a6184786-ae02-4327-90cd-9a6ba3c388ac"), "03", new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2379), "99999" },
                    { new Guid("e27767f5-a06f-4bf0-b271-f38ad96ea473"), "02", new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2393), "99999" },
                    { new Guid("f48ff26c-3e8d-4ba7-91a9-f1319c744e39"), "04", new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2385), "99999" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetroleumProducts");

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("4288b02f-3f5c-4196-b320-42d9d405cb43"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("5cd10186-97bc-40c2-a8d6-2c844de29678"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("81dea499-b06a-4fd2-ba5a-89324198c3fb"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("9188c8d8-6564-4777-a284-7ae2c4e2ea46"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("31f99a81-aa88-4839-ab1c-41854d92b9f1"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("a6184786-ae02-4327-90cd-9a6ba3c388ac"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("e27767f5-a06f-4bf0-b271-f38ad96ea473"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("f48ff26c-3e8d-4ba7-91a9-f1319c744e39"));

            migrationBuilder.DropColumn(
                name: "PetroleumCode",
                table: "Dispensers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "ba9f1b60-6628-4cc7-ad89-d3b602ba50fc", new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3000), new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2998), new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3002), new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2988), "964defe3-8b2b-4d2c-9bd0-4ecf79bedd48" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(1565));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(1577));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(1582));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(1586));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(1591));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(1604));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(1608));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(1612));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(1617));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(1624));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(1628));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(1632));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(1636));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3797));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3461));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3720));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3726));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2410));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2414));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2418));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2421));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2430));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2434));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2437));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2441));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2444));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2447));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2451));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2454));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2457));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2461));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2464));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3636));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3564));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("10c5ebcd-51bb-4dbd-af74-5e4e2e7269b7"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2644), "" },
                    { new Guid("19cface2-03cd-4aee-bb4c-cf1de43d016d"), "02", "Bulk App", "", new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2653), "" },
                    { new Guid("b6ca3380-0792-43de-a67c-b68ef7361304"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2680), "" },
                    { new Guid("fc1e8941-be5f-4a7e-80e8-6e49eed55006"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(2660), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3943));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3965));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3126));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(4049));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(4042));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3862), new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3863) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("86e67802-4d26-4c6c-bf85-d488bdfe6c07"), "02", new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3386), "99999" },
                    { new Guid("b7fb8162-25c1-4787-a8cf-0d1dd74bc4db"), "01", new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3379), "99999" },
                    { new Guid("d24efc8b-0c70-4d74-9594-c72300f2e21d"), "04", new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3361), "99999" },
                    { new Guid("ebc0957b-3fea-4e42-a204-658db616734e"), "03", new DateTime(2026, 3, 17, 8, 55, 40, 401, DateTimeKind.Utc).AddTicks(3354), "99999" }
                });
        }
    }
}
