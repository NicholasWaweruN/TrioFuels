using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class accesapps_digit_increase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.RenameColumn(
                name: "storeNumber",
                table: "UsageBalanceDto",
                newName: "StoreNumber");

            migrationBuilder.AlterColumn<string>(
                name: "AccessApps",
                table: "AspNetUsers",
                type: "character varying(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(4)",
                oldUnicode: false,
                oldMaxLength: 4);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "bf6a374b-7b0b-452a-81ba-b891f2ce13e0", new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7580), new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7579), new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7581), new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7576), "2bd7713c-ce60-490f-b136-2dd69d457ad7" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6968));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6971));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6973));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6975));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6977));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6978));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6980));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6981));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(6983));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8793));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8105));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7810));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8075));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8078));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7338));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7341));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7342));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7344));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7346));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7347));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7349));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7351));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7352));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7354));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7355));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7357));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8050));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8262));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8265));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8267));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8988));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8991));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8993));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7839));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7840));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7842));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8183));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8194));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8293));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8296));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8297));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8299));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(7613));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8227));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8222));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 15, 10, 28, 27, 902, DateTimeKind.Utc).AddTicks(8958));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoreNumber",
                table: "UsageBalanceDto",
                newName: "storeNumber");

            migrationBuilder.AlterColumn<string>(
                name: "AccessApps",
                table: "AspNetUsers",
                type: "character varying(4)",
                unicode: false,
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldUnicode: false,
                oldMaxLength: 20);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "cc6a7d94-6c03-44f4-8baa-82cc348d261b", new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(5279), new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(5278), new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(5281), new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(5274), "062f7734-b476-4ccb-8fa7-965b4ccb32c8" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3779));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3787));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3792));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3796));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3801));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3832));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3836));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3870));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3874));

            migrationBuilder.InsertData(
                table: "Codegenerators",
                columns: new[] { "Id", "DateCreated", "Length", "NextNumber", "Prefix", "Seed", "Suffix", "TypeName", "UserCode" },
                values: new object[,]
                {
                    { 6L, new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3805), 5, 10000, "", 1, "", "BULCUST", "00001" },
                    { 7L, new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3810), 7, 0, "", 1, "", "PLANID", "00001" },
                    { 8L, new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3814), 2, 0, "T", 1, "", "TANKID", "00001" },
                    { 9L, new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3819), 5, 0, "", 1, "", "TILLID", "00001" },
                    { 10L, new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3823), 4, 0, "P", 1, "", "PDAID", "00001" },
                    { 11L, new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3827), 7, 0, "", 1, "", "VEHICLEID", "00001" }
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(8643));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(5944));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(5563));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(5855));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(5864));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(4749));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(4757));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(4761));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(4764));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(4768));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(4772));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(4775));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(4778));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(4782));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(4786));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(4789));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(4792));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(5773));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(6400));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(6405));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(6409));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(8824));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(8831));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(8836));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(5651));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(5658));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(5664));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(6181));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(6200));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(6513));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(6518));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(6522));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(6525));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(5386));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(6296));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(6288));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(8726));
        }
    }
}
