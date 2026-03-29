using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class usersAssigments2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "39d9ac1c-e7a4-430e-a9c4-49f0e23feb64", new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4387), new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4386), new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4388), new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4381), "a93c595a-9838-4f84-a961-f6f2bcd0d584" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(3613));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(3620));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(3623));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(3626));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(3629));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(3631));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(3634));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(3636));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(3639));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(3641));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(3643));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(3646));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(3648));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(3651));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4554));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4475));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4516));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4041));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4045));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4047));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4049));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4051));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4053));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4055));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4056));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4059));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4061));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("47bf83b5-35e9-4aa0-bae4-005eb2bd2810"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4186), "" },
                    { new Guid("99e078db-f945-45a2-952a-a20bf84c8100"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4161), "" },
                    { new Guid("b7641684-8cfe-473a-bd09-6f9962c364d0"), "02", "Bulk App", "", new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4166), "" },
                    { new Guid("c9772014-1bb8-47b3-a041-37c76d53be38"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4170), "" }
                });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 7, 9, 329, DateTimeKind.Utc).AddTicks(4439));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("47bf83b5-35e9-4aa0-bae4-005eb2bd2810"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("99e078db-f945-45a2-952a-a20bf84c8100"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("b7641684-8cfe-473a-bd09-6f9962c364d0"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("c9772014-1bb8-47b3-a041-37c76d53be38"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "6ee8753f-7a9a-49bb-a333-80e2e7f9cb61", new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5980), new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(5980), null, new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(6071), "3c0ae88b-ad65-411a-a492-a41def35984b" });

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

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(6268));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(6188));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(6229));

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

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 6, 2, 43, 285, DateTimeKind.Utc).AddTicks(6135));
        }
    }
}
