using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Pdadevices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("1b67fc52-a9e9-4503-9154-71e053a16c61"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("a319d237-09c5-45d1-9199-3fe2537245eb"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("c8e0c6b1-2ff6-4530-926a-e033fbca15c9"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("e3e0c55b-0c7b-4158-8051-94f3a578a7fb"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "b6127e8b-ad1d-48c0-ad53-8f8d1984c50e", new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5483), new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5483), new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5485), new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5478), "d3f3cf96-5b1a-4378-97c0-be8bf5a2da57" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(4319));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(4326));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(4328));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(4331));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(4334));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(4336));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(4339));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(4341));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(4344));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(4346));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(4349));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(4351));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(4354));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(4356));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5705));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5576));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5662));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5079));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5084));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5087));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5089));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5091));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5094));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5098));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5100));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5102));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5104));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5106));

            migrationBuilder.InsertData(
                table: "PdaDevices",
                columns: new[] { "Id", "DateCreated", "DeviceCode", "DeviceIMEI", "DeviceMacAddress", "DeviceModel", "DeviceName", "DeviceSerialNumber", "DispenserCode", "IsActive", "UserCode" },
                values: new object[] { 1L, new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5619), "1234567890", "1234567890", "1234567890", "1234567890", "Test PDA", "1234567890", "D01", true, "00001" });

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("0b8fb7b6-b235-43c8-86d5-971049e6c478"), "02", "Bulk App", "", new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5280), "" },
                    { new Guid("57b92a10-d1ed-4e9b-914f-cac27bf3bf0f"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5266), "" },
                    { new Guid("86be07ec-32e7-4253-818f-a4fdcdb829c2"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5288), "" },
                    { new Guid("fdba3723-edf6-46fe-8a48-5d503da1d922"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5284), "" }
                });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5539));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5743), new DateTime(2026, 2, 19, 15, 36, 53, 737, DateTimeKind.Utc).AddTicks(5743) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("0b8fb7b6-b235-43c8-86d5-971049e6c478"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("57b92a10-d1ed-4e9b-914f-cac27bf3bf0f"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("86be07ec-32e7-4253-818f-a4fdcdb829c2"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("fdba3723-edf6-46fe-8a48-5d503da1d922"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "e27779cf-5bb4-4abc-8d95-533089b000a2", new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5922), new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5921), new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5925), new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5914), "d233a9bc-3f50-4906-91c2-d6c5ececee02" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(4116));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(4128));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(4132));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(4138));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(4142));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(4145));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(4149));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(4152));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(4156));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(4160));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(4163));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(4167));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(4170));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(6388));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(6237));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(6311));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5066));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5072));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5074));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5077));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5080));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5082));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5085));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5087));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5090));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5092));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5095));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("1b67fc52-a9e9-4503-9154-71e053a16c61"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5288), "" },
                    { new Guid("a319d237-09c5-45d1-9199-3fe2537245eb"), "02", "Bulk App", "", new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5296), "" },
                    { new Guid("c8e0c6b1-2ff6-4530-926a-e033fbca15c9"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5307), "" },
                    { new Guid("e3e0c55b-0c7b-4158-8051-94f3a578a7fb"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5302), "" }
                });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(6142));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(6467), new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(6468) });
        }
    }
}
