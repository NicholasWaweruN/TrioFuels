using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AllUserAppsAddeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("63be1611-b048-495b-a224-cfb3ca203a6a"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("d4159556-faaa-4364-b22c-4c7bc3711209"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("fac05c59-9dec-45d4-98f1-49ca4b98106c"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("fc735bb6-eb9b-45d7-a845-1c6c74543ecb"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("39b0ca6f-2d67-44a6-ad14-53c182fced7d"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("9254d65d-de68-4b87-b4fc-69f36fcbffbb"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("a1e766dc-296e-43fe-9b01-99937ea234cb"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("b845469a-5909-454e-b831-a085130f8113"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "47a87fa0-b443-4111-90d2-2bcc750d3700", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8566), new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8565), new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8568), new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8558), "a8fc3619-de96-4c88-88cc-331ce367c912" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7244));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7252));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7257));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7261));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7265));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7269));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7273));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7276));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7280));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7283));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7286));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7290));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7312));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7316));

            migrationBuilder.InsertData(
                table: "Codegenerators",
                columns: new[] { "Id", "DateCreated", "Length", "NextNumber", "Prefix", "Seed", "Suffix", "TypeName", "UserCode" },
                values: new object[] { 17L, new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7319), 5, 1, "", 1, "", "VehicleCode", "00001" });

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9118));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8871));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9015));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9021));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7992));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(7998));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8002));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8005));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8008));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "DateCreated", "PaymentTypeName" },
                values: new object[] { new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8011), "Employee_Mpesa_Payments" });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8014));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8017));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8020));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8023));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "DateCreated", "IsAppUsed", "PaymentTypeName" },
                values: new object[] { new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8026), false, "BatchVoucher" });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "DateCreated", "HasValue", "IsAppUsed", "PaymentTypeId", "PaymentTypeName", "ProcessType", "UserCode" },
                values: new object[,]
                {
                    { 12L, new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8029), true, true, 11, "Personal_Wallet", "", "00001" },
                    { 13L, new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8032), true, true, 12, "Cash", "", "00001" }
                });

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8942));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("14ebbbaa-119c-4388-9c92-6a365df6c585"), "02", "Bulk App", "", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8209), "" },
                    { new Guid("35bda58b-b5f8-407d-9e93-03f314e60a4b"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8200), "" },
                    { new Guid("48ef101d-ed69-42ef-ae35-7d7b5449907d"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8250), "" },
                    { new Guid("df392818-33b1-4e53-8c54-717a138e04e9"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8257), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9260));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9278));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8676));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9363));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9355));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9185), new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(9186) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("0cbce2d1-ca85-4d9e-8062-3135ed45852c"), "01", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8792), "99999" },
                    { new Guid("6ff0cc5c-8fa2-4f64-8f06-93ee6b3c4cbc"), "02", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8798), "99999" },
                    { new Guid("a875f6e4-394f-4fa3-aef1-471073ef4dcf"), "03", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8768), "99999" },
                    { new Guid("d10f8bb0-519a-4486-8887-131a68ba04a2"), "04", new DateTime(2026, 2, 23, 18, 5, 43, 395, DateTimeKind.Utc).AddTicks(8786), "99999" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("14ebbbaa-119c-4388-9c92-6a365df6c585"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("35bda58b-b5f8-407d-9e93-03f314e60a4b"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("48ef101d-ed69-42ef-ae35-7d7b5449907d"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("df392818-33b1-4e53-8c54-717a138e04e9"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("0cbce2d1-ca85-4d9e-8062-3135ed45852c"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("6ff0cc5c-8fa2-4f64-8f06-93ee6b3c4cbc"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("a875f6e4-394f-4fa3-aef1-471073ef4dcf"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("d10f8bb0-519a-4486-8887-131a68ba04a2"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "ea7373cd-c504-4102-99fa-4e1bb5fdb6bd", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4878), new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4877), new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4879), new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4873), "0dd79e78-f79c-4eae-85b8-ec345fe2907a" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8405));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8410));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8520));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8524));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8526));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8529));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8532));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8534));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8536));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8539));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8541));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8543));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 19, DateTimeKind.Utc).AddTicks(8548));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5165));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5033));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5122));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5125));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4317));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4342));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4345));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4347));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4349));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "DateCreated", "PaymentTypeName" },
                values: new object[] { new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4351), "Salary" });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4353));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4355));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4357));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4359));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "DateCreated", "IsAppUsed", "PaymentTypeName" },
                values: new object[] { new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4361), true, "Hand_Cash" });

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5079));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("63be1611-b048-495b-a224-cfb3ca203a6a"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4659), "" },
                    { new Guid("d4159556-faaa-4364-b22c-4c7bc3711209"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4681), "" },
                    { new Guid("fac05c59-9dec-45d4-98f1-49ca4b98106c"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4677), "" },
                    { new Guid("fc735bb6-eb9b-45d7-a845-1c6c74543ecb"), "02", "Bulk App", "", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4673), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5244));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5256));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4933));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5480));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5476));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5200), new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("39b0ca6f-2d67-44a6-ad14-53c182fced7d"), "01", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4990), "99999" },
                    { new Guid("9254d65d-de68-4b87-b4fc-69f36fcbffbb"), "04", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4987), "99999" },
                    { new Guid("a1e766dc-296e-43fe-9b01-99937ea234cb"), "02", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4994), "99999" },
                    { new Guid("b845469a-5909-454e-b831-a085130f8113"), "03", new DateTime(2026, 2, 23, 13, 43, 50, 22, DateTimeKind.Utc).AddTicks(4981), "99999" }
                });
        }
    }
}
