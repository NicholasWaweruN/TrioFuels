using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class LatestUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "66b3baa5-8aee-43f6-8df6-8b54f2aec498", new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6400), new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6399), new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6402), new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6392), "c00f5ae9-4e0e-4255-97c1-a7316fd9a8f9" });

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(25));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(39));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(49));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(58));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(67));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(77));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(86));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(96));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(105));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(115));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(125));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(134));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(144));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(153));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(162));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(171));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(181));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 18L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(190));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 19L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(200));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 20L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 21L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(220));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 22L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(229));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 23L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(254));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 24L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(264));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 25L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(273));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 26L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(282));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 27L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(293));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 28L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(295));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 29L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(298));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 30L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(301));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 31L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(305));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 32L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(308));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 33L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(311));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 34L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(315));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 35L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(318));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 36L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(321));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 37L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(325));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 38L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(328));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 39L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(332));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 40L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(340));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 41L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(347));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 42L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(354));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 43L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(383));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 44L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(390));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 45L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(393));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 46L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(397));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 47L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(400));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 48L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(404));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 49L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(407));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 50L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(410));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 51L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(413));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 52L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(417));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 53L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(421));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 54L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(424));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(656));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(663));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(666));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(668));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(671));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(673));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(676));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(678));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 882, DateTimeKind.Unspecified).AddTicks(681));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(5368));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(5375));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(5381));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(5385));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(5390));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(5394));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(5398));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(5417));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(5421));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(9140));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6807));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6575));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6753));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6759));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6012));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6017));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6021));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6024));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6028));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6043));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6070));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6074));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6078));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6081));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6085));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6089));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6093));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6686));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6693));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6698));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6703));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(7090));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(7095));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(7099));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(9520));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(9539));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(9557));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6629));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6633));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6637));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6954));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6967));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(7157));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(7162));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(7165));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(7169));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(6462));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(7024));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(7018));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(9772));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(9775));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(9779));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(9785));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(9759));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(9768));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 17, 19, 17, 881, DateTimeKind.Unspecified).AddTicks(9352));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "82ac803b-a343-451d-9d84-0bb6c509fe76", new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7979), new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7978), new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7981), new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7973), "ef6dfd0a-f9f8-40cf-9040-22cc7d22391b" });

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9974));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9978));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9981));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9983));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9985));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9987));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9989));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9991));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9993));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9995));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9998));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(2));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(4));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(6));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(8));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(10));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 18L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(12));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 19L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(14));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 20L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(17));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 21L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(19));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 22L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(21));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 23L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(31));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 24L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(33));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 25L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(36));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 26L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(38));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 27L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(40));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 28L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(43));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 29L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(45));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 30L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(47));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 31L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(49));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 32L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(51));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 33L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(53));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 34L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(55));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 35L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(58));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 36L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(61));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 37L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(63));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 38L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(65));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 39L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(67));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 40L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(69));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 41L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(74));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 42L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(77));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 43L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(79));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 44L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(81));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 45L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(83));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 46L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(85));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 47L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(88));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 48L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(90));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 49L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(92));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 50L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(94));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 51L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(96));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 52L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(98));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 53L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(100));

            migrationBuilder.UpdateData(
                table: "CarWashProductPrices",
                keyColumn: "Id",
                keyValue: 54L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(102));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(198));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(202));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(205));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(208));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(213));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(215));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(217));

            migrationBuilder.UpdateData(
                table: "CarWashProducts",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 711, DateTimeKind.Unspecified).AddTicks(220));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7183));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7191));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7196));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7202));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7206));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7214));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7232));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7237));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9741));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8336));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8133));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8287));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8293));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7598));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7603));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7607));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7610));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7614));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7617));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7665));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7668));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7672));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7675));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7678));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7682));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(7685));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8228));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8238));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8242));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8246));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8547));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8552));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8555));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9851));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9857));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9861));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8179));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8184));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8188));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8444));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8459));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8602));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8606));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8610));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8613));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8032));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8507));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(8501));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9918));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9922));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9924));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9912));

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9915));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 12, 16, 50, 12, 710, DateTimeKind.Unspecified).AddTicks(9798));
        }
    }
}
