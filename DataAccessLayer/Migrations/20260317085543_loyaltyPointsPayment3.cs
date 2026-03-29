using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class loyaltyPointsPayment3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("05486a6b-2b6c-45e0-884d-f1097c78f0b4"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("093b1c83-8e3f-46f3-b7d3-0d6f4baa55eb"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("a7d46ec7-c815-4c2f-86a3-63d83350c94e"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("f0575a79-5d85-44a2-8fb9-3950a11b14ae"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("27ca1d3f-0c02-406f-a2f2-adbce003f5bb"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("7a3d5219-5382-4ff0-9bdd-50b4abfc778e"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("a3f0f683-a050-431e-959c-f3a89a35ae0e"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("c3430c1e-3183-494c-a7d8-4627dafba600"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "ea989fea-a592-4653-8cc7-51e693028d6e", new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4064), new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4063), new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4066), new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4057), "69525d81-f98b-4d34-9881-acf832b92ec4" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(2354));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(2363));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(2368));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(2376));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(2380));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(2384));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(2387));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(2391));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(2395));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(2399));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(2403));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(2407));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(2411));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(2415));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4789));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4435));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4702));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4708));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3209));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3215));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3219));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3222));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3225));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3228));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3231));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3235));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3238));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3241));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3265));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3269));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3273));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3276));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3279));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4614));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4537));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("05486a6b-2b6c-45e0-884d-f1097c78f0b4"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3556), "" },
                    { new Guid("093b1c83-8e3f-46f3-b7d3-0d6f4baa55eb"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3550), "" },
                    { new Guid("a7d46ec7-c815-4c2f-86a3-63d83350c94e"), "02", "Bulk App", "", new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3544), "" },
                    { new Guid("f0575a79-5d85-44a2-8fb9-3950a11b14ae"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3536), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4963));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4981));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4228));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(5068));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(5061));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4872), new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4874) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("27ca1d3f-0c02-406f-a2f2-adbce003f5bb"), "02", new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4365), "99999" },
                    { new Guid("7a3d5219-5382-4ff0-9bdd-50b4abfc778e"), "03", new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4335), "99999" },
                    { new Guid("a3f0f683-a050-431e-959c-f3a89a35ae0e"), "01", new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4358), "99999" },
                    { new Guid("c3430c1e-3183-494c-a7d8-4627dafba600"), "04", new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(4343), "99999" }
                });
        }
    }
}
