using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ss");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "ss",
                columns: table => new
                {
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Category_Parent",
                        column: x => x.ParentCategoryId,
                        principalSchema: "ss",
                        principalTable: "Category",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "ss",
                columns: table => new
                {
                    CustomerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsWalkIn = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                schema: "ss",
                columns: table => new
                {
                    SupplierId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTermsDays = table.Column<short>(type: "smallint", nullable: false),
                    ConfiguredLeadTimeDays = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "UserProfile",
                schema: "ss",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MustChangePassword = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeactivatedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserProfile_IdentityUser",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "ss",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    PreferredSupplierId = table.Column<long>(type: "bigint", nullable: true),
                    Sku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NormalizedSku = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedBarcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityScale = table.Column<byte>(type: "tinyint", nullable: false),
                    QuantityIncrement = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SellingUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DefaultPurchaseUnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReorderMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManualReorderPoint = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PackQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumOrderQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpiryTracked = table.Column<bool>(type: "bit", nullable: false),
                    ImageAssetReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ActivatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArchivedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category",
                        column: x => x.CategoryId,
                        principalSchema: "ss",
                        principalTable: "Category",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_Product_Supplier",
                        column: x => x.PreferredSupplierId,
                        principalSchema: "ss",
                        principalTable: "Supplier",
                        principalColumn: "SupplierId");
                });

            migrationBuilder.CreateTable(
                name: "AiChatLog",
                schema: "ss",
                columns: table => new
                {
                    AiChatLogId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionRedacted = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerRedacted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetMonth = table.Column<DateOnly>(type: "date", nullable: false),
                    ReservedTokens = table.Column<int>(type: "int", nullable: false),
                    InputTokens = table.Column<int>(type: "int", nullable: false),
                    OutputTokens = table.Column<int>(type: "int", nullable: false),
                    LatencyMilliseconds = table.Column<int>(type: "int", nullable: true),
                    SafeFailureCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetainUntilUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LegalHold = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiChatLog", x => x.AiChatLogId);
                    table.ForeignKey(
                        name: "FK_Chat_User",
                        column: x => x.UserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "BusinessSettings",
                schema: "ss",
                columns: table => new
                {
                    SettingsId = table.Column<byte>(type: "tinyint", nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeZoneId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoicePrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountApprovalPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdjustmentApprovalThreshold = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpiryWarningDays = table.Column<short>(type: "smallint", nullable: false),
                    DeadStockDays = table.Column<short>(type: "smallint", nullable: false),
                    ReviewPeriodDays = table.Column<short>(type: "smallint", nullable: false),
                    SafetyFactor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LlmEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LlmProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LlmModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LlmSecretReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LlmRequestsPerMinute = table.Column<short>(type: "smallint", nullable: false),
                    LlmMonthlyTokenBudget = table.Column<long>(type: "bigint", nullable: false),
                    SmtpEnabled = table.Column<bool>(type: "bit", nullable: false),
                    SmtpHost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmtpPort = table.Column<int>(type: "int", nullable: true),
                    SmtpSecretReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedRecipientsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatRetentionMonths = table.Column<short>(type: "smallint", nullable: false),
                    RetentionPolicyReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetentionApprovedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LegalHold = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessSettings", x => x.SettingsId);
                    table.ForeignKey(
                        name: "FK_Settings_User",
                        column: x => x.UpdatedByUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "IdempotencyRequest",
                schema: "ss",
                columns: table => new
                {
                    RequestId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayloadHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RequestStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResultJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdempotencyRequest", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_Request_User",
                        column: x => x.ActorUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ProductBatch",
                schema: "ss",
                columns: table => new
                {
                    ProductBatchId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    IsUnbatched = table.Column<bool>(type: "bit", nullable: false),
                    BatchNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedBatchNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    FirstReceivedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBatch", x => x.ProductBatchId);
                    table.UniqueConstraint("UQ_Batch_Product", x => new { x.ProductBatchId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Batch_Product",
                        column: x => x.ProductId,
                        principalSchema: "ss",
                        principalTable: "Product",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "Document",
                schema: "ss",
                columns: table => new
                {
                    DocumentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentKind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentVersionNo = table.Column<int>(type: "int", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationRequestId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Document_Request",
                        column: x => x.CreationRequestId,
                        principalSchema: "ss",
                        principalTable: "IdempotencyRequest",
                        principalColumn: "RequestId");
                    table.ForeignKey(
                        name: "FK_Document_User",
                        column: x => x.CreatedByUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ImportBatch",
                schema: "ss",
                columns: table => new
                {
                    ImportBatchId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportKind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    SourceRowCount = table.Column<int>(type: "int", nullable: false),
                    ValidationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidationReportJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConfirmedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConfirmedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportBatch", x => x.ImportBatchId);
                    table.ForeignKey(
                        name: "FK_Import_Confirmer",
                        column: x => x.ConfirmedByUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Import_Creator",
                        column: x => x.CreatedByUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Import_Request",
                        column: x => x.RequestId,
                        principalSchema: "ss",
                        principalTable: "IdempotencyRequest",
                        principalColumn: "RequestId");
                });

            migrationBuilder.CreateTable(
                name: "JobRun",
                schema: "ss",
                columns: table => new
                {
                    JobRunId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestId = table.Column<long>(type: "bigint", nullable: true),
                    QueuedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastSuccessUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AttemptCount = table.Column<int>(type: "int", nullable: false),
                    CheckpointJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SafeErrorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SafeErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRun", x => x.JobRunId);
                    table.ForeignKey(
                        name: "FK_Job_Request",
                        column: x => x.RequestId,
                        principalSchema: "ss",
                        principalTable: "IdempotencyRequest",
                        principalColumn: "RequestId");
                    table.ForeignKey(
                        name: "FK_Job_User",
                        column: x => x.RequestedByUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "DocumentVersion",
                schema: "ss",
                columns: table => new
                {
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    VersionNo = table.Column<int>(type: "int", nullable: false),
                    PayloadHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    SnapshotJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapturedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CapturedUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentVersion", x => new { x.DocumentId, x.VersionNo });
                    table.ForeignKey(
                        name: "FK_DocumentVersion_Document",
                        column: x => x.DocumentId,
                        principalSchema: "ss",
                        principalTable: "Document",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_DocumentVersion_User",
                        column: x => x.CapturedByUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrder",
                schema: "ss",
                columns: table => new
                {
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    SupplierId = table.Column<long>(type: "bigint", nullable: false),
                    ExpectedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SupplierNameSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierAddressSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTermsDaysSnapshot = table.Column<short>(type: "smallint", nullable: false),
                    ApprovedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalReceiptUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedOrCancelledByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClosedOrCancelledUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CloseReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrder", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_PO_ClosingUser",
                        column: x => x.ClosedOrCancelledByUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_PO_Document",
                        column: x => x.DocumentId,
                        principalSchema: "ss",
                        principalTable: "Document",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_PO_Supplier",
                        column: x => x.SupplierId,
                        principalSchema: "ss",
                        principalTable: "Supplier",
                        principalColumn: "SupplierId");
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                schema: "ss",
                columns: table => new
                {
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerNameSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Sale_Customer",
                        column: x => x.CustomerId,
                        principalSchema: "ss",
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Sale_Document",
                        column: x => x.DocumentId,
                        principalSchema: "ss",
                        principalTable: "Document",
                        principalColumn: "DocumentId");
                });

            migrationBuilder.CreateTable(
                name: "StockChange",
                schema: "ss",
                columns: table => new
                {
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ChangeKind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvidenceReference = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockChange", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_StockChange_Document",
                        column: x => x.DocumentId,
                        principalSchema: "ss",
                        principalTable: "Document",
                        principalColumn: "DocumentId");
                });

            migrationBuilder.CreateTable(
                name: "DocumentLine",
                schema: "ss",
                columns: table => new
                {
                    LineId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    LineNumber = table.Column<short>(type: "smallint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    SourceReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceRowNumber = table.Column<int>(type: "int", nullable: true),
                    ImportBatchId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentLine", x => x.LineId);
                    table.UniqueConstraint("UQ_DocumentLine_Identity", x => new { x.LineId, x.DocumentId, x.ProductId });
                    table.UniqueConstraint("UQ_DocumentLine_Product", x => new { x.LineId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_DocumentLine_Document",
                        column: x => x.DocumentId,
                        principalSchema: "ss",
                        principalTable: "Document",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_DocumentLine_Import",
                        column: x => x.ImportBatchId,
                        principalSchema: "ss",
                        principalTable: "ImportBatch",
                        principalColumn: "ImportBatchId");
                    table.ForeignKey(
                        name: "FK_DocumentLine_Product",
                        column: x => x.ProductId,
                        principalSchema: "ss",
                        principalTable: "Product",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "OpeningAccountBalance",
                schema: "ss",
                columns: table => new
                {
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SourceReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceRowNumber = table.Column<int>(type: "int", nullable: false),
                    ImportBatchId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningAccountBalance", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_OpeningAccount_Customer",
                        column: x => x.CustomerId,
                        principalSchema: "ss",
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_OpeningAccount_Document",
                        column: x => x.DocumentId,
                        principalSchema: "ss",
                        principalTable: "Document",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_OpeningAccount_Import",
                        column: x => x.ImportBatchId,
                        principalSchema: "ss",
                        principalTable: "ImportBatch",
                        principalColumn: "ImportBatchId");
                    table.ForeignKey(
                        name: "FK_OpeningAccount_Supplier",
                        column: x => x.SupplierId,
                        principalSchema: "ss",
                        principalTable: "Supplier",
                        principalColumn: "SupplierId");
                });

            migrationBuilder.CreateTable(
                name: "Alert",
                schema: "ss",
                columns: table => new
                {
                    AlertId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConditionKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlertKind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: true),
                    ProductBatchId = table.Column<long>(type: "bigint", nullable: true),
                    JobRunId = table.Column<long>(type: "bigint", nullable: true),
                    SafeMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RaisedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastObservedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcknowledgedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AcknowledgedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResolvedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alert", x => x.AlertId);
                    table.ForeignKey(
                        name: "FK_Alert_Batch",
                        columns: x => new { x.ProductBatchId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "ProductBatch",
                        principalColumns: new[] { "ProductBatchId", "ProductId" });
                    table.ForeignKey(
                        name: "FK_Alert_Job",
                        column: x => x.JobRunId,
                        principalSchema: "ss",
                        principalTable: "JobRun",
                        principalColumn: "JobRunId");
                    table.ForeignKey(
                        name: "FK_Alert_Product",
                        column: x => x.ProductId,
                        principalSchema: "ss",
                        principalTable: "Product",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_Alert_User",
                        column: x => x.AcknowledgedByUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ForecastRun",
                schema: "ss",
                columns: table => new
                {
                    ForecastRunId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    JobRunId = table.Column<long>(type: "bigint", nullable: false),
                    GeneratedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Confidence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HistoryStartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    HistoryEndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TrainingCutoffUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidationStartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ValidationEndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ValidationCutoffUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefitCutoffUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompleteDayCount = table.Column<int>(type: "int", nullable: false),
                    NonzeroDayCount = table.Column<int>(type: "int", nullable: false),
                    ExcludedDayCount = table.Column<int>(type: "int", nullable: false),
                    CensoredDayCount = table.Column<int>(type: "int", nullable: false),
                    InputHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ParametersAndPostprocessingJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarningText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForecastRun", x => x.ForecastRunId);
                    table.UniqueConstraint("UQ_Forecast_Product", x => new { x.ForecastRunId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Forecast_Job",
                        column: x => x.JobRunId,
                        principalSchema: "ss",
                        principalTable: "JobRun",
                        principalColumn: "JobRunId");
                    table.ForeignKey(
                        name: "FK_Forecast_Product",
                        column: x => x.ProductId,
                        principalSchema: "ss",
                        principalTable: "Product",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "Approval",
                schema: "ss",
                columns: table => new
                {
                    ApprovalId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    VersionNo = table.Column<int>(type: "int", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Decision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewedUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approval", x => x.ApprovalId);
                    table.ForeignKey(
                        name: "FK_Approval_User",
                        column: x => x.ReviewedByUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Approval_Version",
                        columns: x => new { x.DocumentId, x.VersionNo },
                        principalSchema: "ss",
                        principalTable: "DocumentVersion",
                        principalColumns: new[] { "DocumentId", "VersionNo" });
                });

            migrationBuilder.CreateTable(
                name: "Posting",
                schema: "ss",
                columns: table => new
                {
                    PostingId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    VersionNo = table.Column<int>(type: "int", nullable: false),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    ActorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BusinessDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posting", x => x.PostingId);
                    table.UniqueConstraint("UQ_Posting_IdDocument", x => new { x.PostingId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_Posting_Request",
                        column: x => x.RequestId,
                        principalSchema: "ss",
                        principalTable: "IdempotencyRequest",
                        principalColumn: "RequestId");
                    table.ForeignKey(
                        name: "FK_Posting_User",
                        column: x => x.ActorUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Posting_Version",
                        columns: x => new { x.DocumentId, x.VersionNo },
                        principalSchema: "ss",
                        principalTable: "DocumentVersion",
                        principalColumns: new[] { "DocumentId", "VersionNo" });
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceipt",
                schema: "ss",
                columns: table => new
                {
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    PurchaseOrderId = table.Column<long>(type: "bigint", nullable: false),
                    SupplierDeliveryReference = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceipt", x => x.DocumentId);
                    table.UniqueConstraint("UQ_GRN_PO", x => new { x.DocumentId, x.PurchaseOrderId });
                    table.ForeignKey(
                        name: "FK_GRN_Document",
                        column: x => x.DocumentId,
                        principalSchema: "ss",
                        principalTable: "Document",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_GRN_PO",
                        column: x => x.PurchaseOrderId,
                        principalSchema: "ss",
                        principalTable: "PurchaseOrder",
                        principalColumn: "DocumentId");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "ss",
                columns: table => new
                {
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentKind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    PurchaseOrderId = table.Column<long>(type: "bigint", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalEffectiveDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CashTenderedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CashChangeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Payment_Customer",
                        column: x => x.CustomerId,
                        principalSchema: "ss",
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Payment_Document",
                        column: x => x.DocumentId,
                        principalSchema: "ss",
                        principalTable: "Document",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_Payment_PO",
                        column: x => x.PurchaseOrderId,
                        principalSchema: "ss",
                        principalTable: "PurchaseOrder",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_Payment_Supplier",
                        column: x => x.SupplierId,
                        principalSchema: "ss",
                        principalTable: "Supplier",
                        principalColumn: "SupplierId");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderLine",
                schema: "ss",
                columns: table => new
                {
                    LineId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductNameSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetReceivedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderLine", x => x.LineId);
                    table.UniqueConstraint("UQ_POLine_Identity", x => new { x.LineId, x.DocumentId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_POLine_Line",
                        columns: x => new { x.LineId, x.DocumentId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "DocumentLine",
                        principalColumns: new[] { "LineId", "DocumentId", "ProductId" });
                    table.ForeignKey(
                        name: "FK_POLine_PO",
                        column: x => x.DocumentId,
                        principalSchema: "ss",
                        principalTable: "PurchaseOrder",
                        principalColumn: "DocumentId");
                });

            migrationBuilder.CreateTable(
                name: "SaleLine",
                schema: "ss",
                columns: table => new
                {
                    LineId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductNameSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExplicitDiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceDiscountAllocation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WacAtPosting = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CogsValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleLine", x => x.LineId);
                    table.UniqueConstraint("UQ_SaleLine_Identity", x => new { x.LineId, x.DocumentId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_SaleLine_Line",
                        columns: x => new { x.LineId, x.DocumentId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "DocumentLine",
                        principalColumns: new[] { "LineId", "DocumentId", "ProductId" });
                    table.ForeignKey(
                        name: "FK_SaleLine_Sale",
                        column: x => x.DocumentId,
                        principalSchema: "ss",
                        principalTable: "Sale",
                        principalColumn: "DocumentId");
                });

            migrationBuilder.CreateTable(
                name: "StockChangeLine",
                schema: "ss",
                columns: table => new
                {
                    LineId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductBatchId = table.Column<long>(type: "bigint", nullable: false),
                    Bucket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToBucket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityDelta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApprovedUnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExplicitValueDelta = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpectedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CountedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CapturedProductMovementVersion = table.Column<long>(type: "bigint", nullable: true),
                    CapturedBucketMovementVersion = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockChangeLine", x => x.LineId);
                    table.ForeignKey(
                        name: "FK_StockChangeLine_Batch",
                        columns: x => new { x.ProductBatchId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "ProductBatch",
                        principalColumns: new[] { "ProductBatchId", "ProductId" });
                    table.ForeignKey(
                        name: "FK_StockChangeLine_Header",
                        column: x => x.DocumentId,
                        principalSchema: "ss",
                        principalTable: "StockChange",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_StockChangeLine_Line",
                        columns: x => new { x.LineId, x.DocumentId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "DocumentLine",
                        principalColumns: new[] { "LineId", "DocumentId", "ProductId" });
                });

            migrationBuilder.CreateTable(
                name: "NotificationDelivery",
                schema: "ss",
                columns: table => new
                {
                    DeliveryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlertId = table.Column<long>(type: "bigint", nullable: false),
                    JobRunId = table.Column<long>(type: "bigint", nullable: true),
                    NotificationKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedRecipientReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttemptCount = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastAttemptUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextAttemptUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SafeErrorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationDelivery", x => x.DeliveryId);
                    table.ForeignKey(
                        name: "FK_Notification_Alert",
                        column: x => x.AlertId,
                        principalSchema: "ss",
                        principalTable: "Alert",
                        principalColumn: "AlertId");
                    table.ForeignKey(
                        name: "FK_Notification_Job",
                        column: x => x.JobRunId,
                        principalSchema: "ss",
                        principalTable: "JobRun",
                        principalColumn: "JobRunId");
                });

            migrationBuilder.CreateTable(
                name: "ForecastMetric",
                schema: "ss",
                columns: table => new
                {
                    ForecastRunId = table.Column<long>(type: "bigint", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FitSucceeded = table.Column<bool>(type: "bit", nullable: false),
                    SampleCount = table.Column<int>(type: "int", nullable: false),
                    Mae = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Mape = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Wape = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MapeIncludedCount = table.Column<int>(type: "int", nullable: false),
                    MapeExcludedCount = table.Column<int>(type: "int", nullable: false),
                    ObservedIntervalCoverage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FailureCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForecastMetric", x => new { x.ForecastRunId, x.Method });
                    table.ForeignKey(
                        name: "FK_Metric_Run",
                        column: x => x.ForecastRunId,
                        principalSchema: "ss",
                        principalTable: "ForecastRun",
                        principalColumn: "ForecastRunId");
                });

            migrationBuilder.CreateTable(
                name: "ForecastObservation",
                schema: "ss",
                columns: table => new
                {
                    ForecastRunId = table.Column<long>(type: "bigint", nullable: false),
                    SnapshotPhase = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ObservationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    NetDemand = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsCensored = table.Column<bool>(type: "bit", nullable: false),
                    GapReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KnownAsOfUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForecastObservation", x => new { x.ForecastRunId, x.SnapshotPhase, x.ObservationDate });
                    table.ForeignKey(
                        name: "FK_Observation_Run",
                        column: x => x.ForecastRunId,
                        principalSchema: "ss",
                        principalTable: "ForecastRun",
                        principalColumn: "ForecastRunId");
                });

            migrationBuilder.CreateTable(
                name: "ForecastPoint",
                schema: "ss",
                columns: table => new
                {
                    ForecastRunId = table.Column<long>(type: "bigint", nullable: false),
                    HorizonDays = table.Column<short>(type: "smallint", nullable: false),
                    PredictedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LowerQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UpperQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NominalCoverage = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForecastPoint", x => new { x.ForecastRunId, x.HorizonDays });
                    table.ForeignKey(
                        name: "FK_ForecastPoint_Run",
                        column: x => x.ForecastRunId,
                        principalSchema: "ss",
                        principalTable: "ForecastRun",
                        principalColumn: "ForecastRunId");
                });

            migrationBuilder.CreateTable(
                name: "ReorderSuggestion",
                schema: "ss",
                columns: table => new
                {
                    SuggestionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpportunityKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    ForecastRunId = table.Column<long>(type: "bigint", nullable: true),
                    GeneratedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InputAsOfUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CalculationVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    InputSnapshotJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReorderModeSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductMovementVersion = table.Column<long>(type: "bigint", nullable: false),
                    SaleableQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EligibleOnOrderQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UncertainOnOrderQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FutureOnOrderQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PendingDraftQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DailyDemand = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DemandSigma = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LeadTimeDays = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LeadTimeSource = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeadTimeSampleCount = table.Column<short>(type: "smallint", nullable: false),
                    ReviewPeriodDays = table.Column<short>(type: "smallint", nullable: false),
                    SafetyFactor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityIncrement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PackQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinimumOrderQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProposedUnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SavedManualRop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CalculatedRop = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EffectiveRop = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SafetyStock = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RawSuggestedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SuggestedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EstimatedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DaysOfStock = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsFallback = table.Column<bool>(type: "bit", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MissingInputsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutcomeByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OutcomeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OutcomeReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReorderSuggestion", x => x.SuggestionId);
                    table.UniqueConstraint("UQ_Suggestion_Opportunity", x => new { x.SuggestionId, x.OpportunityKey });
                    table.ForeignKey(
                        name: "FK_Suggestion_Forecast",
                        columns: x => new { x.ForecastRunId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "ForecastRun",
                        principalColumns: new[] { "ForecastRunId", "ProductId" });
                    table.ForeignKey(
                        name: "FK_Suggestion_OutcomeUser",
                        column: x => x.OutcomeByUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Suggestion_Product",
                        column: x => x.ProductId,
                        principalSchema: "ss",
                        principalTable: "Product",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_Suggestion_Supplier",
                        column: x => x.SupplierId,
                        principalSchema: "ss",
                        principalTable: "Supplier",
                        principalColumn: "SupplierId");
                });

            migrationBuilder.CreateTable(
                name: "AccountEntry",
                schema: "ss",
                columns: table => new
                {
                    AccountEntryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostingId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    EntryKind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Side = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountEntry", x => x.AccountEntryId);
                    table.ForeignKey(
                        name: "FK_AccountEntry_Customer",
                        column: x => x.CustomerId,
                        principalSchema: "ss",
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_AccountEntry_Posting",
                        column: x => x.PostingId,
                        principalSchema: "ss",
                        principalTable: "Posting",
                        principalColumn: "PostingId");
                    table.ForeignKey(
                        name: "FK_AccountEntry_Supplier",
                        column: x => x.SupplierId,
                        principalSchema: "ss",
                        principalTable: "Supplier",
                        principalColumn: "SupplierId");
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                schema: "ss",
                columns: table => new
                {
                    AuditLogId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ActorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccurredUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Outcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: true),
                    PostingId = table.Column<long>(type: "bigint", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: true),
                    TargetUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImportBatchId = table.Column<long>(type: "bigint", nullable: true),
                    JobRunId = table.Column<long>(type: "bigint", nullable: true),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeforeRedactedJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AfterRedactedJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.AuditLogId);
                    table.ForeignKey(
                        name: "FK_Audit_Actor",
                        column: x => x.ActorUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Audit_Document",
                        column: x => x.DocumentId,
                        principalSchema: "ss",
                        principalTable: "Document",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_Audit_Import",
                        column: x => x.ImportBatchId,
                        principalSchema: "ss",
                        principalTable: "ImportBatch",
                        principalColumn: "ImportBatchId");
                    table.ForeignKey(
                        name: "FK_Audit_Job",
                        column: x => x.JobRunId,
                        principalSchema: "ss",
                        principalTable: "JobRun",
                        principalColumn: "JobRunId");
                    table.ForeignKey(
                        name: "FK_Audit_Posting",
                        column: x => x.PostingId,
                        principalSchema: "ss",
                        principalTable: "Posting",
                        principalColumn: "PostingId");
                    table.ForeignKey(
                        name: "FK_Audit_Product",
                        column: x => x.ProductId,
                        principalSchema: "ss",
                        principalTable: "Product",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_Audit_TargetUser",
                        column: x => x.TargetUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ProductStockBalance",
                schema: "ss",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InventoryValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MovementVersion = table.Column<long>(type: "bigint", nullable: false),
                    LastPostingId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStockBalance", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductBalance_Posting",
                        column: x => x.LastPostingId,
                        principalSchema: "ss",
                        principalTable: "Posting",
                        principalColumn: "PostingId");
                    table.ForeignKey(
                        name: "FK_ProductBalance_Product",
                        column: x => x.ProductId,
                        principalSchema: "ss",
                        principalTable: "Product",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "SalesReturn",
                schema: "ss",
                columns: table => new
                {
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    OriginalSaleId = table.Column<long>(type: "bigint", nullable: false),
                    ReturnMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostingId = table.Column<long>(type: "bigint", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReturn", x => x.DocumentId);
                    table.UniqueConstraint("UQ_Return_Sale", x => new { x.DocumentId, x.OriginalSaleId });
                    table.ForeignKey(
                        name: "FK_Return_Document",
                        column: x => x.DocumentId,
                        principalSchema: "ss",
                        principalTable: "Document",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_Return_Sale",
                        column: x => x.OriginalSaleId,
                        principalSchema: "ss",
                        principalTable: "Sale",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_SalesReturn_Posting",
                        columns: x => new { x.PostingId, x.DocumentId },
                        principalSchema: "ss",
                        principalTable: "Posting",
                        principalColumns: new[] { "PostingId", "DocumentId" });
                });

            migrationBuilder.CreateTable(
                name: "StockBalance",
                schema: "ss",
                columns: table => new
                {
                    ProductBatchId = table.Column<long>(type: "bigint", nullable: false),
                    Bucket = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MovementVersion = table.Column<long>(type: "bigint", nullable: false),
                    LastPostingId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockBalance", x => new { x.ProductBatchId, x.Bucket });
                    table.ForeignKey(
                        name: "FK_StockBalance_Batch",
                        columns: x => new { x.ProductBatchId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "ProductBatch",
                        principalColumns: new[] { "ProductBatchId", "ProductId" });
                    table.ForeignKey(
                        name: "FK_StockBalance_Posting",
                        column: x => x.LastPostingId,
                        principalSchema: "ss",
                        principalTable: "Posting",
                        principalColumn: "PostingId");
                });

            migrationBuilder.CreateTable(
                name: "StockMovement",
                schema: "ss",
                columns: table => new
                {
                    StockMovementId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostingId = table.Column<long>(type: "bigint", nullable: false),
                    SourceLineId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductBatchId = table.Column<long>(type: "bigint", nullable: false),
                    Bucket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovementKind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityDelta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValueDelta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReversesMovementId = table.Column<long>(type: "bigint", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovement", x => x.StockMovementId);
                    table.ForeignKey(
                        name: "FK_Movement_Batch",
                        columns: x => new { x.ProductBatchId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "ProductBatch",
                        principalColumns: new[] { "ProductBatchId", "ProductId" });
                    table.ForeignKey(
                        name: "FK_Movement_Posting",
                        column: x => x.PostingId,
                        principalSchema: "ss",
                        principalTable: "Posting",
                        principalColumn: "PostingId");
                    table.ForeignKey(
                        name: "FK_Movement_Reversed",
                        column: x => x.ReversesMovementId,
                        principalSchema: "ss",
                        principalTable: "StockMovement",
                        principalColumn: "StockMovementId");
                    table.ForeignKey(
                        name: "FK_Movement_Source",
                        columns: x => new { x.SourceLineId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "DocumentLine",
                        principalColumns: new[] { "LineId", "ProductId" });
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceiptReversal",
                schema: "ss",
                columns: table => new
                {
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    OriginalGoodsReceiptId = table.Column<long>(type: "bigint", nullable: false),
                    PostingId = table.Column<long>(type: "bigint", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceiptReversal", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_GRNReversal_Document",
                        column: x => x.DocumentId,
                        principalSchema: "ss",
                        principalTable: "Document",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_GRNReversal_Original",
                        column: x => x.OriginalGoodsReceiptId,
                        principalSchema: "ss",
                        principalTable: "GoodsReceipt",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_GRNReversal_Posting",
                        columns: x => new { x.PostingId, x.DocumentId },
                        principalSchema: "ss",
                        principalTable: "Posting",
                        principalColumns: new[] { "PostingId", "DocumentId" });
                });

            migrationBuilder.CreateTable(
                name: "PaymentReversal",
                schema: "ss",
                columns: table => new
                {
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    OriginalPaymentId = table.Column<long>(type: "bigint", nullable: false),
                    PostingId = table.Column<long>(type: "bigint", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentReversal", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_PaymentReversal_Document",
                        column: x => x.DocumentId,
                        principalSchema: "ss",
                        principalTable: "Document",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_PaymentReversal_Original",
                        column: x => x.OriginalPaymentId,
                        principalSchema: "ss",
                        principalTable: "Payment",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_PaymentReversal_Posting",
                        columns: x => new { x.PostingId, x.DocumentId },
                        principalSchema: "ss",
                        principalTable: "Posting",
                        principalColumns: new[] { "PostingId", "DocumentId" });
                });

            migrationBuilder.CreateTable(
                name: "PaymentSettlementEvent",
                schema: "ss",
                columns: table => new
                {
                    EventId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<long>(type: "bigint", nullable: false),
                    EventNo = table.Column<int>(type: "int", nullable: false),
                    SettlementStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvidenceReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordedUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSettlementEvent", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_PaymentSettlement_Payment",
                        column: x => x.PaymentId,
                        principalSchema: "ss",
                        principalTable: "Payment",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_PaymentSettlement_User",
                        column: x => x.RecordedByUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceiptLine",
                schema: "ss",
                columns: table => new
                {
                    LineId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    PurchaseOrderId = table.Column<long>(type: "bigint", nullable: false),
                    PurchaseOrderLineId = table.Column<long>(type: "bigint", nullable: false),
                    ProductBatchId = table.Column<long>(type: "bigint", nullable: false),
                    Bucket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InventoryValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayableAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceiptLine", x => x.LineId);
                    table.ForeignKey(
                        name: "FK_GRNLine_Batch",
                        columns: x => new { x.ProductBatchId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "ProductBatch",
                        principalColumns: new[] { "ProductBatchId", "ProductId" });
                    table.ForeignKey(
                        name: "FK_GRNLine_GRN",
                        columns: x => new { x.DocumentId, x.PurchaseOrderId },
                        principalSchema: "ss",
                        principalTable: "GoodsReceipt",
                        principalColumns: new[] { "DocumentId", "PurchaseOrderId" });
                    table.ForeignKey(
                        name: "FK_GRNLine_Line",
                        columns: x => new { x.LineId, x.DocumentId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "DocumentLine",
                        principalColumns: new[] { "LineId", "DocumentId", "ProductId" });
                    table.ForeignKey(
                        name: "FK_GRNLine_POLine",
                        columns: x => new { x.PurchaseOrderLineId, x.PurchaseOrderId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "PurchaseOrderLine",
                        principalColumns: new[] { "LineId", "DocumentId", "ProductId" });
                });

            migrationBuilder.CreateTable(
                name: "SaleBatchAllocation",
                schema: "ss",
                columns: table => new
                {
                    AllocationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleLineId = table.Column<long>(type: "bigint", nullable: false),
                    SaleId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductBatchId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InventoryValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleBatchAllocation", x => x.AllocationId);
                    table.UniqueConstraint("UQ_SaleAllocation_Identity", x => new { x.AllocationId, x.SaleId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_SaleAllocation_Batch",
                        columns: x => new { x.ProductBatchId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "ProductBatch",
                        principalColumns: new[] { "ProductBatchId", "ProductId" });
                    table.ForeignKey(
                        name: "FK_SaleAllocation_Line",
                        columns: x => new { x.SaleLineId, x.SaleId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "SaleLine",
                        principalColumns: new[] { "LineId", "DocumentId", "ProductId" });
                });

            migrationBuilder.CreateTable(
                name: "AiToolCallLog",
                schema: "ss",
                columns: table => new
                {
                    ToolCallId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AiChatLogId = table.Column<long>(type: "bigint", nullable: false),
                    StepNo = table.Column<short>(type: "smallint", nullable: false),
                    ToolName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParametersRedactedJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultDescriptorRedactedJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourcePostingId = table.Column<long>(type: "bigint", nullable: true),
                    ForecastRunId = table.Column<long>(type: "bigint", nullable: true),
                    SuggestionId = table.Column<long>(type: "bigint", nullable: true),
                    CalledUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAsOfUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WasTruncated = table.Column<bool>(type: "bit", nullable: false),
                    SafeOutcomeCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiToolCallLog", x => x.ToolCallId);
                    table.ForeignKey(
                        name: "FK_ToolCall_Chat",
                        column: x => x.AiChatLogId,
                        principalSchema: "ss",
                        principalTable: "AiChatLog",
                        principalColumn: "AiChatLogId");
                    table.ForeignKey(
                        name: "FK_ToolCall_Forecast",
                        column: x => x.ForecastRunId,
                        principalSchema: "ss",
                        principalTable: "ForecastRun",
                        principalColumn: "ForecastRunId");
                    table.ForeignKey(
                        name: "FK_ToolCall_Posting",
                        column: x => x.SourcePostingId,
                        principalSchema: "ss",
                        principalTable: "Posting",
                        principalColumn: "PostingId");
                    table.ForeignKey(
                        name: "FK_ToolCall_Suggestion",
                        column: x => x.SuggestionId,
                        principalSchema: "ss",
                        principalTable: "ReorderSuggestion",
                        principalColumn: "SuggestionId");
                });

            migrationBuilder.CreateTable(
                name: "SuggestionConversion",
                schema: "ss",
                columns: table => new
                {
                    ConversionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuggestionId = table.Column<long>(type: "bigint", nullable: false),
                    OpportunityKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseOrderLineId = table.Column<long>(type: "bigint", nullable: false),
                    ReviewedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReviewedUnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UncertainOrdersAcknowledged = table.Column<bool>(type: "bit", nullable: false),
                    ReviewedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConvertedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    ReleasedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReleasedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReleaseReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionConversion", x => x.ConversionId);
                    table.ForeignKey(
                        name: "FK_Conversion_POLine",
                        column: x => x.PurchaseOrderLineId,
                        principalSchema: "ss",
                        principalTable: "PurchaseOrderLine",
                        principalColumn: "LineId");
                    table.ForeignKey(
                        name: "FK_Conversion_ReleaseUser",
                        column: x => x.ReleasedByUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Conversion_Request",
                        column: x => x.RequestId,
                        principalSchema: "ss",
                        principalTable: "IdempotencyRequest",
                        principalColumn: "RequestId");
                    table.ForeignKey(
                        name: "FK_Conversion_Suggestion",
                        columns: x => new { x.SuggestionId, x.OpportunityKey },
                        principalSchema: "ss",
                        principalTable: "ReorderSuggestion",
                        principalColumns: new[] { "SuggestionId", "OpportunityKey" });
                    table.ForeignKey(
                        name: "FK_Conversion_User",
                        column: x => x.ReviewedByUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AccountAllocation",
                schema: "ss",
                columns: table => new
                {
                    AllocationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditEntryId = table.Column<long>(type: "bigint", nullable: false),
                    DebitEntryId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AllocationKind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReversesAllocationId = table.Column<long>(type: "bigint", nullable: true),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    ActorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllocatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountAllocation", x => x.AllocationId);
                    table.ForeignKey(
                        name: "FK_AccountAllocation_Credit",
                        column: x => x.CreditEntryId,
                        principalSchema: "ss",
                        principalTable: "AccountEntry",
                        principalColumn: "AccountEntryId");
                    table.ForeignKey(
                        name: "FK_AccountAllocation_Debit",
                        column: x => x.DebitEntryId,
                        principalSchema: "ss",
                        principalTable: "AccountEntry",
                        principalColumn: "AccountEntryId");
                    table.ForeignKey(
                        name: "FK_AccountAllocation_Request",
                        column: x => x.RequestId,
                        principalSchema: "ss",
                        principalTable: "IdempotencyRequest",
                        principalColumn: "RequestId");
                    table.ForeignKey(
                        name: "FK_AccountAllocation_Reversed",
                        column: x => x.ReversesAllocationId,
                        principalSchema: "ss",
                        principalTable: "AccountAllocation",
                        principalColumn: "AllocationId");
                    table.ForeignKey(
                        name: "FK_AccountAllocation_User",
                        column: x => x.ActorUserId,
                        principalSchema: "ss",
                        principalTable: "UserProfile",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "SalesReturnLine",
                schema: "ss",
                columns: table => new
                {
                    LineId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    OriginalSaleId = table.Column<long>(type: "bigint", nullable: false),
                    OriginalAllocationId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RestockValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReturnLine", x => x.LineId);
                    table.ForeignKey(
                        name: "FK_ReturnLine_Allocation",
                        columns: x => new { x.OriginalAllocationId, x.OriginalSaleId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "SaleBatchAllocation",
                        principalColumns: new[] { "AllocationId", "SaleId", "ProductId" });
                    table.ForeignKey(
                        name: "FK_ReturnLine_Line",
                        columns: x => new { x.LineId, x.DocumentId, x.ProductId },
                        principalSchema: "ss",
                        principalTable: "DocumentLine",
                        principalColumns: new[] { "LineId", "DocumentId", "ProductId" });
                    table.ForeignKey(
                        name: "FK_ReturnLine_Return",
                        columns: x => new { x.DocumentId, x.OriginalSaleId },
                        principalSchema: "ss",
                        principalTable: "SalesReturn",
                        principalColumns: new[] { "DocumentId", "OriginalSaleId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountAllocation_ActorUserId",
                schema: "ss",
                table: "AccountAllocation",
                column: "ActorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountAllocation_CreditEntryId",
                schema: "ss",
                table: "AccountAllocation",
                column: "CreditEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountAllocation_DebitEntryId",
                schema: "ss",
                table: "AccountAllocation",
                column: "DebitEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountAllocation_RequestId",
                schema: "ss",
                table: "AccountAllocation",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountAllocation_ReversesAllocationId",
                schema: "ss",
                table: "AccountAllocation",
                column: "ReversesAllocationId",
                unique: true,
                filter: "[ReversesAllocationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccountEntry_CustomerId",
                schema: "ss",
                table: "AccountEntry",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountEntry_PostingId",
                schema: "ss",
                table: "AccountEntry",
                column: "PostingId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountEntry_SupplierId",
                schema: "ss",
                table: "AccountEntry",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_AiChatLog_UserId",
                schema: "ss",
                table: "AiChatLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AiToolCallLog_AiChatLogId",
                schema: "ss",
                table: "AiToolCallLog",
                column: "AiChatLogId");

            migrationBuilder.CreateIndex(
                name: "IX_AiToolCallLog_ForecastRunId",
                schema: "ss",
                table: "AiToolCallLog",
                column: "ForecastRunId");

            migrationBuilder.CreateIndex(
                name: "IX_AiToolCallLog_SourcePostingId",
                schema: "ss",
                table: "AiToolCallLog",
                column: "SourcePostingId");

            migrationBuilder.CreateIndex(
                name: "IX_AiToolCallLog_SuggestionId",
                schema: "ss",
                table: "AiToolCallLog",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Alert_AcknowledgedByUserId",
                schema: "ss",
                table: "Alert",
                column: "AcknowledgedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Alert_JobRunId",
                schema: "ss",
                table: "Alert",
                column: "JobRunId");

            migrationBuilder.CreateIndex(
                name: "IX_Alert_ProductBatchId_ProductId",
                schema: "ss",
                table: "Alert",
                columns: new[] { "ProductBatchId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_Alert_ProductId",
                schema: "ss",
                table: "Alert",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_DocumentId_VersionNo",
                schema: "ss",
                table: "Approval",
                columns: new[] { "DocumentId", "VersionNo" });

            migrationBuilder.CreateIndex(
                name: "IX_Approval_ReviewedByUserId",
                schema: "ss",
                table: "Approval",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_ActorUserId",
                schema: "ss",
                table: "AuditLog",
                column: "ActorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_DocumentId",
                schema: "ss",
                table: "AuditLog",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_ImportBatchId",
                schema: "ss",
                table: "AuditLog",
                column: "ImportBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_JobRunId",
                schema: "ss",
                table: "AuditLog",
                column: "JobRunId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_PostingId",
                schema: "ss",
                table: "AuditLog",
                column: "PostingId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_ProductId",
                schema: "ss",
                table: "AuditLog",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_TargetUserId",
                schema: "ss",
                table: "AuditLog",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSettings_UpdatedByUserId",
                schema: "ss",
                table: "BusinessSettings",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                schema: "ss",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_CreatedByUserId",
                schema: "ss",
                table: "Document",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_CreationRequestId",
                schema: "ss",
                table: "Document",
                column: "CreationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentLine_DocumentId",
                schema: "ss",
                table: "DocumentLine",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentLine_ImportBatchId",
                schema: "ss",
                table: "DocumentLine",
                column: "ImportBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentLine_ProductId",
                schema: "ss",
                table: "DocumentLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentVersion_CapturedByUserId",
                schema: "ss",
                table: "DocumentVersion",
                column: "CapturedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ForecastRun_JobRunId",
                schema: "ss",
                table: "ForecastRun",
                column: "JobRunId");

            migrationBuilder.CreateIndex(
                name: "IX_ForecastRun_ProductId",
                schema: "ss",
                table: "ForecastRun",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipt_PurchaseOrderId",
                schema: "ss",
                table: "GoodsReceipt",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptLine_DocumentId_PurchaseOrderId",
                schema: "ss",
                table: "GoodsReceiptLine",
                columns: new[] { "DocumentId", "PurchaseOrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptLine_LineId_DocumentId_ProductId",
                schema: "ss",
                table: "GoodsReceiptLine",
                columns: new[] { "LineId", "DocumentId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptLine_ProductBatchId_ProductId",
                schema: "ss",
                table: "GoodsReceiptLine",
                columns: new[] { "ProductBatchId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptLine_PurchaseOrderLineId_PurchaseOrderId_ProductId",
                schema: "ss",
                table: "GoodsReceiptLine",
                columns: new[] { "PurchaseOrderLineId", "PurchaseOrderId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptReversal_OriginalGoodsReceiptId",
                schema: "ss",
                table: "GoodsReceiptReversal",
                column: "OriginalGoodsReceiptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptReversal_PostingId_DocumentId",
                schema: "ss",
                table: "GoodsReceiptReversal",
                columns: new[] { "PostingId", "DocumentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdempotencyRequest_ActorUserId",
                schema: "ss",
                table: "IdempotencyRequest",
                column: "ActorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportBatch_ConfirmedByUserId",
                schema: "ss",
                table: "ImportBatch",
                column: "ConfirmedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportBatch_CreatedByUserId",
                schema: "ss",
                table: "ImportBatch",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportBatch_RequestId",
                schema: "ss",
                table: "ImportBatch",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRun_RequestedByUserId",
                schema: "ss",
                table: "JobRun",
                column: "RequestedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRun_RequestId",
                schema: "ss",
                table: "JobRun",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationDelivery_AlertId",
                schema: "ss",
                table: "NotificationDelivery",
                column: "AlertId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationDelivery_JobRunId",
                schema: "ss",
                table: "NotificationDelivery",
                column: "JobRunId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningAccountBalance_CustomerId",
                schema: "ss",
                table: "OpeningAccountBalance",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningAccountBalance_ImportBatchId",
                schema: "ss",
                table: "OpeningAccountBalance",
                column: "ImportBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningAccountBalance_SupplierId",
                schema: "ss",
                table: "OpeningAccountBalance",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CustomerId",
                schema: "ss",
                table: "Payment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PurchaseOrderId",
                schema: "ss",
                table: "Payment",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_SupplierId",
                schema: "ss",
                table: "Payment",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReversal_OriginalPaymentId",
                schema: "ss",
                table: "PaymentReversal",
                column: "OriginalPaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReversal_PostingId_DocumentId",
                schema: "ss",
                table: "PaymentReversal",
                columns: new[] { "PostingId", "DocumentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSettlementEvent_PaymentId",
                schema: "ss",
                table: "PaymentSettlementEvent",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSettlementEvent_RecordedByUserId",
                schema: "ss",
                table: "PaymentSettlementEvent",
                column: "RecordedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posting_ActorUserId",
                schema: "ss",
                table: "Posting",
                column: "ActorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posting_DocumentId_VersionNo",
                schema: "ss",
                table: "Posting",
                columns: new[] { "DocumentId", "VersionNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posting_RequestId",
                schema: "ss",
                table: "Posting",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                schema: "ss",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_PreferredSupplierId",
                schema: "ss",
                table: "Product",
                column: "PreferredSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBatch_ProductId",
                schema: "ss",
                table: "ProductBatch",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStockBalance_LastPostingId",
                schema: "ss",
                table: "ProductStockBalance",
                column: "LastPostingId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_ClosedOrCancelledByUserId",
                schema: "ss",
                table: "PurchaseOrder",
                column: "ClosedOrCancelledByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_SupplierId",
                schema: "ss",
                table: "PurchaseOrder",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLine_DocumentId",
                schema: "ss",
                table: "PurchaseOrderLine",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReorderSuggestion_ForecastRunId_ProductId",
                schema: "ss",
                table: "ReorderSuggestion",
                columns: new[] { "ForecastRunId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_ReorderSuggestion_OutcomeByUserId",
                schema: "ss",
                table: "ReorderSuggestion",
                column: "OutcomeByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReorderSuggestion_ProductId",
                schema: "ss",
                table: "ReorderSuggestion",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReorderSuggestion_SupplierId",
                schema: "ss",
                table: "ReorderSuggestion",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_CustomerId",
                schema: "ss",
                table: "Sale",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleBatchAllocation_ProductBatchId_ProductId",
                schema: "ss",
                table: "SaleBatchAllocation",
                columns: new[] { "ProductBatchId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_SaleBatchAllocation_SaleLineId_SaleId_ProductId",
                schema: "ss",
                table: "SaleBatchAllocation",
                columns: new[] { "SaleLineId", "SaleId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_SaleLine_DocumentId",
                schema: "ss",
                table: "SaleLine",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturn_OriginalSaleId",
                schema: "ss",
                table: "SalesReturn",
                column: "OriginalSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturn_PostingId_DocumentId",
                schema: "ss",
                table: "SalesReturn",
                columns: new[] { "PostingId", "DocumentId" },
                unique: true,
                filter: "[PostingId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnLine_DocumentId_OriginalSaleId",
                schema: "ss",
                table: "SalesReturnLine",
                columns: new[] { "DocumentId", "OriginalSaleId" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnLine_LineId_DocumentId_ProductId",
                schema: "ss",
                table: "SalesReturnLine",
                columns: new[] { "LineId", "DocumentId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnLine_OriginalAllocationId_OriginalSaleId_ProductId",
                schema: "ss",
                table: "SalesReturnLine",
                columns: new[] { "OriginalAllocationId", "OriginalSaleId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_StockBalance_LastPostingId",
                schema: "ss",
                table: "StockBalance",
                column: "LastPostingId");

            migrationBuilder.CreateIndex(
                name: "IX_StockBalance_ProductBatchId_ProductId",
                schema: "ss",
                table: "StockBalance",
                columns: new[] { "ProductBatchId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_StockChangeLine_DocumentId",
                schema: "ss",
                table: "StockChangeLine",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_StockChangeLine_LineId_DocumentId_ProductId",
                schema: "ss",
                table: "StockChangeLine",
                columns: new[] { "LineId", "DocumentId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockChangeLine_ProductBatchId_ProductId",
                schema: "ss",
                table: "StockChangeLine",
                columns: new[] { "ProductBatchId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_StockMovement_PostingId",
                schema: "ss",
                table: "StockMovement",
                column: "PostingId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovement_ProductBatchId_ProductId",
                schema: "ss",
                table: "StockMovement",
                columns: new[] { "ProductBatchId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_StockMovement_ReversesMovementId",
                schema: "ss",
                table: "StockMovement",
                column: "ReversesMovementId",
                unique: true,
                filter: "[ReversesMovementId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovement_SourceLineId_ProductId",
                schema: "ss",
                table: "StockMovement",
                columns: new[] { "SourceLineId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionConversion_PurchaseOrderLineId",
                schema: "ss",
                table: "SuggestionConversion",
                column: "PurchaseOrderLineId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionConversion_ReleasedByUserId",
                schema: "ss",
                table: "SuggestionConversion",
                column: "ReleasedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionConversion_RequestId",
                schema: "ss",
                table: "SuggestionConversion",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionConversion_ReviewedByUserId",
                schema: "ss",
                table: "SuggestionConversion",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionConversion_SuggestionId_OpportunityKey",
                schema: "ss",
                table: "SuggestionConversion",
                columns: new[] { "SuggestionId", "OpportunityKey" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountAllocation",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "AiToolCallLog",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "Approval",
                schema: "ss");

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
                name: "AuditLog",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "BusinessSettings",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "ForecastMetric",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "ForecastObservation",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "ForecastPoint",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "GoodsReceiptLine",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "GoodsReceiptReversal",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "NotificationDelivery",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "OpeningAccountBalance",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "PaymentReversal",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "PaymentSettlementEvent",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "ProductStockBalance",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "SalesReturnLine",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "StockBalance",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "StockChangeLine",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "StockMovement",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "SuggestionConversion",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "AccountEntry",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "AiChatLog",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "GoodsReceipt",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "Alert",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "Payment",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "SaleBatchAllocation",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "SalesReturn",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "StockChange",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "PurchaseOrderLine",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "ReorderSuggestion",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "ProductBatch",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "SaleLine",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "Posting",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "PurchaseOrder",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "ForecastRun",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "DocumentLine",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "Sale",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "DocumentVersion",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "JobRun",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "ImportBatch",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "Document",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "Supplier",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "IdempotencyRequest",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "UserProfile",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
