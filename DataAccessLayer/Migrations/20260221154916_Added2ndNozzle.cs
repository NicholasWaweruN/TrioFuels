using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Added2ndNozzle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("604b03d1-7ae4-407d-baa2-8f6cbdedfc30"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("88678da7-5b89-4652-a038-d2d98b840c70"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("937a110e-d245-42d2-b1d4-6a5e50e935b9"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("bd7c1a23-b2db-4762-b879-c65c2f2ffd64"));

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("f7a91d61-23f7-46c1-bfd5-bc102dbd35a7"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "0e1e0e95-d575-4bf1-94f0-25fec53d8c64", new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8547), new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8546), new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8547), new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8542), "242b6129-e10b-4120-9e23-5f61022b07e9" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8040));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8044));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8046));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8048));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8050));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8051));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8053));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8055));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8056));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8058));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8059));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8061));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8063));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8064));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8691));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8622));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8666));

            migrationBuilder.InsertData(
                table: "Nozzles",
                columns: new[] { "Id", "DateCreated", "DispenserCode", "IsActive", "NozzleCode", "NozzleName", "UserCode" },
                values: new object[] { 2L, new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8668), "D01", true, "N02", "N01", "00001" });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8292));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8295));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8296));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8297));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8299));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8300));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8301));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8303));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8304));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8305));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8306));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8644));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("416e04bb-3da5-4085-8661-543cfea4938e"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8401), "" },
                    { new Guid("8e190461-76a7-480f-8b19-bba605a34662"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8395), "" },
                    { new Guid("c2be3dd7-0c5f-4a4b-82e4-b1ea02fc7344"), "02", "Bulk App", "", new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8399), "" },
                    { new Guid("f1a08308-673c-431d-a0c8-d84da08020ec"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8403), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8733));

            migrationBuilder.InsertData(
                table: "QuantityTransactions",
                columns: new[] { "Id", "AmountCredit", "AmountDebit", "DateCreated", "Discount", "DispenserCode", "IsReversed", "NozzleCode", "OtpUsed", "PaymentTypeCode", "Price", "QuantityCredit", "QuantityDebit", "SaleId", "ShiftNumber", "StationCode", "UserCode", "Vat_Amount", "VehicleCode" },
                values: new object[] { 2L, 0m, 0m, new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8745), 0m, "D01", false, "N02", "", 99, 0m, 50m, 0m, "", "", "S001", "99999", 0m, "" });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8575));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8768));

            migrationBuilder.InsertData(
                table: "StockTakes",
                columns: new[] { "Id", "ClosingReading", "DateCreated", "NozzleCode", "OpeningReading", "ShiftNumber", "TakeType", "UserCode" },
                values: new object[] { -1L, 0m, new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8771), "N02", 50m, "", 99, "99999" });

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8709), new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[] { new Guid("2ac54ba4-c394-4529-a08e-30f9e7378055"), "03", new DateTime(2026, 2, 21, 15, 49, 15, 958, DateTimeKind.Utc).AddTicks(8605), "99999" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("416e04bb-3da5-4085-8661-543cfea4938e"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("8e190461-76a7-480f-8b19-bba605a34662"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("c2be3dd7-0c5f-4a4b-82e4-b1ea02fc7344"));

            migrationBuilder.DeleteData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("f1a08308-673c-431d-a0c8-d84da08020ec"));

            migrationBuilder.DeleteData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L);

            migrationBuilder.DeleteData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("2ac54ba4-c394-4529-a08e-30f9e7378055"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "bff3a690-313a-4e1f-966f-5db58b25966c", new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5814), new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5813), new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5815), new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5810), "7266118f-b584-4c92-ab23-a23097c6c96e" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5183));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5188));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5190));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5193));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5195));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5197));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5199));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5201));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5203));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5205));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5207));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5209));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5212));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5214));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(6010));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5915));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5978));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5479));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5554));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5556));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5557));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5559));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5561));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5562));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5564));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5566));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5568));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5569));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5948));

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("604b03d1-7ae4-407d-baa2-8f6cbdedfc30"), "02", "Bulk App", "", new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5657), "" },
                    { new Guid("88678da7-5b89-4652-a038-d2d98b840c70"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5661), "" },
                    { new Guid("937a110e-d245-42d2-b1d4-6a5e50e935b9"), "01", "Bulk DashBoard", "", new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5654), "" },
                    { new Guid("bd7c1a23-b2db-4762-b879-c65c2f2ffd64"), "04", "Fuel Flow App", "", new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5674), "" }
                });

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(6070));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5851));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(6107));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(6037), new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(6038) });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[] { new Guid("f7a91d61-23f7-46c1-bfd5-bc102dbd35a7"), "03", new DateTime(2026, 2, 20, 18, 15, 5, 645, DateTimeKind.Utc).AddTicks(5887), "99999" });
        }
    }
}
