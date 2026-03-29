using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class PriceDispenser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "DispenserCode",
                table: "Prices",
                type: "character varying(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "abf827b5-ca7e-4bf1-af20-eb17a6003651", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8749), new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8746), new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8751), new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8740), "af7dda7b-4273-4f3e-bfa0-92cdf7e77170" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7601));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7609));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7615));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7619));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7624));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7628));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7632));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7637));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7641));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7646));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7650));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7654));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7659));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7663));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(7681));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9269));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8996));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9198));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9205));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8165));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8171));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8175));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8178));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8181));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8185));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8189));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8192));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8196));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8199));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8203));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8206));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8210));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8213));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8217));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9138));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9667));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9671));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9675));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9077));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("147a3041-9102-49c8-8744-6c6c791f54cd"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8469), "" },
                    { new Guid("a3457135-b9fb-4a1f-bd19-910b4d9344d6"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8445), "" },
                    { new Guid("b43c62d1-5318-41cf-ae17-800a2ad6a4e3"), "02", "Bulk App", "", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8456), "" },
                    { new Guid("b7f2197a-06f9-4138-add1-218eb4c5670a"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8463), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9509));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9532));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8831));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9604));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9597));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9333), new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(9334) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("831eec75-9f4b-457c-a99e-7a8871c7f0c3"), "01", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8930), "99999" },
                    { new Guid("cbb126d1-5819-4132-918a-8b2588d0a9b5"), "03", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8908), "99999" },
                    { new Guid("eb008a96-56fa-4754-8c1d-6dbd2e38804f"), "02", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8936), "99999" },
                    { new Guid("f6b603bb-0aeb-4536-bb7e-17b9c4a6878e"), "04", new DateTime(2026, 3, 18, 16, 53, 1, 332, DateTimeKind.Utc).AddTicks(8918), "99999" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("147a3041-9102-49c8-8744-6c6c791f54cd"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("a3457135-b9fb-4a1f-bd19-910b4d9344d6"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("b43c62d1-5318-41cf-ae17-800a2ad6a4e3"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("b7f2197a-06f9-4138-add1-218eb4c5670a"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("831eec75-9f4b-457c-a99e-7a8871c7f0c3"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("cbb126d1-5819-4132-918a-8b2588d0a9b5"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("eb008a96-56fa-4754-8c1d-6dbd2e38804f"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("f6b603bb-0aeb-4536-bb7e-17b9c4a6878e"));

            migrationBuilder.DropColumn(
                name: "DispenserCode",
                table: "Prices");

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
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2443));

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

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2872));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2875));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 10, 54, 9, 353, DateTimeKind.Utc).AddTicks(2878));

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
    }
}
