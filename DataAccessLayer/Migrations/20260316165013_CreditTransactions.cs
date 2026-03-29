using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreditTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("18cad7c8-e800-4bb9-9f39-795a559c90c6"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("a50ba5f0-362f-4bf8-9598-b9835d93ea17"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("ccaac53e-e9ab-4dbb-bd8d-5a9809e5f399"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("f45bb2fe-77b6-41f5-a67e-436af2f170e9"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("6b1da1f6-def8-48ed-9696-a90385fe0eed"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("aa4b6e69-5f9d-4cc5-ac1a-276d7efc533a"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("cdc928c4-5c9a-426f-b86d-22a396662f79"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("d31e7152-df11-4ddf-85ca-8df7afa71351"));

            migrationBuilder.RenameColumn(
                name: "Credit",
                table: "Customers",
                newName: "CreditLimit");

            migrationBuilder.AddColumn<bool>(
                name: "IsCreditCustomer",
                table: "Customers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CreditTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    Credit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Debit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SaleId = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    TransactionReference = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    VehicleCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    StationCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditTransactions", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditTransactions");

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

            migrationBuilder.DropColumn(
                name: "IsCreditCustomer",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "CreditLimit",
                table: "Customers",
                newName: "Credit");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "154fb896-59e6-4f08-893c-a74a99193314", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1272), new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1271), new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1274), new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1265), "bfcad9ce-8823-4151-b7f5-5939cd31f0b9" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(238));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(245));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(249));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(253));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(257));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(261));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(266));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(271));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(274));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(278));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(281));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(285));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(290));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(293));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(317));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2346));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1825));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2251));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2256));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(825));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(829));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(834));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(837));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(840));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(843));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(846));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(849));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(851));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(854));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(857));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(860));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(863));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2032));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1944));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("18cad7c8-e800-4bb9-9f39-795a559c90c6"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1022), "" },
                    { new Guid("a50ba5f0-362f-4bf8-9598-b9835d93ea17"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1006), "" },
                    { new Guid("ccaac53e-e9ab-4dbb-bd8d-5a9809e5f399"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(994), "" },
                    { new Guid("f45bb2fe-77b6-41f5-a67e-436af2f170e9"), "02", "Bulk App", "", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1001), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2467));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2484));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1468));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2548));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2542));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2402), new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(2404) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("6b1da1f6-def8-48ed-9696-a90385fe0eed"), "02", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1664), "99999" },
                    { new Guid("aa4b6e69-5f9d-4cc5-ac1a-276d7efc533a"), "03", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1627), "99999" },
                    { new Guid("cdc928c4-5c9a-426f-b86d-22a396662f79"), "04", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1648), "99999" },
                    { new Guid("d31e7152-df11-4ddf-85ca-8df7afa71351"), "01", new DateTime(2026, 3, 15, 9, 38, 7, 148, DateTimeKind.Utc).AddTicks(1657), "99999" }
                });
        }
    }
}
