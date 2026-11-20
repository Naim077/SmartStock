using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AIMS.DataAccess;

public class ApplicationDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<AccountAllocation> AccountAllocations { get; set; } = null!;
    public DbSet<AccountEntry> AccountEntries { get; set; } = null!;
    public DbSet<AiChatLog> AiChatLogs { get; set; } = null!;
    public DbSet<AiToolCallLog> AiToolCallLogs { get; set; } = null!;
    public DbSet<Alert> Alerts { get; set; } = null!;
    public DbSet<Approval> Approvals { get; set; } = null!;
    public DbSet<AuditLog> AuditLogs { get; set; } = null!;
    public DbSet<BusinessSettings> BusinessSettingses { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Document> Documents { get; set; } = null!;
    public DbSet<DocumentLine> DocumentLines { get; set; } = null!;
    public DbSet<DocumentVersion> DocumentVersions { get; set; } = null!;
    public DbSet<ForecastMetric> ForecastMetrics { get; set; } = null!;
    public DbSet<ForecastObservation> ForecastObservations { get; set; } = null!;
    public DbSet<ForecastPoint> ForecastPoints { get; set; } = null!;
    public DbSet<ForecastRun> ForecastRuns { get; set; } = null!;
    public DbSet<GoodsReceipt> GoodsReceipts { get; set; } = null!;
    public DbSet<GoodsReceiptLine> GoodsReceiptLines { get; set; } = null!;
    public DbSet<GoodsReceiptReversal> GoodsReceiptReversals { get; set; } = null!;
    public DbSet<IdempotencyRequest> IdempotencyRequests { get; set; } = null!;
    public DbSet<ImportBatch> ImportBatches { get; set; } = null!;
    public DbSet<JobRun> JobRuns { get; set; } = null!;
    public DbSet<NotificationDelivery> NotificationDeliveries { get; set; } = null!;
    public DbSet<OpeningAccountBalance> OpeningAccountBalances { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<PaymentReversal> PaymentReversals { get; set; } = null!;
    public DbSet<PaymentSettlementEvent> PaymentSettlementEvents { get; set; } = null!;
    public DbSet<Permission> Permissions { get; set; } = null!;
    public DbSet<PermissionGroup> PermissionGroups { get; set; } = null!;
    public DbSet<PermissionGroupMap> PermissionGroupMaps { get; set; } = null!;
    public DbSet<Posting> Postings { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductBatch> ProductBatches { get; set; } = null!;
    public DbSet<ProductStockBalance> ProductStockBalances { get; set; } = null!;
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; } = null!;
    public DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; } = null!;
    public DbSet<ReorderSuggestion> ReorderSuggestions { get; set; } = null!;
    public DbSet<Sale> Sales { get; set; } = null!;
    public DbSet<SaleBatchAllocation> SaleBatchAllocations { get; set; } = null!;
    public DbSet<SaleLine> SaleLines { get; set; } = null!;
    public DbSet<SalesReturn> SalesReturns { get; set; } = null!;
    public DbSet<SalesReturnLine> SalesReturnLines { get; set; } = null!;
    public DbSet<StockBalance> StockBalances { get; set; } = null!;
    public DbSet<StockChange> StockChanges { get; set; } = null!;
    public DbSet<StockChangeLine> StockChangeLines { get; set; } = null!;
    public DbSet<StockMovement> StockMovements { get; set; } = null!;
    public DbSet<SuggestionConversion> SuggestionConversions { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<UserPermission> UserPermissions { get; set; } = null!;
    public DbSet<UserProfile> UserProfiles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ASP.NET Core Identity tables are kept in dbo; application tables retain
        // the schemas declared by the entity attributes (ss or dbo).
        modelBuilder.Entity<UserProfile>()
            .HasOne<IdentityUser<Guid>>()
            .WithOne()
            .HasForeignKey<UserProfile>(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);


        // FK_AccountAllocation_Credit
        modelBuilder.Entity<AccountAllocation>()
            .HasOne<AccountEntry>()
            .WithMany()
            .HasForeignKey("CreditEntryId")
            .HasPrincipalKey("AccountEntryId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_AccountAllocation_Debit
        modelBuilder.Entity<AccountAllocation>()
            .HasOne<AccountEntry>()
            .WithMany()
            .HasForeignKey("DebitEntryId")
            .HasPrincipalKey("AccountEntryId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_AccountAllocation_Request
        modelBuilder.Entity<AccountAllocation>()
            .HasOne<IdempotencyRequest>()
            .WithMany()
            .HasForeignKey("RequestId")
            .HasPrincipalKey("RequestId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_AccountAllocation_Reversed
        modelBuilder.Entity<AccountAllocation>()
            .HasOne<AccountAllocation>()
            .WithMany()
            .HasForeignKey("ReversesAllocationId")
            .HasPrincipalKey("AllocationId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_AccountAllocation_User
        modelBuilder.Entity<AccountAllocation>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("ActorUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_AccountEntry_Customer
        modelBuilder.Entity<AccountEntry>()
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey("CustomerId")
            .HasPrincipalKey("CustomerId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_AccountEntry_Posting
        modelBuilder.Entity<AccountEntry>()
            .HasOne<Posting>()
            .WithMany()
            .HasForeignKey("PostingId")
            .HasPrincipalKey("PostingId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_AccountEntry_Supplier
        modelBuilder.Entity<AccountEntry>()
            .HasOne<Supplier>()
            .WithMany()
            .HasForeignKey("SupplierId")
            .HasPrincipalKey("SupplierId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Chat_User
        modelBuilder.Entity<AiChatLog>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("UserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_ToolCall_Chat
        modelBuilder.Entity<AiToolCallLog>()
            .HasOne<AiChatLog>()
            .WithMany()
            .HasForeignKey("AiChatLogId")
            .HasPrincipalKey("AiChatLogId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_ToolCall_Forecast
        modelBuilder.Entity<AiToolCallLog>()
            .HasOne<ForecastRun>()
            .WithMany()
            .HasForeignKey("ForecastRunId")
            .HasPrincipalKey("ForecastRunId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_ToolCall_Posting
        modelBuilder.Entity<AiToolCallLog>()
            .HasOne<Posting>()
            .WithMany()
            .HasForeignKey("SourcePostingId")
            .HasPrincipalKey("PostingId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_ToolCall_Suggestion
        modelBuilder.Entity<AiToolCallLog>()
            .HasOne<ReorderSuggestion>()
            .WithMany()
            .HasForeignKey("SuggestionId")
            .HasPrincipalKey("SuggestionId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Alert_Batch
        modelBuilder.Entity<Alert>()
            .HasOne<ProductBatch>()
            .WithMany()
            .HasForeignKey("ProductBatchId", "ProductId")
            .HasPrincipalKey("ProductBatchId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Alert_Job
        modelBuilder.Entity<Alert>()
            .HasOne<JobRun>()
            .WithMany()
            .HasForeignKey("JobRunId")
            .HasPrincipalKey("JobRunId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Alert_Product
        modelBuilder.Entity<Alert>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey("ProductId")
            .HasPrincipalKey("ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Alert_User
        modelBuilder.Entity<Alert>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("AcknowledgedByUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Approval_User
        modelBuilder.Entity<Approval>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("ReviewedByUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Approval_Version
        modelBuilder.Entity<Approval>()
            .HasOne<DocumentVersion>()
            .WithMany()
            .HasForeignKey("DocumentId", "VersionNo")
            .HasPrincipalKey("DocumentId", "VersionNo")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Audit_Actor
        modelBuilder.Entity<AuditLog>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("ActorUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Audit_Document
        modelBuilder.Entity<AuditLog>()
            .HasOne<Document>()
            .WithMany()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Audit_Import
        modelBuilder.Entity<AuditLog>()
            .HasOne<ImportBatch>()
            .WithMany()
            .HasForeignKey("ImportBatchId")
            .HasPrincipalKey("ImportBatchId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Audit_Job
        modelBuilder.Entity<AuditLog>()
            .HasOne<JobRun>()
            .WithMany()
            .HasForeignKey("JobRunId")
            .HasPrincipalKey("JobRunId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Audit_Posting
        modelBuilder.Entity<AuditLog>()
            .HasOne<Posting>()
            .WithMany()
            .HasForeignKey("PostingId")
            .HasPrincipalKey("PostingId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Audit_Product
        modelBuilder.Entity<AuditLog>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey("ProductId")
            .HasPrincipalKey("ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Audit_TargetUser
        modelBuilder.Entity<AuditLog>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("TargetUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Settings_User
        modelBuilder.Entity<BusinessSettings>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("UpdatedByUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Category_Parent
        modelBuilder.Entity<Category>()
            .HasOne<Category>()
            .WithMany()
            .HasForeignKey("ParentCategoryId")
            .HasPrincipalKey("CategoryId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Document_Request
        modelBuilder.Entity<Document>()
            .HasOne<IdempotencyRequest>()
            .WithMany()
            .HasForeignKey("CreationRequestId")
            .HasPrincipalKey("RequestId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Document_User
        modelBuilder.Entity<Document>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("CreatedByUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_DocumentLine_Document
        modelBuilder.Entity<DocumentLine>()
            .HasOne<Document>()
            .WithMany()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_DocumentLine_Import
        modelBuilder.Entity<DocumentLine>()
            .HasOne<ImportBatch>()
            .WithMany()
            .HasForeignKey("ImportBatchId")
            .HasPrincipalKey("ImportBatchId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_DocumentLine_Product
        modelBuilder.Entity<DocumentLine>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey("ProductId")
            .HasPrincipalKey("ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_DocumentVersion_Document
        modelBuilder.Entity<DocumentVersion>()
            .HasOne<Document>()
            .WithMany()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_DocumentVersion_User
        modelBuilder.Entity<DocumentVersion>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("CapturedByUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Metric_Run
        modelBuilder.Entity<ForecastMetric>()
            .HasOne<ForecastRun>()
            .WithMany()
            .HasForeignKey("ForecastRunId")
            .HasPrincipalKey("ForecastRunId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Observation_Run
        modelBuilder.Entity<ForecastObservation>()
            .HasOne<ForecastRun>()
            .WithMany()
            .HasForeignKey("ForecastRunId")
            .HasPrincipalKey("ForecastRunId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_ForecastPoint_Run
        modelBuilder.Entity<ForecastPoint>()
            .HasOne<ForecastRun>()
            .WithMany()
            .HasForeignKey("ForecastRunId")
            .HasPrincipalKey("ForecastRunId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Forecast_Job
        modelBuilder.Entity<ForecastRun>()
            .HasOne<JobRun>()
            .WithMany()
            .HasForeignKey("JobRunId")
            .HasPrincipalKey("JobRunId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Forecast_Product
        modelBuilder.Entity<ForecastRun>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey("ProductId")
            .HasPrincipalKey("ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_GRN_Document
        modelBuilder.Entity<GoodsReceipt>()
            .HasOne<Document>()
            .WithMany()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_GRN_PO
        modelBuilder.Entity<GoodsReceipt>()
            .HasOne<PurchaseOrder>()
            .WithMany()
            .HasForeignKey("PurchaseOrderId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_GRNLine_Batch
        modelBuilder.Entity<GoodsReceiptLine>()
            .HasOne<ProductBatch>()
            .WithMany()
            .HasForeignKey("ProductBatchId", "ProductId")
            .HasPrincipalKey("ProductBatchId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_GRNLine_GRN
        modelBuilder.Entity<GoodsReceiptLine>()
            .HasOne<GoodsReceipt>()
            .WithMany()
            .HasForeignKey("DocumentId", "PurchaseOrderId")
            .HasPrincipalKey("DocumentId", "PurchaseOrderId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_GRNLine_Line
        modelBuilder.Entity<GoodsReceiptLine>()
            .HasOne<DocumentLine>()
            .WithMany()
            .HasForeignKey("LineId", "DocumentId", "ProductId")
            .HasPrincipalKey("LineId", "DocumentId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_GRNLine_POLine
        modelBuilder.Entity<GoodsReceiptLine>()
            .HasOne<PurchaseOrderLine>()
            .WithMany()
            .HasForeignKey("PurchaseOrderLineId", "PurchaseOrderId", "ProductId")
            .HasPrincipalKey("LineId", "DocumentId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_GRNReversal_Document
        modelBuilder.Entity<GoodsReceiptReversal>()
            .HasOne<Document>()
            .WithMany()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_GRNReversal_Original
        modelBuilder.Entity<GoodsReceiptReversal>()
            .HasOne<GoodsReceipt>()
            .WithMany()
            .HasForeignKey("OriginalGoodsReceiptId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_GRNReversal_Posting
        modelBuilder.Entity<GoodsReceiptReversal>()
            .HasOne<Posting>()
            .WithMany()
            .HasForeignKey("PostingId", "DocumentId")
            .HasPrincipalKey("PostingId", "DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Request_User
        modelBuilder.Entity<IdempotencyRequest>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("ActorUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Import_Confirmer
        modelBuilder.Entity<ImportBatch>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("ConfirmedByUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Import_Creator
        modelBuilder.Entity<ImportBatch>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("CreatedByUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Import_Request
        modelBuilder.Entity<ImportBatch>()
            .HasOne<IdempotencyRequest>()
            .WithMany()
            .HasForeignKey("RequestId")
            .HasPrincipalKey("RequestId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Job_Request
        modelBuilder.Entity<JobRun>()
            .HasOne<IdempotencyRequest>()
            .WithMany()
            .HasForeignKey("RequestId")
            .HasPrincipalKey("RequestId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Job_User
        modelBuilder.Entity<JobRun>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("RequestedByUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Notification_Alert
        modelBuilder.Entity<NotificationDelivery>()
            .HasOne<Alert>()
            .WithMany()
            .HasForeignKey("AlertId")
            .HasPrincipalKey("AlertId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Notification_Job
        modelBuilder.Entity<NotificationDelivery>()
            .HasOne<JobRun>()
            .WithMany()
            .HasForeignKey("JobRunId")
            .HasPrincipalKey("JobRunId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_OpeningAccount_Customer
        modelBuilder.Entity<OpeningAccountBalance>()
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey("CustomerId")
            .HasPrincipalKey("CustomerId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_OpeningAccount_Document
        modelBuilder.Entity<OpeningAccountBalance>()
            .HasOne<Document>()
            .WithMany()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_OpeningAccount_Import
        modelBuilder.Entity<OpeningAccountBalance>()
            .HasOne<ImportBatch>()
            .WithMany()
            .HasForeignKey("ImportBatchId")
            .HasPrincipalKey("ImportBatchId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_OpeningAccount_Supplier
        modelBuilder.Entity<OpeningAccountBalance>()
            .HasOne<Supplier>()
            .WithMany()
            .HasForeignKey("SupplierId")
            .HasPrincipalKey("SupplierId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Payment_Customer
        modelBuilder.Entity<Payment>()
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey("CustomerId")
            .HasPrincipalKey("CustomerId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Payment_Document
        modelBuilder.Entity<Payment>()
            .HasOne<Document>()
            .WithMany()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Payment_PO
        modelBuilder.Entity<Payment>()
            .HasOne<PurchaseOrder>()
            .WithMany()
            .HasForeignKey("PurchaseOrderId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Payment_Supplier
        modelBuilder.Entity<Payment>()
            .HasOne<Supplier>()
            .WithMany()
            .HasForeignKey("SupplierId")
            .HasPrincipalKey("SupplierId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_PaymentReversal_Document
        modelBuilder.Entity<PaymentReversal>()
            .HasOne<Document>()
            .WithMany()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_PaymentReversal_Original
        modelBuilder.Entity<PaymentReversal>()
            .HasOne<Payment>()
            .WithMany()
            .HasForeignKey("OriginalPaymentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_PaymentReversal_Posting
        modelBuilder.Entity<PaymentReversal>()
            .HasOne<Posting>()
            .WithMany()
            .HasForeignKey("PostingId", "DocumentId")
            .HasPrincipalKey("PostingId", "DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_PaymentSettlement_Payment
        modelBuilder.Entity<PaymentSettlementEvent>()
            .HasOne<Payment>()
            .WithMany()
            .HasForeignKey("PaymentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_PaymentSettlement_User
        modelBuilder.Entity<PaymentSettlementEvent>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("RecordedByUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Posting_Request
        modelBuilder.Entity<Posting>()
            .HasOne<IdempotencyRequest>()
            .WithMany()
            .HasForeignKey("RequestId")
            .HasPrincipalKey("RequestId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Posting_User
        modelBuilder.Entity<Posting>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("ActorUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Posting_Version
        modelBuilder.Entity<Posting>()
            .HasOne<DocumentVersion>()
            .WithMany()
            .HasForeignKey("DocumentId", "VersionNo")
            .HasPrincipalKey("DocumentId", "VersionNo")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Product_Category
        modelBuilder.Entity<Product>()
            .HasOne<Category>()
            .WithMany()
            .HasForeignKey("CategoryId")
            .HasPrincipalKey("CategoryId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Product_Supplier
        modelBuilder.Entity<Product>()
            .HasOne<Supplier>()
            .WithMany()
            .HasForeignKey("PreferredSupplierId")
            .HasPrincipalKey("SupplierId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Batch_Product
        modelBuilder.Entity<ProductBatch>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey("ProductId")
            .HasPrincipalKey("ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_ProductBalance_Posting
        modelBuilder.Entity<ProductStockBalance>()
            .HasOne<Posting>()
            .WithMany()
            .HasForeignKey("LastPostingId")
            .HasPrincipalKey("PostingId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_ProductBalance_Product
        modelBuilder.Entity<ProductStockBalance>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey("ProductId")
            .HasPrincipalKey("ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_PO_ClosingUser
        modelBuilder.Entity<PurchaseOrder>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("ClosedOrCancelledByUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_PO_Document
        modelBuilder.Entity<PurchaseOrder>()
            .HasOne<Document>()
            .WithMany()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_PO_Supplier
        modelBuilder.Entity<PurchaseOrder>()
            .HasOne<Supplier>()
            .WithMany()
            .HasForeignKey("SupplierId")
            .HasPrincipalKey("SupplierId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_POLine_Line
        modelBuilder.Entity<PurchaseOrderLine>()
            .HasOne<DocumentLine>()
            .WithMany()
            .HasForeignKey("LineId", "DocumentId", "ProductId")
            .HasPrincipalKey("LineId", "DocumentId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_POLine_PO
        modelBuilder.Entity<PurchaseOrderLine>()
            .HasOne<PurchaseOrder>()
            .WithMany()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Suggestion_Forecast
        modelBuilder.Entity<ReorderSuggestion>()
            .HasOne<ForecastRun>()
            .WithMany()
            .HasForeignKey("ForecastRunId", "ProductId")
            .HasPrincipalKey("ForecastRunId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Suggestion_OutcomeUser
        modelBuilder.Entity<ReorderSuggestion>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("OutcomeByUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Suggestion_Product
        modelBuilder.Entity<ReorderSuggestion>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey("ProductId")
            .HasPrincipalKey("ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Suggestion_Supplier
        modelBuilder.Entity<ReorderSuggestion>()
            .HasOne<Supplier>()
            .WithMany()
            .HasForeignKey("SupplierId")
            .HasPrincipalKey("SupplierId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Sale_Customer
        modelBuilder.Entity<Sale>()
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey("CustomerId")
            .HasPrincipalKey("CustomerId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Sale_Document
        modelBuilder.Entity<Sale>()
            .HasOne<Document>()
            .WithMany()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_SaleAllocation_Batch
        modelBuilder.Entity<SaleBatchAllocation>()
            .HasOne<ProductBatch>()
            .WithMany()
            .HasForeignKey("ProductBatchId", "ProductId")
            .HasPrincipalKey("ProductBatchId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_SaleAllocation_Line
        modelBuilder.Entity<SaleBatchAllocation>()
            .HasOne<SaleLine>()
            .WithMany()
            .HasForeignKey("SaleLineId", "SaleId", "ProductId")
            .HasPrincipalKey("LineId", "DocumentId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_SaleLine_Line
        modelBuilder.Entity<SaleLine>()
            .HasOne<DocumentLine>()
            .WithMany()
            .HasForeignKey("LineId", "DocumentId", "ProductId")
            .HasPrincipalKey("LineId", "DocumentId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_SaleLine_Sale
        modelBuilder.Entity<SaleLine>()
            .HasOne<Sale>()
            .WithMany()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Return_Document
        modelBuilder.Entity<SalesReturn>()
            .HasOne<Document>()
            .WithMany()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Return_Sale
        modelBuilder.Entity<SalesReturn>()
            .HasOne<Sale>()
            .WithMany()
            .HasForeignKey("OriginalSaleId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_SalesReturn_Posting
        modelBuilder.Entity<SalesReturn>()
            .HasOne<Posting>()
            .WithMany()
            .HasForeignKey("PostingId", "DocumentId")
            .HasPrincipalKey("PostingId", "DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_ReturnLine_Allocation
        modelBuilder.Entity<SalesReturnLine>()
            .HasOne<SaleBatchAllocation>()
            .WithMany()
            .HasForeignKey("OriginalAllocationId", "OriginalSaleId", "ProductId")
            .HasPrincipalKey("AllocationId", "SaleId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_ReturnLine_Line
        modelBuilder.Entity<SalesReturnLine>()
            .HasOne<DocumentLine>()
            .WithMany()
            .HasForeignKey("LineId", "DocumentId", "ProductId")
            .HasPrincipalKey("LineId", "DocumentId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_ReturnLine_Return
        modelBuilder.Entity<SalesReturnLine>()
            .HasOne<SalesReturn>()
            .WithMany()
            .HasForeignKey("DocumentId", "OriginalSaleId")
            .HasPrincipalKey("DocumentId", "OriginalSaleId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_StockBalance_Batch
        modelBuilder.Entity<StockBalance>()
            .HasOne<ProductBatch>()
            .WithMany()
            .HasForeignKey("ProductBatchId", "ProductId")
            .HasPrincipalKey("ProductBatchId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_StockBalance_Posting
        modelBuilder.Entity<StockBalance>()
            .HasOne<Posting>()
            .WithMany()
            .HasForeignKey("LastPostingId")
            .HasPrincipalKey("PostingId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_StockChange_Document
        modelBuilder.Entity<StockChange>()
            .HasOne<Document>()
            .WithMany()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_StockChangeLine_Batch
        modelBuilder.Entity<StockChangeLine>()
            .HasOne<ProductBatch>()
            .WithMany()
            .HasForeignKey("ProductBatchId", "ProductId")
            .HasPrincipalKey("ProductBatchId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_StockChangeLine_Header
        modelBuilder.Entity<StockChangeLine>()
            .HasOne<StockChange>()
            .WithMany()
            .HasForeignKey("DocumentId")
            .HasPrincipalKey("DocumentId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_StockChangeLine_Line
        modelBuilder.Entity<StockChangeLine>()
            .HasOne<DocumentLine>()
            .WithMany()
            .HasForeignKey("LineId", "DocumentId", "ProductId")
            .HasPrincipalKey("LineId", "DocumentId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Movement_Batch
        modelBuilder.Entity<StockMovement>()
            .HasOne<ProductBatch>()
            .WithMany()
            .HasForeignKey("ProductBatchId", "ProductId")
            .HasPrincipalKey("ProductBatchId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Movement_Posting
        modelBuilder.Entity<StockMovement>()
            .HasOne<Posting>()
            .WithMany()
            .HasForeignKey("PostingId")
            .HasPrincipalKey("PostingId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Movement_Reversed
        modelBuilder.Entity<StockMovement>()
            .HasOne<StockMovement>()
            .WithMany()
            .HasForeignKey("ReversesMovementId")
            .HasPrincipalKey("StockMovementId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Movement_Source
        modelBuilder.Entity<StockMovement>()
            .HasOne<DocumentLine>()
            .WithMany()
            .HasForeignKey("SourceLineId", "ProductId")
            .HasPrincipalKey("LineId", "ProductId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Conversion_POLine
        modelBuilder.Entity<SuggestionConversion>()
            .HasOne<PurchaseOrderLine>()
            .WithMany()
            .HasForeignKey("PurchaseOrderLineId")
            .HasPrincipalKey("LineId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Conversion_ReleaseUser
        modelBuilder.Entity<SuggestionConversion>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("ReleasedByUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Conversion_Request
        modelBuilder.Entity<SuggestionConversion>()
            .HasOne<IdempotencyRequest>()
            .WithMany()
            .HasForeignKey("RequestId")
            .HasPrincipalKey("RequestId")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Conversion_Suggestion
        modelBuilder.Entity<SuggestionConversion>()
            .HasOne<ReorderSuggestion>()
            .WithMany()
            .HasForeignKey("SuggestionId", "OpportunityKey")
            .HasPrincipalKey("SuggestionId", "OpportunityKey")
            .OnDelete(DeleteBehavior.NoAction);

        // FK_Conversion_User
        modelBuilder.Entity<SuggestionConversion>()
            .HasOne<UserProfile>()
            .WithMany()
            .HasForeignKey("ReviewedByUserId")
            .HasPrincipalKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);

    }
}
