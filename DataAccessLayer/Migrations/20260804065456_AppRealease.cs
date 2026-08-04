using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AppRealease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchNumber",
                table: "CustomerTransactions");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "CustomerTransactions");

            migrationBuilder.AddColumn<string>(
                name: "CustomerCode",
                table: "CustomerTransactions",
                type: "character varying(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "PackageId",
                table: "CarWashTransactionItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PackageInstanceId",
                table: "CarWashTransactionItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppReleases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Platform = table.Column<string>(type: "text", nullable: false),
                    VersionCode = table.Column<int>(type: "integer", nullable: false),
                    VersionName = table.Column<string>(type: "text", nullable: false),
                    ApkFileName = table.Column<string>(type: "text", nullable: false),
                    ReleaseNotes = table.Column<string>(type: "text", nullable: true),
                    IsMandatory = table.Column<bool>(type: "boolean", nullable: false),
                    ReleasedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppReleases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarWashPackages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarWashPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarWashPackageItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PackageId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarWashPackageItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarWashPackageItems_CarWashPackages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "CarWashPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarWashPackageItems_CarWashProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "CarWashProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarWashPackagePrices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PackageId = table.Column<long>(type: "bigint", nullable: false),
                    VehicleTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarWashPackagePrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarWashPackagePrices_CarWashPackages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "CarWashPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "dc132277-7941-4249-b795-56e7e651d2c3", new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6044), new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6043), new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6046), new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6037), "9969ffc4-73ea-4138-a085-83ee729a82e1" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5366));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5373));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5377));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5381));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5385));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5389));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5393));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5409));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5413));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(7517));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6383));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6191));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6336));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6341));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5674));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5678));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5681));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5685));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5688));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5702));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5732));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5736));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5740));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5743));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5746));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5750));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5753));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6279));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6285));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6290));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6294));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6621));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6626));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6629));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(7616));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(7621));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(7625));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6234));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6239));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6243));

            migrationBuilder.UpdateData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000005"),
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(5883));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6499));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6513));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6677));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6682));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6685));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6689));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6101));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6562));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6557));

            migrationBuilder.UpdateData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000003"),
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(6157));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 8, 4, 9, 54, 54, 186, DateTimeKind.Unspecified).AddTicks(7562));

            migrationBuilder.CreateIndex(
                name: "IX_CarWashTransactionItems_PackageId",
                table: "CarWashTransactionItems",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_CarWashPackageItems_PackageId",
                table: "CarWashPackageItems",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_CarWashPackageItems_ProductId",
                table: "CarWashPackageItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CarWashPackagePrices_PackageId",
                table: "CarWashPackagePrices",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarWashTransactionItems_CarWashPackages_PackageId",
                table: "CarWashTransactionItems",
                column: "PackageId",
                principalTable: "CarWashPackages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarWashTransactionItems_CarWashPackages_PackageId",
                table: "CarWashTransactionItems");

            migrationBuilder.DropTable(
                name: "AppReleases");

            migrationBuilder.DropTable(
                name: "CarWashPackageItems");

            migrationBuilder.DropTable(
                name: "CarWashPackagePrices");

            migrationBuilder.DropTable(
                name: "CarWashPackages");

            migrationBuilder.DropIndex(
                name: "IX_CarWashTransactionItems_PackageId",
                table: "CarWashTransactionItems");

            migrationBuilder.DropColumn(
                name: "CustomerCode",
                table: "CustomerTransactions");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "CarWashTransactionItems");

            migrationBuilder.DropColumn(
                name: "PackageInstanceId",
                table: "CarWashTransactionItems");

            migrationBuilder.AddColumn<string>(
                name: "BatchNumber",
                table: "CustomerTransactions",
                type: "character varying(30)",
                unicode: false,
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Source",
                table: "CustomerTransactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "LastLoginDate", "PasswordLastUpdated", "SecurityStamp" },
                values: new object[] { "df7ff5f9-20b9-4867-b4f2-8c2f2daa3cf0", new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1659), new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1657), new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1660), new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1654), "d1dde17a-8ad3-4b4f-aab6-e8593c91f5f4" });

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1095));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1101));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1105));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1109));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1112));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1115));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1118));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1123));

            migrationBuilder.UpdateData(
                table: "Codegenerators",
                keyColumn: "Id",
                keyValue: 17L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1127));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2844));

            migrationBuilder.UpdateData(
                table: "DispenserAssignments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateAssigned",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1949));

            migrationBuilder.UpdateData(
                table: "Dispensers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1780));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1900));

            migrationBuilder.UpdateData(
                table: "Nozzles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1905));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1377));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1381));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1384));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1387));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1390));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1393));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1395));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1398));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 11L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1401));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 13L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1404));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1406));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 15L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1409));

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1412));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1853));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1859));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1864));

            migrationBuilder.UpdateData(
                table: "PdaDevices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1867));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2139));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2143));

            migrationBuilder.UpdateData(
                table: "PetroleumProducts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2146));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2924));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2930));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2934));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1815));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1819));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1822));

            migrationBuilder.UpdateData(
                table: "ProtoApps",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000005"),
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1515));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2036));

            migrationBuilder.UpdateData(
                table: "QuantityTransactions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2048));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2178));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2181));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2183));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2186));

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1703));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2091));

            migrationBuilder.UpdateData(
                table: "StockTakes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2086));

            migrationBuilder.UpdateData(
                table: "UserApps",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000003"),
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(1751));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2026, 7, 19, 10, 26, 44, 720, DateTimeKind.Unspecified).AddTicks(2883));
        }
    }
}
