using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTypeCredit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("04d3458f-0747-425c-9afa-41e3aee12be4"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("2040965d-8df3-403a-a89e-e13e6a1eda65"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("22c99500-23a2-4ec5-9682-70d97b7ffad9"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("4e0c58bf-29dc-44a9-9bcb-2d71d5d8a5b3"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("8e9691a1-57a7-446a-81d2-3c8a481a2f82"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("99e3fc3a-ed97-4eb6-bee7-163b3fdbcd6e"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("af5aee2f-4bfb-48e2-bf05-8890a3cf0d8b"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("f0b7c451-5403-435a-885e-42d5787c583d"));

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

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "DateCreated", "HasValue", "IsAppUsed", "PaymentTypeId", "PaymentTypeName", "ProcessType", "UserCode" },
                values: new object[] { 14L, new DateTime(2026, 3, 16, 16, 53, 41, 177, DateTimeKind.Utc).AddTicks(3807), true, true, 13, "Credit", "", "00001" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L);

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "288e9117-3fc9-4900-9c8b-0fd8363b1564", new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9069), new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9068), new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9071), new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9063), "43969412-9354-48b9-8029-7a0f2f541558" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8013));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8019));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8023));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8027));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8030));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8033));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8036));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8040));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8043));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8047));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8050));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8053));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8056));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8059));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8062));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9468));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9269));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9418));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9422));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8539));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8542));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8545));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8548));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8550));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8553));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8556));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8558));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8561));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8563));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8566));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8568));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8571));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9369));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9329));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("04d3458f-0747-425c-9afa-41e3aee12be4"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8830), "" },
                    { new Guid("2040965d-8df3-403a-a89e-e13e6a1eda65"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8679), "" },
                    { new Guid("22c99500-23a2-4ec5-9682-70d97b7ffad9"), "02", "Bulk App", "", new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8684), "" },
                    { new Guid("4e0c58bf-29dc-44a9-9bcb-2d71d5d8a5b3"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(8835), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9564));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9578));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9130));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9632));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9627));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9512), new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9513) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("8e9691a1-57a7-446a-81d2-3c8a481a2f82"), "03", new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9206), "99999" },
                    { new Guid("99e3fc3a-ed97-4eb6-bee7-163b3fdbcd6e"), "04", new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9212), "99999" },
                    { new Guid("af5aee2f-4bfb-48e2-bf05-8890a3cf0d8b"), "01", new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9217), "99999" },
                    { new Guid("f0b7c451-5403-435a-885e-42d5787c583d"), "02", new DateTime(2026, 3, 16, 16, 50, 11, 28, DateTimeKind.Utc).AddTicks(9222), "99999" }
                });
        }
    }
}
