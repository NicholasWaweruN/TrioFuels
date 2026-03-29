using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class loyaltyPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("08b0a60e-c608-4e7c-b408-d9ab7cc43f97"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("70072b55-22b3-45b4-be71-e0ce47b38a4f"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("806dad68-dee7-488a-b859-80baa2c6440f"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("87d748a0-2966-4b3c-984e-bacd14102db4"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("35828028-5a57-4c46-8cfc-f11d27eea838"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("77684d9c-1473-4d6d-8eb5-2573c26b32aa"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("89d74a54-0bbf-4efd-902b-756e2ab077cb"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("dc9205bf-5776-4cd2-8d8b-5178370f22ef"));

            migrationBuilder.AddColumn<decimal>(
                name: "BaseLoyaltyPoints",
                table: "Customers",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "BaseLoyaltyPoints",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "80aceec0-5580-4723-a123-ec0f8b0e547f", new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(4282), new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(4281), new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(4284), new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(4275), "23ed877a-0fba-45fc-a33b-8650f702f490" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3052));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3060));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3064));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3068));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3073));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3077));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3081));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3086));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3090));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3094));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3097));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3101));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3104));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3108));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3112));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(5028));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(4721));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(4952));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(4959));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3766));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3774));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3777));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3783));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3786));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3789));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3792));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3795));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3798));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3801));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3804));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3807));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(4880));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(4815));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("08b0a60e-c608-4e7c-b408-d9ab7cc43f97"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3999), "" },
                    { new Guid("70072b55-22b3-45b4-be71-e0ce47b38a4f"), "02", "Bulk App", "", new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3969), "" },
                    { new Guid("806dad68-dee7-488a-b859-80baa2c6440f"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3961), "" },
                    { new Guid("87d748a0-2966-4b3c-984e-bacd14102db4"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3977), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(5177));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(5195));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(5276));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(5268));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(5098), new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(5100) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("35828028-5a57-4c46-8cfc-f11d27eea838"), "02", new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(4650), "99999" },
                    { new Guid("77684d9c-1473-4d6d-8eb5-2573c26b32aa"), "03", new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(4619), "99999" },
                    { new Guid("89d74a54-0bbf-4efd-902b-756e2ab077cb"), "04", new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(4637), "99999" },
                    { new Guid("dc9205bf-5776-4cd2-8d8b-5178370f22ef"), "01", new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(4644), "99999" }
                });
        }
    }
}
