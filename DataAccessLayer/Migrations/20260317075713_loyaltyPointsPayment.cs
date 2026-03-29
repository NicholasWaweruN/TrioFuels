using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class loyaltyPointsPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("6d62f0cb-b3a4-44af-ba1b-eb350b26313a"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("8f60b167-6686-45c7-a170-c02121449804"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("c0b367ed-36ee-45c9-9120-c01091e2b7b8"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("dfe7d07b-5409-4b75-a792-10abbee2d568"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("00a95a8a-09da-4f08-adc1-71549297d4c6"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("7c71ef68-d60b-4d73-9d8d-2d7326468253"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("b2647094-9a60-46ab-a199-ea5bf14a0cca"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("e7c4cc9d-3cf6-452a-b430-91f85d2aa95d"));

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

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "DateCreated", "HasValue", "IsAppUsed", "PaymentTypeId", "PaymentTypeName", "ProcessType", "UserCode" },
                values: new object[] { 15L, new DateTime(2026, 3, 17, 7, 57, 9, 870, DateTimeKind.Utc).AddTicks(3187), true, true, 14, "Loyalty Points", "", "00001" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L);

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
                values: new object[] { "d8284ed5-598a-4411-9b52-2bcbcdf38fc3", new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(4969), new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(4968), new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(4971), new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(4961), "84aea3a3-9b30-4f05-83e7-b1b1af8788ad" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3119));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3128));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3133));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3138));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3143));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3147));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3152));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3157));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3161));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3166));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3170));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3174));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3178));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3182));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3186));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5615));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5277));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5529));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5536));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3965));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3971));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3975));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3978));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3982));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3986));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3990));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3993));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(3997));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(4001));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(4004));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(4008));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(4011));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(4015));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5455));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5383));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("6d62f0cb-b3a4-44af-ba1b-eb350b26313a"), "02", "Bulk App", "", new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(4289), "" },
                    { new Guid("8f60b167-6686-45c7-a170-c02121449804"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(4484), "" },
                    { new Guid("c0b367ed-36ee-45c9-9120-c01091e2b7b8"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(4476), "" },
                    { new Guid("dfe7d07b-5409-4b75-a792-10abbee2d568"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(4281), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5777));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5803));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5079));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5883));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5877));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5696), new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5698) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("00a95a8a-09da-4f08-adc1-71549297d4c6"), "04", new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5187), "99999" },
                    { new Guid("7c71ef68-d60b-4d73-9d8d-2d7326468253"), "02", new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5211), "99999" },
                    { new Guid("b2647094-9a60-46ab-a199-ea5bf14a0cca"), "03", new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5177), "99999" },
                    { new Guid("e7c4cc9d-3cf6-452a-b430-91f85d2aa95d"), "01", new DateTime(2026, 3, 17, 7, 15, 14, 770, DateTimeKind.Utc).AddTicks(5203), "99999" }
                });
        }
    }
}
