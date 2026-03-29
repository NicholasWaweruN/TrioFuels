using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class usersAssigments23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "354ffa9b-fc55-462e-9880-9d47bfbb7ab3", new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(8068), new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(8067), new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(8071), new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(8063), "a2dc64a9-46c0-4987-871a-b3a6992d3ac5" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7229));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7235));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7239));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7242));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7245));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7248));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7252));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7256));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7261));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7264));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7268));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7272));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7275));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7279));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(8373));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(8177));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(8323));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7679));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7685));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7688));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7691));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7695));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7699));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7701));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7704));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7706));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7708));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("64294d2d-f916-4b46-9063-0eebdc18796c"), "02", "Bulk App", "", new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7827), "" },
                    { new Guid("b90d4bfd-356a-48c5-a3f1-3680f878e0fa"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7849), "" },
                    { new Guid("b9ea3eff-44c0-4b35-91d0-0106bdacdcea"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7844), "" },
                    { new Guid("bbc6f0c1-e68f-45e1-9c08-fdbe446a55a4"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(7820), "" }
                });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(8136));

            migrationBuilder.InsertData(
                table: "Tills",
                columns: new[] { "Id", "DateCreated", "IsActive", "LastFetch", "OffsetValue", "StoreNumber", "TillName", "TillNumber", "UserCode" },
                values: new object[] { 1L, new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(8417), true, new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(8418), 0, "078678", "Test Till", "078678", "99999" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("64294d2d-f916-4b46-9063-0eebdc18796c"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("b90d4bfd-356a-48c5-a3f1-3680f878e0fa"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("b9ea3eff-44c0-4b35-91d0-0106bdacdcea"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("bbc6f0c1-e68f-45e1-9c08-fdbe446a55a4"));

            migrationBuilder.DeleteData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L);

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
    }
}
