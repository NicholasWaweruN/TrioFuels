using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class loyaltyPoints2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("18e339c3-bdfa-47a2-9428-f1e749138d6e"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("88e7ff47-0bf5-463c-905f-1389f3f952d5"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("8c959dfc-8daa-4b20-a055-cbe19b3d47c3"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("fa13ab3f-1479-48dd-a8a8-1caffc5bbb75"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("2bb091d0-fb76-4b86-9bca-f6778c5c51df"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("2cec92a8-6442-4eed-83db-9a30e7f20a8b"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("2fcd9127-7d2a-4892-98cc-df7aec4ecb2b"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("9b3328f2-d49c-4d20-bc32-29420446f80d"));

            migrationBuilder.RenameColumn(
                name: "VehicleCode",
                table: "RoyaltyPoints",
                newName: "CustomerCode");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "CustomerCode",
                table: "RoyaltyPoints",
                newName: "VehicleCode");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "af0b57a1-0cb9-436d-9a68-4fd607fa594b", new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(4725), new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(4723), new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(4727), new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(4598), "ce60adcd-323d-4b84-aa03-25a841f332bc" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3056));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3067));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3073));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3078));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3083));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3088));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3094));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3099));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3105));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3110));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3115));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3121));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3126));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3131));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3136));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(5421));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(5094));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(5328));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(5336));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3849));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3857));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3862));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3866));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3870));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3874));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3878));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3882));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3888));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3892));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3896));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3900));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3905));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(3909));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(5248));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(5184));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("18e339c3-bdfa-47a2-9428-f1e749138d6e"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(4091), "" },
                    { new Guid("88e7ff47-0bf5-463c-905f-1389f3f952d5"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(4128), "" },
                    { new Guid("8c959dfc-8daa-4b20-a055-cbe19b3d47c3"), "02", "Bulk App", "", new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(4118), "" },
                    { new Guid("fa13ab3f-1479-48dd-a8a8-1caffc5bbb75"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(4135), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(5590));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(4873));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(5675));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(5666));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(5491), new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(5492) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("2bb091d0-fb76-4b86-9bca-f6778c5c51df"), "04", new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(4989), "99999" },
                    { new Guid("2cec92a8-6442-4eed-83db-9a30e7f20a8b"), "01", new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(4999), "99999" },
                    { new Guid("2fcd9127-7d2a-4892-98cc-df7aec4ecb2b"), "03", new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(4978), "99999" },
                    { new Guid("9b3328f2-d49c-4d20-bc32-29420446f80d"), "02", new DateTime(2026, 3, 16, 21, 3, 23, 897, DateTimeKind.Utc).AddTicks(5015), "99999" }
                });
        }
    }
}
