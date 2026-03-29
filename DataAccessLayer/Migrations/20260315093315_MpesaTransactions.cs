using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class MpesaTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("96ec4530-e9c4-4521-848e-bea2ad33c563"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("b46898e9-8964-430d-ade7-18001880a974"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("bcb06588-c5b8-4555-b04c-f2c25a4b0748"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("d4d5d811-6153-48ff-a2b3-25017a1297e7"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("85e7f4cd-6df9-44bd-b9a9-db6c81458b46"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("c5b90c68-34c9-4467-b721-d841b459c0dc"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("f0da02fc-2bfb-4a93-91d5-817f5bec873f"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("fdd15f4f-c4be-4e7d-8a89-cab31decc65e"));

            migrationBuilder.AddColumn<string>(
                name: "CheckoutRequestID",
                table: "MpesaTransactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MerchantRequestID",
                table: "MpesaTransactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MpesaReceiptNumber",
                table: "MpesaTransactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "MpesaTransactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TillName",
                table: "MpesaTransactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TillNumber",
                table: "MpesaTransactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "KRAPin",
                table: "Customers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "3ef3d5ed-03eb-4313-8227-e34fc980729c", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5892), new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5891), new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5894), new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5885), "bcfcb4ef-b551-466e-afc4-0245032cfd2c" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4834));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4841));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4845));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4849));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4853));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4856));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4860));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4863));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4866));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4870));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4873));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4876));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4882));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4885));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(4898));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6280));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6075));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6228));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6232));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5354));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5358));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5360));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5364));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5368));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5371));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5375));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5377));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5380));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5382));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5384));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5387));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5390));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6180));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateCreated", "IsActive", "ProductCode", "ProductName", "UserCode" },
                values: new object[] { -1L, new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6136), true, "02", "Diesel", "000001" });

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("5904b303-d852-4969-b659-67da3fbda4df"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5646), "" },
                    { new Guid("a989e807-5bea-4894-accb-7c698a05108c"), "04", "Fuel Flow App", "", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5662), "" },
                    { new Guid("a9f536be-0805-4c43-84a9-df2789e917dc"), "02", "Bulk App", "", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5641), "" },
                    { new Guid("c449a262-ad99-405e-9cb5-1e53b40ca2d5"), "01", "Bulk DashBoard", "", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5636), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6373));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6387));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(5952));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6444));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6438));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6320), new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6321) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("8700ef75-db3c-4843-a83f-928e3274db82"), "04", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6012), "99999" },
                    { new Guid("98b54821-6e5d-4a6a-8b7c-fb43a71dd4ab"), "01", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6023), "99999" },
                    { new Guid("9edcce50-28c9-40c1-b9b7-cb605d560b96"), "02", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6028), "99999" },
                    { new Guid("a1b2a598-9905-4f1d-962d-d34f78f49924"), "03", new DateTime(2026, 3, 15, 9, 33, 12, 839, DateTimeKind.Utc).AddTicks(6007), "99999" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L);

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("5904b303-d852-4969-b659-67da3fbda4df"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("a989e807-5bea-4894-accb-7c698a05108c"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("a9f536be-0805-4c43-84a9-df2789e917dc"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("c449a262-ad99-405e-9cb5-1e53b40ca2d5"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("8700ef75-db3c-4843-a83f-928e3274db82"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("98b54821-6e5d-4a6a-8b7c-fb43a71dd4ab"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("9edcce50-28c9-40c1-b9b7-cb605d560b96"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("a1b2a598-9905-4f1d-962d-d34f78f49924"));

            migrationBuilder.DropColumn(
                name: "CheckoutRequestID",
                table: "MpesaTransactions");

            migrationBuilder.DropColumn(
                name: "MerchantRequestID",
                table: "MpesaTransactions");

            migrationBuilder.DropColumn(
                name: "MpesaReceiptNumber",
                table: "MpesaTransactions");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "MpesaTransactions");

            migrationBuilder.DropColumn(
                name: "TillName",
                table: "MpesaTransactions");

            migrationBuilder.DropColumn(
                name: "TillNumber",
                table: "MpesaTransactions");

            migrationBuilder.AlterColumn<string>(
                name: "KRAPin",
                table: "Customers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "d6530d7b-89da-4ae3-89bb-939fea5ced54", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9489), new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9488), new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9490), new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9483), "1a6af186-ae1b-4f5b-9a0f-6f79207744f6" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8734));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8740));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8743));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8747));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8750));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8753));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8756));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8758));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8761));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8764));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8767));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8769));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8772));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8775));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(8787));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9860));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9650));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9813));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9817));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9128));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9131));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9134));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9136));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9139));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9143));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9145));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9148));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9150));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9153));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9155));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9157));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9694));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("96ec4530-e9c4-4521-848e-bea2ad33c563"), "02", "Bulk App", "", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9284), "" },
                    { new Guid("b46898e9-8964-430d-ade7-18001880a974"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9288), "" },
                    { new Guid("bcb06588-c5b8-4555-b04c-f2c25a4b0748"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9292), "" },
                    { new Guid("d4d5d811-6153-48ff-a2b3-25017a1297e7"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9279), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9939));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9954));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9543));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 691, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9996));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9896), new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9897) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("85e7f4cd-6df9-44bd-b9a9-db6c81458b46"), "03", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9591), "99999" },
                    { new Guid("c5b90c68-34c9-4467-b721-d841b459c0dc"), "04", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9596), "99999" },
                    { new Guid("f0da02fc-2bfb-4a93-91d5-817f5bec873f"), "02", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9608), "99999" },
                    { new Guid("fdd15f4f-c4be-4e7d-8a89-cab31decc65e"), "01", new DateTime(2026, 2, 26, 13, 29, 0, 690, DateTimeKind.Utc).AddTicks(9604), "99999" }
                });
        }
    }
}
