using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class NewCarwashTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carWashTransactionItems_CarWashProducts_ProductId",
                table: "carWashTransactionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_carWashTransactionItems_CarWashTransactions_TransactionId",
                table: "carWashTransactionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_carWashTransactionItems",
                table: "carWashTransactionItems");

            migrationBuilder.RenameTable(
                name: "carWashTransactionItems",
                newName: "CarWashTransactionItems");

            migrationBuilder.RenameIndex(
                name: "IX_carWashTransactionItems_TransactionId",
                table: "CarWashTransactionItems",
                newName: "IX_CarWashTransactionItems_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_carWashTransactionItems_ProductId",
                table: "CarWashTransactionItems",
                newName: "IX_CarWashTransactionItems_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarWashTransactionItems",
                table: "CarWashTransactionItems",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "0a22f82d-f794-4033-9841-91b9bb5f921a", new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9168), new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9167), new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9170), new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9164), "f537e4f4-831e-4ea1-b0e0-143973320741" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8355));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8363));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8366));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8369));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8372));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8376));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8379));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8391));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8395));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 288, DateTimeKind.Unspecified).AddTicks(1199));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9552));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9337));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9508));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9512));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8824));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8827));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8830));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8834));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8836));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8845));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8864));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8867));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8870));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8873));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8876));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8879));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(8882));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9447));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9453));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9457));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9461));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9888));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9891));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9894));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 288, DateTimeKind.Unspecified).AddTicks(1314));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 288, DateTimeKind.Unspecified).AddTicks(1320));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 288, DateTimeKind.Unspecified).AddTicks(1323));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9387));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9391));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9394));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9726));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9737));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9950));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9953));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9956));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9959));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9229));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9829));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 287, DateTimeKind.Unspecified).AddTicks(9824));

            migrationBuilder.InsertData(
                table: "VehicleTypes",
                columns: new[] { "Id", "DateCreated", "IsActive", "Name", "UserCode" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 7, 12, 15, 17, 52, 288, DateTimeKind.Unspecified).AddTicks(1389), true, "Saloon", "" },
                    { 2L, new DateTime(2026, 7, 12, 15, 17, 52, 288, DateTimeKind.Unspecified).AddTicks(1391), true, "SUV", "" },
                    { 3L, new DateTime(2026, 7, 12, 15, 17, 52, 288, DateTimeKind.Unspecified).AddTicks(1393), true, "Truck", "" },
                    { 4L, new DateTime(2026, 7, 12, 15, 17, 52, 288, DateTimeKind.Unspecified).AddTicks(1395), true, "Trailer", "" },
                    { 5L, new DateTime(2026, 7, 12, 15, 17, 52, 288, DateTimeKind.Unspecified).AddTicks(1381), true, "Motorcycle", "" },
                    { 6L, new DateTime(2026, 7, 12, 15, 17, 52, 288, DateTimeKind.Unspecified).AddTicks(1387), true, "Tuk Tuk", "" }
                });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 15, 17, 52, 288, DateTimeKind.Unspecified).AddTicks(1257));

            migrationBuilder.AddForeignKey(
                name: "FK_CarWashTransactionItems_CarWashProducts_ProductId",
                table: "CarWashTransactionItems",
                column: "ProductId",
                principalTable: "CarWashProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarWashTransactionItems_CarWashTransactions_TransactionId",
                table: "CarWashTransactionItems",
                column: "TransactionId",
                principalTable: "CarWashTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarWashTransactionItems_CarWashProducts_ProductId",
                table: "CarWashTransactionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CarWashTransactionItems_CarWashTransactions_TransactionId",
                table: "CarWashTransactionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarWashTransactionItems",
                table: "CarWashTransactionItems");

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.RenameTable(
                name: "CarWashTransactionItems",
                newName: "carWashTransactionItems");

            migrationBuilder.RenameIndex(
                name: "IX_CarWashTransactionItems_TransactionId",
                table: "carWashTransactionItems",
                newName: "IX_carWashTransactionItems_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_CarWashTransactionItems_ProductId",
                table: "carWashTransactionItems",
                newName: "IX_carWashTransactionItems_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_carWashTransactionItems",
                table: "carWashTransactionItems",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "29189ed4-3be0-4b57-80f5-930746afb707", new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7605), new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7604), new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7608), new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7600), "fca513f1-d238-4480-823e-c1db58cf1b09" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6123));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6130));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6134));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6140));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6144));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6148));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6152));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6180));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6184));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 763, DateTimeKind.Unspecified).AddTicks(108));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7892));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7735));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7856));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7861));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6590));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6594));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6597));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6601));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6604));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6607));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6634));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6638));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6642));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6645));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6648));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6650));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(6653));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7810));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7816));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7820));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7823));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8108));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8112));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8115));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 763, DateTimeKind.Unspecified).AddTicks(211));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 763, DateTimeKind.Unspecified).AddTicks(216));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 763, DateTimeKind.Unspecified).AddTicks(220));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7771));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7775));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7778));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7989));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8010));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8157));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8163));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8166));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8168));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(7656));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8056));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 762, DateTimeKind.Unspecified).AddTicks(8051));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 14, 55, 11, 763, DateTimeKind.Unspecified).AddTicks(158));

            migrationBuilder.AddForeignKey(
                name: "FK_carWashTransactionItems_CarWashProducts_ProductId",
                table: "carWashTransactionItems",
                column: "ProductId",
                principalTable: "CarWashProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_carWashTransactionItems_CarWashTransactions_TransactionId",
                table: "carWashTransactionItems",
                column: "TransactionId",
                principalTable: "CarWashTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
