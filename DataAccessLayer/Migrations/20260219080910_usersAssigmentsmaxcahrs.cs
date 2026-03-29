using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class usersAssigmentsmaxcahrs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "RoleCode",
                table: "RoleToUser",
                type: "character varying(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "RoleCode",
                table: "RoleAndPermisions",
                type: "character varying(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PermissionCode",
                table: "RoleAndPermisions",
                type: "character varying(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Role",
                type: "character varying(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "RoleCode",
                table: "Role",
                type: "character varying(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "ApiPermisions",
                type: "character varying(140)",
                unicode: false,
                maxLength: 140,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "ApiPermission",
                table: "ApiPermisions",
                type: "character varying(150)",
                unicode: false,
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldUnicode: false,
                oldMaxLength: 50);

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
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5995));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 19, 8, 9, 7, 556, DateTimeKind.Utc).AddTicks(5999));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "RoleCode",
                table: "RoleToUser",
                type: "character varying(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "RoleCode",
                table: "RoleAndPermisions",
                type: "character varying(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "PermissionCode",
                table: "RoleAndPermisions",
                type: "character varying(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Role",
                type: "character varying(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "RoleCode",
                table: "Role",
                type: "character varying(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "ApiPermisions",
                type: "character varying(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(140)",
                oldUnicode: false,
                oldMaxLength: 140);

            migrationBuilder.AlterColumn<string>(
                name: "ApiPermission",
                table: "ApiPermisions",
                type: "character varying(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldUnicode: false,
                oldMaxLength: 150);

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

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(8417), new DateTime(2026, 2, 19, 7, 20, 40, 413, DateTimeKind.Utc).AddTicks(8418) });
        }
    }
}
