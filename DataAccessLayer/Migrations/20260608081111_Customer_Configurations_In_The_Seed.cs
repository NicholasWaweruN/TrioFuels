using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Customer_Configurations_In_The_Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "39d7aef3-02ea-4c64-a059-7a0c3bcba707", new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3775), new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3774), new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3776), new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3772), "efd5812a-47c1-406b-8c3e-1bc6939f2f62" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3188));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3192));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3194));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3196));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3198));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3200));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3202));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3204));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3206));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3208));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3209));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3211));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3213));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3215));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3227));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "BaseLoyaltyPoints", "CreditLimit", "CustomerCode", "CustomerEmail", "CustomerName", "CustomerPhone", "DateCreated", "IdentificationNumber", "IsCreditCustomer", "KRAPin", "OrganisationCode", "Receive_Receipts", "Receive_Statements", "UserCode" },
                values: new object[] { 1L, 1m, 0m, "C00001", "test@fuelflow.com", "System Test Vehicle", "0715821303", new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4902), "27838753", true, "", "ORG001", false, false, "" });

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4018));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3888));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3982));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3984));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3492));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3494));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3496));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3498));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3499));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3501));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3502));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3504));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3506));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3507));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3509));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3511));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4191));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4194));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4195));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3922));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4083));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4091));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4227));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(3815));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4129));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4126));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4054), new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4054) });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "ConversionDate", "ConversionStation", "CreditLimit", "CustomerCode", "DateCreated", "Discount", "IsActive", "IsTelematicInstalled", "NFC_CardNumber", "PhoneNumber", "PhoneNumber2", "ProductCode", "RoyaltyPointPerLitre", "Status", "TankCapacity", "TelematicInstallationDate", "TelematicSerialNumber", "TransactionPIN", "UserCode", "VehicleCode", "VehicleMake", "VehicleModel", "VehicleRegistrationNumber" },
                values: new object[] { 1L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", 1000m, "C00001", new DateTime(2026, 6, 8, 8, 11, 10, 575, DateTimeKind.Utc).AddTicks(4939), 0m, true, false, "0000000000", "0715821303", "", "01", 1m, "Active", 60, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "0000", "", "V001", "Toyota", "Walk-In", "KDL849R" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "0e141f5c-bda4-42d4-8bd2-e12e5f0a0dcb", new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8706), new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8705), new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8707), new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8702), "436ea0d3-1225-4683-9148-ce1b8077d5f4" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7879));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7887));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7890));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7893));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7895));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7898));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7901));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7903));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7906));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7908));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7911));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7914));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7916));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7919));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(7931));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9238));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8865));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9197));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9200));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8347));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8362));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8365));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8367));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8369));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8372));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8374));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8376));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8378));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8381));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8383));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8385));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9157));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9429));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9432));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9434));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9114));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9318));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9484));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(8762));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9378));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9374));

            migrationBuilder.UpdateData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "LastFetch" },
                values: new object[] { new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9279), new DateTime(2026, 6, 8, 6, 13, 17, 842, DateTimeKind.Utc).AddTicks(9280) });
        }
    }
}
