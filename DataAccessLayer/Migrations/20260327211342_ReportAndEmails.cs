using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class ReportAndEmails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("11aedac2-0371-486c-bc10-580d9a76b9cf"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("3b4fe686-6c38-4e00-a71c-0744244e5158"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("4207b92e-734a-4dbd-befa-3e5bc4f0b9be"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("6a1b4e98-c062-4e1d-a655-66004ffd338f"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("17a61aa8-e29c-4b48-8328-b05c6ce23231"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("35a3a313-7e72-432b-8603-0f92f1075001"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("89f4a963-4c58-494d-a2f1-01f2c5e53d27"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("f420bbb2-e979-4c85-8b02-7fb3d4597072"));

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ReportName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "Email", "LastLoginDate", "NormalizedEmail", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "432c6cad-4839-4ce1-9f73-6a2a6df7a7ed", new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3679), new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3678), "nicholas@fuelflo.com", new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3680), "NICHOLAS@FUELFLOW.COM", new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3672), "4eb73bc2-5622-40e8-afaa-4459f0fd0604" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(2765));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(2771));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(2775));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(2779));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(2782));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(2785));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(2788));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(2791));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(2795));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(2798));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(2801));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(2804));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(2807));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(2810));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(2823));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(4252));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3855));

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "DateCreated", "From", "NotificationName", "ReportCode", "To", "ToCC", "UserCode" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Variance Report", "001", "", "", "99999" },
                    { 2L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Sales Report", "002", "", "", "99999" },
                    { 3L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Monthly Sales Report", "003", "", "", "99999" },
                    { 4L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Cumulative Variance Report", "004", "", "", "99999" },
                    { 5L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Mpesa Usage Report", "005", "", "", "99999" }
                });

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(4200));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(4205));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3267));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3270));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3273));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3276));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3279));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3281));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3284));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3286));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3289));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3291));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3294));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3296));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(4139));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(4466));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(4470));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(4473));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3910));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("556fe0d5-e13e-4df8-a6d2-0c101b89f89e"), "02", "Bulk App", "", new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3443), "" },
                    { new Guid("56c6889f-60b4-4afd-991f-79d756b10409"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3453), "" },
                    { new Guid("9dd6cf42-3451-411c-89a8-071874742c34"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3448), "" },
                    { new Guid("f0f0cf6d-a5c1-452d-a5bf-f774af7f21a3"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3436), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(4352));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(4368));

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "ReportName" },
                values: new object[,]
                {
                    { "001", "Variance Report" },
                    { "002", "Sales Report" },
                    { "003", "Monthly Sales Report" },
                    { "004", "Cumulative Variance Report" },
                    { "005", "Mpesa Usage Report" }
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(4617));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3739));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(4418));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(4413));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(4303), new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(4303) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("2456b230-5357-4628-bf51-1468e00afe4f"), "03", new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3788), "99999" },
                    { new Guid("303e01a8-82f2-49be-9d35-add60530d606"), "02", new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3809), "99999" },
                    { new Guid("9bc95ba9-ad07-4df6-a13f-d079d82fd57b"), "04", new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3794), "99999" },
                    { new Guid("b62aa076-e86e-4782-8ede-7549b3df7c2d"), "01", new DateTime(2026, 3, 27, 21, 13, 39, 891, DateTimeKind.Utc).AddTicks(3805), "99999" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DeleteData(
                table: "Emails",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Emails",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Emails",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Emails",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Emails",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("556fe0d5-e13e-4df8-a6d2-0c101b89f89e"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("56c6889f-60b4-4afd-991f-79d756b10409"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("9dd6cf42-3451-411c-89a8-071874742c34"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("f0f0cf6d-a5c1-452d-a5bf-f774af7f21a3"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("2456b230-5357-4628-bf51-1468e00afe4f"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("303e01a8-82f2-49be-9d35-add60530d606"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("9bc95ba9-ad07-4df6-a13f-d079d82fd57b"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("b62aa076-e86e-4782-8ede-7549b3df7c2d"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "Email", "LastLoginDate", "NormalizedEmail", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "604ab914-46fa-42f2-8435-b0f8b8a0589b", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1701), new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1700), "admin@protoenergy.com", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1704), "ADMIN@PROTOENERGY.COM", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1693), "f5d6e2c9-bd02-41c8-8904-902e9ed99299" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(418));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(429));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(436));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(442));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(448));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(453));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(459));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(464));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(470));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(476));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(481));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(487));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(493));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(498));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(519));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2338));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2011));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2251));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2257));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1082));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1088));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1094));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1099));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1105));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1109));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1114));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1118));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1123));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1128));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1132));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1136));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2176));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2718));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2723));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2727));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2108));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("11aedac2-0371-486c-bc10-580d9a76b9cf"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1322), "" },
                    { new Guid("3b4fe686-6c38-4e00-a71c-0744244e5158"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1367), "" },
                    { new Guid("4207b92e-734a-4dbd-befa-3e5bc4f0b9be"), "02", "Bulk App", "", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1350), "" },
                    { new Guid("6a1b4e98-c062-4e1d-a655-66004ffd338f"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1359), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2531));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2558));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2823));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1804));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2644));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2637));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2445), new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(2446) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("17a61aa8-e29c-4b48-8328-b05c6ce23231"), "04", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1915), "99999" },
                    { new Guid("35a3a313-7e72-432b-8603-0f92f1075001"), "03", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1905), "99999" },
                    { new Guid("89f4a963-4c58-494d-a2f1-01f2c5e53d27"), "02", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1932), "99999" },
                    { new Guid("f420bbb2-e979-4c85-8b02-7fb3d4597072"), "01", new DateTime(2026, 3, 21, 14, 20, 48, 894, DateTimeKind.Utc).AddTicks(1924), "99999" }
                });
        }
    }
}
