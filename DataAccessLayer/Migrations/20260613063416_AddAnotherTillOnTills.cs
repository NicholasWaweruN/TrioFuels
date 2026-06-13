using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddAnotherTillOnTills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3805));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3814));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3819));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3823));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(3827));

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

            migrationBuilder.InsertData(
                table: "Tills",
                columns: new[] { "Id", "DateCreated", "IsActive", "LastFetch", "OffsetValue", "StoreNumber", "TillName", "TillNumber", "UserCode" },
                values: new object[] { 5L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "5617660", "Till 5", "5617660", "99999" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 13, 6, 34, 13, 261, DateTimeKind.Utc).AddTicks(8726));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tills",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "8508fc3f-5c58-494c-9c92-24be6fca6192", new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4760), new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4759), new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4761), new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4756), "f6f5ac75-4bda-4885-acae-453cc52f9e3d" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(3990));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(3997));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4003));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4005));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4008));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4011));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4014));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4016));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4019));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4022));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4024));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4027));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4030));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4043));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(6377));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5097));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4919));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5048));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5052));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4403));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4407));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4410));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4412));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4414));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4416));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4419));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4421));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4423));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4425));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4428));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4430));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5003));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5494));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5498));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5501));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(6489));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(6494));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(6498));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4961));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4965));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4968));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5206));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5217));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5557));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5562));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5564));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5566));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(4819));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5434));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(5427));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 6, 11, 10, 6, 40, 959, DateTimeKind.Utc).AddTicks(6424));
        }
    }
}
