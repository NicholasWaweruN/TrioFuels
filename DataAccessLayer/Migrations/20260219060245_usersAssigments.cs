using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class usersAssigments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("069cfcdb-e9bd-4268-a01e-b67b4af53118"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("06e8224f-3b6a-4c41-a61b-102323578f55"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("11b481d3-c5a9-4fd8-8bfa-ed4b358a024e"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("9acc8996-d063-42cb-ae5d-61b0cf1ff698"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "6ee8753f-7a9a-49bb-a333-80e2e7f9cb61", new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5980), new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5980), new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(6071), "3c0ae88b-ad65-411a-a492-a41def35984b" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5260));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5264));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5268));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5271));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5273));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5276));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5279));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5282));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5284));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5287));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5289));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5292));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5295));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5297));

            migrationBuilder.InsertData(
                table: "DispenserAssignments",
                columns: new[] { "Id", "AssignedBy", "AttedantUserCode", "DateAssigned", "DispenserCode", "IsActive", "StationCode" },
                values: new object[] { 1L, "99999", "99999", new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(6268), "D01", true, "S001" });

            migrationBuilder.InsertData(
                table: "Dispensers",
                columns: new[] { "Id", "DateCreated", "DispenserCode", "DispenserName", "IsActive", "StationCode", "StorageLocation", "TillNumber", "UserCode" },
                values: new object[] { 1L, new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(6188), "D01", "D1", true, "S001", "kenya", "078678", "00001" });

            migrationBuilder.InsertData(
                table: "Nozzles",
                columns: new[] { "Id", "DateCreated", "DispenserCode", "IsActive", "NozzleCode", "NozzleName", "UserCode" },
                values: new object[] { 1L, new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(6229), "D01", true, "S001", "N01", "00001" });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5726));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5730));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5732));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5734));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5736));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5738));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5740));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5742));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5744));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5746));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("0c8f7ead-a5ee-45cf-82b5-0243ed84e4e0"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5860), "" },
                    { new Guid("1a6ca20e-15d8-465f-a9e1-21d5e69a0d3b"), "02", "Bulk App", "", new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5856), "" },
                    { new Guid("3b6b6825-f37e-42d6-b47a-6c0533d8f7f7"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5850), "" },
                    { new Guid("4f4b5513-ce6e-4135-8229-b3b019757f95"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5877), "" }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "DateCreated", "IsActive", "LocationId", "StationAddress", "StationCode", "StationName", "UserCode" },
                values: new object[] { 1L, new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(6135), true, "Test Station", "Test Station", "S001", "TEST STATION", "00001" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("0c8f7ead-a5ee-45cf-82b5-0243ed84e4e0"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("1a6ca20e-15d8-465f-a9e1-21d5e69a0d3b"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("3b6b6825-f37e-42d6-b47a-6c0533d8f7f7"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("4f4b5513-ce6e-4135-8229-b3b019757f95"));

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "cad204d9-04c6-4191-921b-ad0dbb23a0cc", new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(6374), new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(6374), null, "36614fcd-5b72-4799-a240-838a29bb822c" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(4936));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(4947));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(4954));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(4961));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(4964));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(4971));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(4977));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(4984));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(4988));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(4993));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(4998));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(5002));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(5011));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(5014));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(5842));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(5847));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(5851));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(5854));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(5857));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(5860));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(5863));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(5866));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(5869));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(5872));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("069cfcdb-e9bd-4268-a01e-b67b4af53118"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(6114), "" },
                    { new Guid("06e8224f-3b6a-4c41-a61b-102323578f55"), "02", "Bulk App", "", new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(6091), "" },
                    { new Guid("11b481d3-c5a9-4fd8-8bfa-ed4b358a024e"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(6076), "" },
                    { new Guid("9acc8996-d063-42cb-ae5d-61b0cf1ff698"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 16, 6, 37, 51, 779, DateTimeKind.Utc).AddTicks(6105), "" }
                });
        }
    }
}
