using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTypesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("46dd0571-1516-44a1-b96d-04b35bb38fc1"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("c56bb7a4-d900-4a4d-9071-1c918f302d35"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("e833f9e4-202c-4ac5-a11f-f792209b1df3"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("fd8a238b-365d-471c-aed9-fa5763cbdf7c"));

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
                columns: new[] { "DateCreated", "IsAppUsed" },
                values: new object[] { new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5066), true });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateCreated", "IsAppUsed" },
                values: new object[] { new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5072), true });

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

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "DateCreated", "HasValue", "IsAppUsed", "PaymentTypeId", "PaymentTypeName", "ProcessType", "UserCode" },
                values: new object[] { 11L, new DateTime(2026, 2, 19, 15, 16, 16, 634, DateTimeKind.Utc).AddTicks(5095), true, false, 10, "Hand_Cash", "", "00001" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L);

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
                values: new object[] { "924e0fc3-5066-4a76-9b06-d14bc7505df2", new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6426), new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6425), new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6518), new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6420), "23147e26-e61a-4705-8ec3-1f797ec27300" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5421));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5426));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5429));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5432));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5435));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5442));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5445));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5448));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5451));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5454));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5457));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5460));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5463));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6845));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6644));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6791));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "IsAppUsed" },
                values: new object[] { new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5995), false });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateCreated", "IsAppUsed" },
                values: new object[] { new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5999), false });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6001));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6003));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6006));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6008));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6010));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6013));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6015));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6017));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("46dd0571-1516-44a1-b96d-04b35bb38fc1"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6154), "" },
                    { new Guid("c56bb7a4-d900-4a4d-9071-1c918f302d35"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6208), "" },
                    { new Guid("e833f9e4-202c-4ac5-a11f-f792209b1df3"), "02", "Bulk App", "", new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6149), "" },
                    { new Guid("fd8a238b-365d-471c-aed9-fa5763cbdf7c"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6143), "" }
                });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6596));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6888), new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(6889) });
        }
    }
}
