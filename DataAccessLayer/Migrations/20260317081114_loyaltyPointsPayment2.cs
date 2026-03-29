using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class loyaltyPointsPayment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("75d1d029-3f92-4f52-8e08-fdc5e83550a8"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("7900bdae-e7e1-47f8-a648-f6bf9403e2e4"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("a9e9f870-5f25-44d1-8409-6b515edb5513"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("e62183de-86b1-4b9e-922c-5da10364bb7f"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("0274f4a2-95b7-422b-b067-2aaa58804619"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("0f4a8d94-6c3d-4558-a6a0-4fef82322d97"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("a5d16c64-6079-4143-979e-51d38e7ca5b5"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("beb6a172-0267-47e8-98a9-3a07672ce429"));

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
                columns: new[] { "DateCreated", "PaymentTypeName" },
                values: new object[] { new DateTime(2026, 3, 17, 8, 11, 10, 658, DateTimeKind.Utc).AddTicks(3279), "Loyalty" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "df5a2e9d-5f0e-4c67-ac73-93a92e21bf07", new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3731), new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3730), new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3733), new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3721), "4ebccd50-cf32-4b09-9e46-5907341b1496" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(2638));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(2649));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(2653));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(2657));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(2667));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(2670));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(2674));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(2677));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(2680));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(2683));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(2686));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(2690));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(2693));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(2696));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(2699));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(4227));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3922));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(4172));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(4179));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3145));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3150));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3153));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3156));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3162));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3165));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3167));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3170));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3172));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3175));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3177));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3180));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3182));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3184));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "DateCreated", "PaymentTypeName" },
                values: new object[] { new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3187), "Loyalty Points" });

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(4026));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3978));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("75d1d029-3f92-4f52-8e08-fdc5e83550a8"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3313), "" },
                    { new Guid("7900bdae-e7e1-47f8-a648-f6bf9403e2e4"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3347), "" },
                    { new Guid("a9e9f870-5f25-44d1-8409-6b515edb5513"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3343), "" },
                    { new Guid("e62183de-86b1-4b9e-922c-5da10364bb7f"), "02", "Bulk App", "", new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3318), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(4334));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(4350));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3803));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(4397));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(4392));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(4272), new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(4273) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("0274f4a2-95b7-422b-b067-2aaa58804619"), "03", new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3865), "99999" },
                    { new Guid("0f4a8d94-6c3d-4558-a6a0-4fef82322d97"), "04", new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3870), "99999" },
                    { new Guid("a5d16c64-6079-4143-979e-51d38e7ca5b5"), "02", new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3880), "99999" },
                    { new Guid("beb6a172-0267-47e8-98a9-3a07672ce429"), "01", new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3875), "99999" }
                });
        }
    }
}
