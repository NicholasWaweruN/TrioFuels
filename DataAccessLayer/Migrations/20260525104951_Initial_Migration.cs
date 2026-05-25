using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AfricasTalkingCallback",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhoneNumber = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    MessageId = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    NetworkCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    FailureReason = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AfricasTalkingCallback", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllCredits",
                columns: table => new
                {
                    CustomerName = table.Column<string>(type: "text", nullable: false),
                    VehicleRegistrationNumber = table.Column<string>(type: "text", nullable: false),
                    Credit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TransactionReference = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ApiPermisions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "character varying(140)", unicode: false, maxLength: 140, nullable: false),
                    ApiPermission = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiPermisions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApiPermission = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    MiddName = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PayrollNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    UserCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    ModifiedBy = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    PasswordLastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AccessApps = table.Column<string>(type: "character varying(4)", unicode: false, maxLength: 4, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", unicode: false, maxLength: 15, nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", unicode: false, maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", unicode: false, maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", unicode: false, maxLength: 256, nullable: false),
                    StationCode = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    UserType = table.Column<int>(type: "integer", nullable: false),
                    DepartmentCode = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BulkMessageLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Sender = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    RecipientNumber = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    StatusCode = table.Column<string>(type: "text", nullable: false),
                    Cost = table.Column<string>(type: "text", nullable: false),
                    MessageId = table.Column<string>(type: "text", nullable: false),
                    BatchNumber = table.Column<string>(type: "text", nullable: false),
                    DeliveryStatus = table.Column<string>(type: "text", nullable: false),
                    FailureReason = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulkMessageLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Codegenerators",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Seed = table.Column<int>(type: "integer", nullable: false),
                    NextNumber = table.Column<int>(type: "integer", nullable: false),
                    Prefix = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    Suffix = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    TypeName = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    Length = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Codegenerators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    CouponId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CouponCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", precision: 18, scale: 2, nullable: false),
                    PointsToRedeem = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.CouponId);
                });

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

            migrationBuilder.CreateTable(
                name: "Customer_Complains",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VehicleCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    CustomerCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    ComplainCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    ComplainDescription = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer_Complains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerFunds",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    SystemReference = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    Credit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Debit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    UserReference = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    Narration = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerFunds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    CustomerPhone = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    CustomerEmail = table.Column<string>(type: "character varying(70)", unicode: false, maxLength: 70, nullable: false),
                    OrganisationCode = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    CustomerCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    IdentificationNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    KRAPin = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CreditLimit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Receive_Receipts = table.Column<bool>(type: "boolean", nullable: false),
                    Receive_Statements = table.Column<bool>(type: "boolean", nullable: false),
                    IsCreditCustomer = table.Column<bool>(type: "boolean", nullable: false),
                    BaseLoyaltyPoints = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VehicleCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    TransactionReference = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    Credit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Debit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    UserReference = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    Narration = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    TopUpType = table.Column<int>(type: "integer", nullable: false),
                    Source = table.Column<int>(type: "integer", nullable: false),
                    BatchNumber = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTransactionSummary",
                columns: table => new
                {
                    TotalCredit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalDebit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    OpeningBalance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ClosingBalance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    VehicleRegistrationNumber = table.Column<string>(type: "text", nullable: false),
                    CustomerName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "DispenserAssignments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DispenserCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    StationCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    AttedantUserCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    AssignedBy = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    DateAssigned = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispenserAssignments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dispensers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StationCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    DispenserName = table.Column<string>(type: "character varying(40)", unicode: false, maxLength: 40, nullable: false),
                    DispenserCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    TillNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PetroleumCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    StorageLocation = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispensers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    To = table.Column<string>(type: "character varying(1000)", unicode: false, maxLength: 1000, nullable: false),
                    ToCC = table.Column<string>(type: "character varying(1000)", unicode: false, maxLength: 1000, nullable: false),
                    From = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    ReportCode = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    NotificationName = table.Column<string>(type: "character varying(40)", unicode: false, maxLength: 40, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorTrails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ErrorCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    ErrorMessage = table.Column<string>(type: "character varying(1000)", unicode: false, maxLength: 1000, nullable: false),
                    Method = table.Column<string>(type: "character varying(1000)", unicode: false, maxLength: 1000, nullable: false),
                    StackTrace = table.Column<string>(type: "character varying(1000)", unicode: false, maxLength: 1000, nullable: false),
                    InnerErrorMessage = table.Column<string>(type: "character varying(1000)", unicode: false, maxLength: 1000, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorTrails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FailedTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegNo = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FailedTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarageTransactionDto",
                columns: table => new
                {
                    TransactionId = table.Column<string>(type: "text", nullable: false),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    VehicleRegistrationNumber = table.Column<string>(type: "text", nullable: false),
                    ItemCode = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    NetAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    TillNumber = table.Column<string>(type: "text", nullable: false),
                    SalesDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MpesaReference = table.Column<string>(type: "text", nullable: false),
                    StationName = table.Column<string>(type: "text", nullable: false),
                    SalesAgent = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "GasPriceAuthorizedPrice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VehicleCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ProductCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AuthorizedPricePerLitre = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Approver = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateApproved = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GasPriceAuthorizedPrice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntValue",
                columns: table => new
                {
                    Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "LoyaltySubscriptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubscriptionId = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    VehicleCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CouponId = table.Column<int>(type: "integer", nullable: false),
                    RewardPoints = table.Column<int>(type: "integer", nullable: false),
                    CurrentPoints = table.Column<int>(type: "integer", nullable: false),
                    IsRewardClaimed = table.Column<bool>(type: "boolean", nullable: false),
                    OtpCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    OtpSentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RewardClaimedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SaleIds = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltySubscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    MessageId = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Cost = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "character varying(300)", unicode: false, maxLength: 300, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovedTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShiftNumber = table.Column<string>(type: "text", unicode: false, nullable: false),
                    NozzleCode = table.Column<string>(type: "text", unicode: false, nullable: false),
                    DispenserCode = table.Column<string>(type: "text", unicode: false, nullable: false),
                    StationCode = table.Column<string>(type: "text", unicode: false, nullable: false),
                    VehicleCode = table.Column<string>(type: "text", unicode: false, nullable: false),
                    QuantityCredit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    QuantityDebit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    AmountCredit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    AmountDebit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SaleId = table.Column<string>(type: "text", unicode: false, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentTypeCode = table.Column<int>(type: "integer", nullable: false),
                    IsReversed = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovedTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MpesaC2bPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionType = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    TransID = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    TransTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TransAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    BusinessShortCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    MSISDN = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    MiddName = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    OrgAccountBalance = table.Column<decimal>(type: "numeric(18,2)", unicode: false, maxLength: 20, precision: 18, scale: 2, nullable: false),
                    DateTimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UsageBalance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MpesaC2bPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MpesaTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionType = table.Column<string>(type: "text", nullable: false),
                    TransID = table.Column<string>(type: "text", nullable: false),
                    TransTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TransAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    BusinessShortCode = table.Column<string>(type: "text", nullable: false),
                    TillNumber = table.Column<string>(type: "text", nullable: false),
                    TillName = table.Column<string>(type: "text", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    CheckoutRequestID = table.Column<string>(type: "text", nullable: true),
                    MerchantRequestID = table.Column<string>(type: "text", nullable: true),
                    MpesaReceiptNumber = table.Column<string>(type: "text", nullable: false),
                    MSISDN = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    MiddName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    OrgAccountBalance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    UsageBalance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DateTimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MpesaTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nozzles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DispenserCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    NozzleName = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    NozzleCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nozzles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganisationName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    OrganisationCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    OrganisationPhone = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    OrganisationEmail = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    OrganisationType = table.Column<int>(type: "integer", unicode: false, maxLength: 10, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationTypes",
                columns: table => new
                {
                    OrganisationTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationTypes", x => x.OrganisationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "OtogasJobs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobName = table.Column<string>(type: "text", nullable: false),
                    JobCode = table.Column<string>(type: "text", nullable: false),
                    JobStatus = table.Column<int>(type: "integer", nullable: false),
                    LastRun = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HasRun = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtogasJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtopaySales",
                columns: table => new
                {
                    SaleId = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    StationName = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    DispenserName = table.Column<string>(type: "character varying(5)", unicode: false, maxLength: 5, nullable: false),
                    NozzleName = table.Column<string>(type: "character varying(5)", unicode: false, maxLength: 5, nullable: false),
                    AttendantName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Litres = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SalesDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentType = table.Column<string>(type: "character varying(40)", unicode: false, maxLength: 40, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TillNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    Vehicle = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    StorageLocation = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    ProductName = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    TransId = table.Column<string>(type: "character varying(40)", unicode: false, maxLength: 40, nullable: false),
                    CustomerName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    ShiftNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    Terminal = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    RunningBalance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Otps",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OTPType = table.Column<int>(type: "integer", nullable: false),
                    OTPCode = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    OTPStatus = table.Column<bool>(type: "boolean", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtpTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OTPType = table.Column<int>(type: "integer", unicode: false, maxLength: 30, nullable: false),
                    OTPDescription = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PasswordHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PasswordHash = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionAmountDebit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SaleId = table.Column<string>(type: "text", unicode: false, nullable: false),
                    PaymentRefrence = table.Column<string>(type: "text", unicode: false, nullable: false),
                    TransactionAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentTypeName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    PaymentTypeId = table.Column<int>(type: "integer", unicode: false, maxLength: 2, nullable: false),
                    IsAppUsed = table.Column<bool>(type: "boolean", nullable: false),
                    ProcessType = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    HasValue = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PdaDevices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    DeviceName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    DeviceModel = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    DeviceSerialNumber = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    DeviceIMEI = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    DeviceMacAddress = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    DispenserCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdaDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personal_Wallet_Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WalletId = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    IdentificationNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    Credit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Receive_Receipts = table.Column<bool>(type: "boolean", nullable: false),
                    Receive_Statements = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    HashedPin = table.Column<string>(type: "character varying(2000)", unicode: false, maxLength: 2000, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal_Wallet_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PetroleumProducts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PetroleumCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    PetroleumName = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetroleumProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceApproval",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApprovalCode = table.Column<string>(type: "character varying(4)", unicode: false, maxLength: 4, nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ProposedPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    NumberPlate = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Initiator = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Approver = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    IsApprovalExecuted = table.Column<bool>(type: "boolean", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ShiftNumber = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceApproval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceApprovers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApprovalUserCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    AppoverName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceApprovers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    StationCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    DispenserCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductCode = table.Column<string>(type: "character varying(5)", unicode: false, maxLength: 5, nullable: false),
                    StationCode = table.Column<string>(type: "character varying(5)", unicode: false, maxLength: 5, nullable: false),
                    ScheduledPrice = table.Column<decimal>(type: "numeric(6,2)", precision: 18, scale: 2, nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "numeric(6,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Processed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductCode = table.Column<string>(type: "character varying(4)", unicode: false, maxLength: 4, nullable: false),
                    ProductName = table.Column<string>(type: "character varying(40)", unicode: false, maxLength: 40, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProtoApps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", unicode: false, maxLength: 50, nullable: false),
                    AppsName = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    AppsCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    UserCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CurrentVersion = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProtoApps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuantityTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShiftNumber = table.Column<string>(type: "character varying(25)", unicode: false, maxLength: 25, nullable: false),
                    NozzleCode = table.Column<string>(type: "character varying(25)", unicode: false, maxLength: 25, nullable: false),
                    DispenserCode = table.Column<string>(type: "text", unicode: false, nullable: false),
                    StationCode = table.Column<string>(type: "text", unicode: false, nullable: false),
                    VehicleCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    QuantityCredit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    QuantityDebit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    AmountCredit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    AmountDebit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Vat_Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SaleId = table.Column<string>(type: "text", unicode: false, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentTypeCode = table.Column<int>(type: "integer", nullable: false),
                    IsReversed = table.Column<bool>(type: "boolean", nullable: false),
                    RoundedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OtpUsed = table.Column<string>(type: "text", unicode: false, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ReportName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RescheduledMessages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    Message = table.Column<string>(type: "character varying(600)", unicode: false, maxLength: 600, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateSent = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ScheduledSendingdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsSent = table.Column<bool>(type: "boolean", nullable: false),
                    SenderId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RescheduledMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    RoleCode = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleAndPermisions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleCode = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    PermissionCode = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAndPermisions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleToUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleCode = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    UserCode = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleToUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoyaltyPoints",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerCode = table.Column<string>(type: "text", nullable: false),
                    Litres = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PointsCredit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PointsDebit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SaleId = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoyaltyPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesReportRow",
                columns: table => new
                {
                    SaleId = table.Column<string>(type: "text", nullable: true),
                    SalesDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TransId = table.Column<string>(type: "text", nullable: true),
                    StationName = table.Column<string>(type: "text", nullable: true),
                    AttendantName = table.Column<string>(type: "text", nullable: true),
                    CustomerName = table.Column<string>(type: "text", nullable: true),
                    TillNumber = table.Column<string>(type: "text", nullable: true),
                    ShiftNumber = table.Column<string>(type: "text", nullable: true),
                    Vehicle = table.Column<string>(type: "text", nullable: true),
                    ProductName = table.Column<string>(type: "text", nullable: true),
                    PaymentType = table.Column<string>(type: "text", nullable: true),
                    Litres = table.Column<decimal>(type: "numeric", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: true),
                    Discount = table.Column<decimal>(type: "numeric", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    DispenserName = table.Column<string>(type: "text", nullable: true),
                    NozzleName = table.Column<string>(type: "text", nullable: true),
                    StorageLocation = table.Column<string>(type: "text", nullable: true),
                    RunningBalance = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SalesTransaction",
                columns: table => new
                {
                    ShiftNumber = table.Column<string>(type: "text", nullable: false),
                    SaleId = table.Column<string>(type: "text", nullable: false),
                    StationName = table.Column<string>(type: "text", nullable: false),
                    Attendant_Name = table.Column<string>(type: "text", nullable: false),
                    Litres = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SalesDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentType = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TillNumber = table.Column<int>(type: "integer", nullable: false),
                    Vehicle = table.Column<string>(type: "text", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    CustomerName = table.Column<string>(type: "text", nullable: false),
                    Transid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SalesTransactions",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TransactionId = table.Column<string>(type: "character varying(8000)", unicode: false, maxLength: 8000, nullable: false),
                    OutLet = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Attendant = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    CustomerName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    TillNumber = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    TerminalName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    ShiftNumber = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Product = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    PaymentType = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    RunningReading = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    AmountPaid = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DispenserName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    NozzleName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    StorageLocation = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    OriginalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesTransactions", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SetupCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    App_VersionCode = table.Column<string>(type: "text", nullable: false),
                    PasswordExpiryDays = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShiftNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    UserCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    DispenserCode = table.Column<string>(type: "character varying(4)", unicode: false, maxLength: 4, nullable: false),
                    ShiftStatus = table.Column<int>(type: "integer", nullable: false),
                    ShiftStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ShiftEndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsEmailSent = table.Column<bool>(type: "boolean", nullable: false),
                    EmailConversationId = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    IsReplySent = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhoneNumber = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    Message = table.Column<string>(type: "character varying(500)", unicode: false, maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmsCallbacks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", unicode: false, maxLength: 15, nullable: false),
                    MessageId = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    NetworkCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    FailureReason = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsCallbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StationName = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    StationCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    StationAddress = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    LocationId = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockTakes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShiftNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    NozzleCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    ClosingReading = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    OpeningReading = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TakeType = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTakes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockTakeScheduler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DispenserCode = table.Column<string>(type: "text", nullable: false),
                    IsTaken = table.Column<bool>(type: "boolean", nullable: false),
                    TakeDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTakeScheduler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockTakeSummaries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShiftNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    NozzleCode = table.Column<string>(type: "character varying(4)", unicode: false, maxLength: 4, nullable: false),
                    OpeningReading = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ExpectedOpeningReading = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    OpeningVariance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ClosingReading = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    QuantitySold = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ExpectedClosingReading = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ClosingVariance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    VarianceStatus = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTakeSummaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StationCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    TankName = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    TankCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TankSizes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TankCapacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TankSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TankTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StationCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    TankCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    OrderId = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    SaleId = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    DeliveredQuantity = table.Column<double>(type: "double precision", nullable: false),
                    QuantitySold = table.Column<double>(type: "double precision", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TankTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TankTransactionsSummaries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StationCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    TankCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    OrderId = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    DeliveredQuantity = table.Column<double>(type: "double precision", nullable: false),
                    QuantitySold = table.Column<double>(type: "double precision", nullable: false),
                    ExpectedQuantity = table.Column<double>(type: "double precision", nullable: false),
                    ActualQuantity = table.Column<double>(type: "double precision", nullable: false),
                    Variance = table.Column<double>(type: "double precision", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TankTransactionsSummaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Targets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StationCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    TargetAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalSales = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Targets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tills",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TillName = table.Column<string>(type: "character varying(40)", unicode: false, maxLength: 40, nullable: false),
                    TillNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    StoreNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    LastFetch = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OffsetValue = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopUpTypes",
                columns: table => new
                {
                    TopUpType = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TopUpDescription = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopUpTypes", x => x.TopUpType);
                });

            migrationBuilder.CreateTable(
                name: "TotalizerReadings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NozzlesCode = table.Column<string>(type: "text", nullable: false),
                    Reading = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalizerReadings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionReceipts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    VehicleReg = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    Quantity = table.Column<double>(type: "double precision", nullable: false),
                    PricePerLitre = table.Column<double>(type: "double precision", nullable: false),
                    Vat_Amount = table.Column<double>(type: "double precision", nullable: false),
                    PaymentMethod = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    TotalAmount = table.Column<double>(type: "double precision", nullable: false),
                    ReceiptNumber = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Duplicate = table.Column<int>(type: "integer", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    ServedBy = table.Column<string>(type: "character varying(40)", unicode: false, maxLength: 40, nullable: false),
                    StationName = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionReceipts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransFeredVehicles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NewCustomerCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    TransFerDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CustomerCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    VehicleCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    VehicleRegistrationNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    VehicleMake = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    VehicleModel = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    TankCapacity = table.Column<int>(type: "integer", nullable: false),
                    ProductCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    ConversionStation = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    ConversionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    NFC_CardNumber = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    TransactionPIN = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransFeredVehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsageBalanceDto",
                columns: table => new
                {
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    storeNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "UserApps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", unicode: false, maxLength: 50, nullable: false),
                    AppsCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    UserCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTrails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    UserName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", unicode: false, maxLength: 1000, nullable: false),
                    ActionType = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    ShiftNumber = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValueDto",
                columns: table => new
                {
                    Value = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    VehicleCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    VehicleRegistrationNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    VehicleMake = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    VehicleModel = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    TankCapacity = table.Column<int>(type: "integer", nullable: false),
                    ProductCode = table.Column<string>(type: "character varying(5)", unicode: false, maxLength: 5, nullable: false),
                    ConversionStation = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    ConversionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    NFC_CardNumber = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    TransactionPIN = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    PhoneNumber2 = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    CreditLimit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TelematicSerialNumber = table.Column<string>(type: "text", nullable: false),
                    IsTelematicInstalled = table.Column<bool>(type: "boolean", nullable: false),
                    TelematicInstallationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RoyaltyPointPerLitre = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleStatusTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleStatusTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    VoucherNo = table.Column<string>(type: "text", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    VehicleCode = table.Column<string>(type: "text", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Walk_In_Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VehicleRegistrationNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    VehicleMake = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    ProductCode = table.Column<string>(type: "character varying(5)", unicode: false, maxLength: 5, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    KitType = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walk_In_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallet_Transactions_Personal",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WalletId = table.Column<string>(type: "text", nullable: false),
                    TransactionCode = table.Column<string>(type: "text", nullable: false),
                    Credit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Debit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TransactionType = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    SaleId = table.Column<string>(type: "text", nullable: false),
                    VehicleCode = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet_Transactions_Personal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessApps", "AccessFailedCount", "ConcurrencyStamp", "CreatedBy", "DateCreated", "DateModified", "DepartmentCode", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastLoginDate", "LastName", "LockoutEnabled", "LockoutEnd", "MiddName", "ModifiedBy", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PasswordLastUpdated", "PayrollNumber", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StationCode", "TwoFactorEnabled", "UserCode", "UserName", "UserType" },
                values: new object[] { "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a", "", 0, "6590f25c-d2ee-4c1d-8ea7-0a92bb5adbd9", "", new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8692), new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8691), "", "nicholas@fuelflo.com", true, "Admin", true, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8693), "Fuel Flow", false, null, "", "", "NICHOLAS@FUELFLOW.COM", null, "AQAAAAIAAYagAAAAEE6B8ismqB4S3ovK4di5qY7F2cwEDfBiowzxCzmmnRa1w0kuyR/ADNBR4B6D0h9sew==", new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8685), "", "+254715821303", true, "ec3ae2b3-2627-432c-aa8e-bd37724a683e", "", false, "99999", "", 1 });

            migrationBuilder.InsertData(
                table: "Codegenerators",
                columns: new[] { "Id", "DateCreated", "Length", "NextNumber", "Prefix", "Seed", "Suffix", "TypeName", "UserCode" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(7911), 5, 0, "", 1, "", "UserCode", "00001" },
                    { 2L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(7917), 2, 0, "D", 1, "", "DispenserCode", "00001" },
                    { 3L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(7920), 2, 0, "N", 1, "", "Nozzlecode", "00001" },
                    { 4L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(7923), 3, 0, "S", 1, "", "StationCode", "00001" },
                    { 5L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(7926), 5, 10000, "", 1, "", "CustomerCode", "00001" },
                    { 6L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(7928), 5, 10000, "", 1, "", "BULCUST", "00001" },
                    { 7L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(7931), 7, 0, "", 1, "", "PLANID", "00001" },
                    { 8L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(7934), 2, 0, "T", 1, "", "TANKID", "00001" },
                    { 9L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(7937), 5, 0, "", 1, "", "TILLID", "00001" },
                    { 10L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(7939), 4, 0, "P", 1, "", "PDAID", "00001" },
                    { 11L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(7942), 7, 0, "", 1, "", "VEHICLEID", "00001" },
                    { 14L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(7945), 4, 0, "PD", 1, "", "pdadevice", "00001" },
                    { 15L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(7948), 2, 0, "", 1, "", "productCode", "00001" },
                    { 16L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(7951), 5, 0, "", 1, "", "WalkInCustomer", "00001" },
                    { 17L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(7953), 5, 1, "", 1, "", "VehicleCode", "00001" }
                });

            migrationBuilder.InsertData(
                table: "DispenserAssignments",
                columns: new[] { "Id", "AssignedBy", "AttedantUserCode", "DateAssigned", "DispenserCode", "IsActive", "StationCode" },
                values: new object[] { 1L, "99999", "99999", new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(9014), "D01", true, "S001" });

            migrationBuilder.InsertData(
                table: "Dispensers",
                columns: new[] { "Id", "DateCreated", "DispenserCode", "DispenserName", "IsActive", "PetroleumCode", "StationCode", "StorageLocation", "TillNumber", "UserCode" },
                values: new object[] { 1L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8843), "D01", "D1", true, "01", "S001", "kenya", "078678", "00001" });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "DateCreated", "From", "NotificationName", "ReportCode", "To", "ToCC", "UserCode" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Variance Report", "001", "", "", "99999" },
                    { 2L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Sales Report", "002", "", "", "99999" },
                    { 3L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Monthly Sales Report", "003", "", "", "99999" },
                    { 4L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Cumulative Variance Report", "004", "", "", "99999" },
                    { 5L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Mpesa Usage Report", "005", "", "", "99999" }
                });

            migrationBuilder.InsertData(
                table: "Nozzles",
                columns: new[] { "Id", "DateCreated", "DispenserCode", "IsActive", "NozzleCode", "NozzleName", "UserCode" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8969), "D01", true, "N01", "N01", "00001" },
                    { 2L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8972), "D01", true, "N02", "N02", "00001" }
                });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "DateCreated", "HasValue", "IsAppUsed", "PaymentTypeId", "PaymentTypeName", "ProcessType", "UserCode" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8341), true, true, 0, "Mpesa", "", "00001" },
                    { 2L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8345), true, true, 1, "Wallet", "", "00001" },
                    { 4L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8347), true, false, 3, "Operational_Loss", "", "00001" },
                    { 6L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8349), true, false, 5, "Employee_Mpesa_Payments", "", "00001" },
                    { 7L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8351), true, false, 6, "Insurance", "", "00001" },
                    { 8L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8353), true, false, 7, "Voucher", "", "00001" },
                    { 9L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8355), true, false, 8, "Calibration", "", "00001" },
                    { 10L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8357), true, false, 9, "Compesation_Fuel", "", "00001" },
                    { 11L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8360), true, false, 10, "BatchVoucher", "", "00001" },
                    { 13L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8363), true, true, 12, "Cash", "", "00001" },
                    { 14L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8366), true, true, 13, "Credit", "", "00001" },
                    { 15L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8368), true, true, 14, "Loyalty", "", "00001" }
                });

            migrationBuilder.InsertData(
                table: "PdaDevices",
                columns: new[] { "Id", "DateCreated", "DeviceCode", "DeviceIMEI", "DeviceMacAddress", "DeviceModel", "DeviceName", "DeviceSerialNumber", "DispenserCode", "IsActive", "UserCode" },
                values: new object[] { 1L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8931), "1234567890", "1234567890", "1234567890", "1234567890", "Test PDA", "1234567890", "D01", true, "99999" });

            migrationBuilder.InsertData(
                table: "PetroleumProducts",
                columns: new[] { "Id", "DateCreated", "PetroleumCode", "PetroleumName", "UserCode" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(9203), "01", "Autogas", "99999" },
                    { 2L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(9205), "02", "Petrol", "99999" },
                    { 3L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(9208), "03", "Diesel", "99999" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateCreated", "IsActive", "ProductCode", "ProductName", "UserCode" },
                values: new object[] { -1L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8895), true, "02", "Diesel", "000001" });

            migrationBuilder.InsertData(
                table: "ProtoApps",
                columns: new[] { "Id", "AppsCode", "AppsName", "CurrentVersion", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("4be00159-2585-43b8-a680-75b745ff1775"), "03", "Fuel Flow DashBoard", "", new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8489), "" },
                    { new Guid("4cc03bbd-51d1-4dbc-8bb3-7f18e503e053"), "02", "Bulk App", "", new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8485), "" },
                    { new Guid("85e92a4a-d946-4c6e-8859-e645e2f2fc06"), "04", "Fuel Flow App", "", new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8492), "" },
                    { new Guid("ee835650-32f3-4718-9606-38fc2b7987d2"), "01", "Bulk DashBoard", "", new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8480), "" }
                });

            migrationBuilder.InsertData(
                table: "QuantityTransactions",
                columns: new[] { "Id", "AmountCredit", "AmountDebit", "DateCreated", "Discount", "DispenserCode", "IsReversed", "NozzleCode", "OtpUsed", "PaymentTypeCode", "Price", "QuantityCredit", "QuantityDebit", "SaleId", "ShiftNumber", "StationCode", "UserCode", "Vat_Amount", "VehicleCode" },
                values: new object[,]
                {
                    { 1L, 0m, 0m, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(9096), 0m, "D01", false, "N01", "", 99, 0m, 50m, 0m, "", "", "S001", "99999", 0m, "" },
                    { 2L, 0m, 0m, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(9110), 0m, "D01", false, "N02", "", 99, 0m, 50m, 0m, "", "", "S001", "99999", 0m, "" }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "ReportName" },
                values: new object[,]
                {
                    { "001", "Variance Report" },
                    { "002", "Sales Report" },
                    { "003", "Monthly Sales Report" },
                    { "004", "Cumulative Variance Report" },
                    { "005", "Mpesa Usage Report" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "DateCreated", "RoleCode", "RoleName", "UserCode" },
                values: new object[] { 1L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(9259), "001", "Administrator", "99999" });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "DateCreated", "IsActive", "LocationId", "StationAddress", "StationCode", "StationName", "UserCode" },
                values: new object[] { 1L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8742), true, "Test Station", "Test Station", "S001", "TEST STATION", "00001" });

            migrationBuilder.InsertData(
                table: "StockTakes",
                columns: new[] { "Id", "ClosingReading", "DateCreated", "NozzleCode", "OpeningReading", "ShiftNumber", "TakeType", "UserCode" },
                values: new object[,]
                {
                    { -1L, 0m, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(9160), "N02", 50m, "", 99, "99999" },
                    { 1L, 0m, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(9153), "N01", 50m, "", 99, "99999" }
                });

            migrationBuilder.InsertData(
                table: "Tills",
                columns: new[] { "Id", "DateCreated", "IsActive", "LastFetch", "OffsetValue", "StoreNumber", "TillName", "TillNumber", "UserCode" },
                values: new object[] { 1L, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(9053), true, new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(9054), 0, "078678", "Test Till", "078678", "99999" });

            migrationBuilder.InsertData(
                table: "UserApps",
                columns: new[] { "Id", "AppsCode", "DateCreated", "UserCode" },
                values: new object[,]
                {
                    { new Guid("149618c3-e575-429f-986a-622661c51868"), "04", new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8795), "99999" },
                    { new Guid("5b6b2175-21b6-4e82-8884-9c292d71503b"), "01", new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8799), "99999" },
                    { new Guid("83e53f12-59f6-44d4-9375-836001ac4bb7"), "02", new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8807), "99999" },
                    { new Guid("e53ff251-c568-4c6b-bf27-ef87b1312cf0"), "03", new DateTime(2026, 5, 25, 10, 49, 49, 946, DateTimeKind.Utc).AddTicks(8791), "99999" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AfricasTalkingCallback_MessageId",
                table: "AfricasTalkingCallback",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiPermisions_ApiName",
                table: "ApiPermisions",
                column: "ApiPermission",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Codegenerator_TypeName",
                table: "Codegenerators",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Complains_CustomerCode",
                table: "Customer_Complains",
                column: "CustomerCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerCode",
                table: "Customers",
                column: "CustomerCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTransactions_DateCreated",
                table: "CustomerTransactions",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTransactions_VehicleCode",
                table: "CustomerTransactions",
                column: "VehicleCode");

            migrationBuilder.CreateIndex(
                name: "IX_DispenserAssignment_AttedantUserCode",
                table: "DispenserAssignments",
                column: "AttedantUserCode");

            migrationBuilder.CreateIndex(
                name: "IX_DispenserAssignment_StationId",
                table: "DispenserAssignments",
                column: "DispenserCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dispenser_DispenserCode",
                table: "Dispensers",
                column: "DispenserCode");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_ReportCode",
                table: "Emails",
                column: "ReportCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Codegenerator_RegNo",
                table: "FailedTransactions",
                column: "RegNo");

            migrationBuilder.CreateIndex(
                name: "IX_MessageDetails_MessageId",
                table: "MessageDetails",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MpesaC2bPayments_TransactionId",
                table: "MpesaC2bPayments",
                column: "TransID");

            migrationBuilder.CreateIndex(
                name: "IX_MpesaTransaction_TransactionId",
                table: "MpesaTransactions",
                column: "TransID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nozzle_NozzleCode",
                table: "Nozzles",
                column: "NozzleCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_OrganisationCode",
                table: "Organisations",
                column: "OrganisationCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Otps_OTPCode",
                table: "Otps",
                column: "OTPCode");

            migrationBuilder.CreateIndex(
                name: "IX_OtpTypes_OTPType",
                table: "OtpTypes",
                column: "OTPType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_DateCreated",
                table: "PaymentTransactions",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_PaymentRefrence",
                table: "PaymentTransactions",
                column: "PaymentRefrence");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_SaleId",
                table: "PaymentTransactions",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentType_PaymentTypeId",
                table: "PaymentTypes",
                column: "PaymentTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PdaDevices_DeviceIMEI",
                table: "PdaDevices",
                column: "DeviceIMEI");

            migrationBuilder.CreateIndex(
                name: "IX_PdaDevices_DispenserCode",
                table: "PdaDevices",
                column: "DispenserCode");

            migrationBuilder.CreateIndex(
                name: "IX_Price_ProductId",
                table: "Prices",
                column: "ProductCode");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCode",
                table: "Products",
                column: "ProductCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProtoApps_AppsCode",
                table: "ProtoApps",
                column: "AppsCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuantityTransactions_DateCreated",
                table: "QuantityTransactions",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityTransactions_NozzleCode",
                table: "QuantityTransactions",
                column: "NozzleCode");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityTransactions_SaleId",
                table: "QuantityTransactions",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityTransactions_ShiftNumber",
                table: "QuantityTransactions",
                column: "ShiftNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Role_RoleCode",
                table: "Role",
                column: "RoleCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleAndPermisions_PermissionCode",
                table: "RoleAndPermisions",
                column: "PermissionCode");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAndPermisions_RoleId",
                table: "RoleAndPermisions",
                column: "RoleCode");

            migrationBuilder.CreateIndex(
                name: "IX_FailedTransactions_TransactionId",
                table: "SalesTransactions",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_DispenserCode",
                table: "Shifts",
                column: "DispenserCode");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_ShiftNumber",
                table: "Shifts",
                column: "ShiftNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sms_PhoneNumber",
                table: "Sms",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_GasStation_StationCode",
                table: "Stations",
                column: "StationCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockTake_ShiftId",
                table: "StockTakes",
                column: "ShiftNumber");

            migrationBuilder.CreateIndex(
                name: "IX_StockTakeSummary_NozzleCode",
                table: "StockTakeSummaries",
                column: "NozzleCode");

            migrationBuilder.CreateIndex(
                name: "IX_StockTakeSummary_ShiftNumber",
                table: "StockTakeSummaries",
                column: "ShiftNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Tank_TankCode",
                table: "Tank",
                column: "TankCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TankSizes_SizeName",
                table: "TankSizes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TankTransactions_TankCode",
                table: "TankTransactions",
                column: "TankCode");

            migrationBuilder.CreateIndex(
                name: "IX_TankTransactionsSummary_TankId",
                table: "TankTransactionsSummaries",
                column: "TankCode");

            migrationBuilder.CreateIndex(
                name: "IX_Tills_StoreNumber",
                table: "Tills",
                column: "StoreNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Tills_TillId",
                table: "Tills",
                column: "TillNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransFeredVehicles_VehicleCode",
                table: "TransFeredVehicles",
                column: "VehicleCode");

            migrationBuilder.CreateIndex(
                name: "IX_UserApps_UserId",
                table: "UserApps",
                column: "AppsCode");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrail_UserId",
                table: "UserTrails",
                column: "UserCode");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_CustomerCode",
                table: "Vehicles",
                column: "CustomerCode");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_VehicleCode",
                table: "Vehicles",
                column: "VehicleCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleStatusTypes_StatusName",
                table: "VehicleStatusTypes",
                column: "StatusCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AfricasTalkingCallback");

            migrationBuilder.DropTable(
                name: "AllCredits");

            migrationBuilder.DropTable(
                name: "ApiPermisions");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BulkMessageLogs");

            migrationBuilder.DropTable(
                name: "Codegenerators");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "CreditTransactions");

            migrationBuilder.DropTable(
                name: "Customer_Complains");

            migrationBuilder.DropTable(
                name: "CustomerFunds");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CustomerTransactions");

            migrationBuilder.DropTable(
                name: "CustomerTransactionSummary");

            migrationBuilder.DropTable(
                name: "DispenserAssignments");

            migrationBuilder.DropTable(
                name: "Dispensers");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "ErrorTrails");

            migrationBuilder.DropTable(
                name: "FailedTransactions");

            migrationBuilder.DropTable(
                name: "GarageTransactionDto");

            migrationBuilder.DropTable(
                name: "GasPriceAuthorizedPrice");

            migrationBuilder.DropTable(
                name: "IntValue");

            migrationBuilder.DropTable(
                name: "LoyaltySubscriptions");

            migrationBuilder.DropTable(
                name: "MessageDetails");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "MovedTransactions");

            migrationBuilder.DropTable(
                name: "MpesaC2bPayments");

            migrationBuilder.DropTable(
                name: "MpesaTransactions");

            migrationBuilder.DropTable(
                name: "Nozzles");

            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.DropTable(
                name: "OrganisationTypes");

            migrationBuilder.DropTable(
                name: "OtogasJobs");

            migrationBuilder.DropTable(
                name: "OtopaySales");

            migrationBuilder.DropTable(
                name: "Otps");

            migrationBuilder.DropTable(
                name: "OtpTypes");

            migrationBuilder.DropTable(
                name: "PasswordHistory");

            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "PdaDevices");

            migrationBuilder.DropTable(
                name: "Personal_Wallet_Customers");

            migrationBuilder.DropTable(
                name: "PetroleumProducts");

            migrationBuilder.DropTable(
                name: "PriceApproval");

            migrationBuilder.DropTable(
                name: "PriceApprovers");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "PriceSchedules");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProtoApps");

            migrationBuilder.DropTable(
                name: "QuantityTransactions");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "RescheduledMessages");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "RoleAndPermisions");

            migrationBuilder.DropTable(
                name: "RoleToUser");

            migrationBuilder.DropTable(
                name: "RoyaltyPoints");

            migrationBuilder.DropTable(
                name: "SalesReportRow");

            migrationBuilder.DropTable(
                name: "SalesTransaction");

            migrationBuilder.DropTable(
                name: "SalesTransactions");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Setup");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Sms");

            migrationBuilder.DropTable(
                name: "SmsCallbacks");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "StockTakes");

            migrationBuilder.DropTable(
                name: "StockTakeScheduler");

            migrationBuilder.DropTable(
                name: "StockTakeSummaries");

            migrationBuilder.DropTable(
                name: "Tank");

            migrationBuilder.DropTable(
                name: "TankSizes");

            migrationBuilder.DropTable(
                name: "TankTransactions");

            migrationBuilder.DropTable(
                name: "TankTransactionsSummaries");

            migrationBuilder.DropTable(
                name: "Targets");

            migrationBuilder.DropTable(
                name: "Tills");

            migrationBuilder.DropTable(
                name: "TopUpTypes");

            migrationBuilder.DropTable(
                name: "TotalizerReadings");

            migrationBuilder.DropTable(
                name: "TransactionReceipts");

            migrationBuilder.DropTable(
                name: "TransFeredVehicles");

            migrationBuilder.DropTable(
                name: "UsageBalanceDto");

            migrationBuilder.DropTable(
                name: "UserApps");

            migrationBuilder.DropTable(
                name: "UserTrails");

            migrationBuilder.DropTable(
                name: "ValueDto");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "VehicleStatusTypes");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "Walk_In_Customers");

            migrationBuilder.DropTable(
                name: "Wallet_Transactions_Personal");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
