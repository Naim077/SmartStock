using AIMS.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AIMS.DataAccess
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 1. Identity & Business Settings
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public DbSet<BusinessSettings> BusinessSettings { get; set; } = null!;

        // 2. Product Catalog
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductBatch> ProductBatches { get; set; } = null!;

        // 3. Shared Documents, Versions & Posting
        public DbSet<IdempotencyRequest> IdempotencyRequests { get; set; } = null!;
        public DbSet<ImportBatch> ImportBatches { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<DocumentVersion> DocumentVersions { get; set; } = null!;
        public DbSet<Approval> Approvals { get; set; } = null!;
        public DbSet<Posting> Postings { get; set; } = null!;
        public DbSet<DocumentLine> DocumentLines { get; set; } = null!;

        // 4. Purchasing & Receiving
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; } = null!;
        public DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; } = null!;
        public DbSet<GoodsReceipt> GoodsReceipts { get; set; } = null!;
        public DbSet<GoodsReceiptLine> GoodsReceiptLines { get; set; } = null!;
        public DbSet<GoodsReceiptReversal> GoodsReceiptReversals { get; set; } = null!;

        // 5. Sales & Returns
        public DbSet<Sale> Sales { get; set; } = null!;
        public DbSet<SaleLine> SaleLines { get; set; } = null!;
        public DbSet<SaleBatchAllocation> SaleBatchAllocations { get; set; } = null!;
        public DbSet<SalesReturn> SalesReturns { get; set; } = null!;
        public DbSet<SalesReturnLine> SalesReturnLines { get; set; } = null!;

        // 6. Stock Management
        public DbSet<StockChange> StockChanges { get; set; } = null!;
        public DbSet<StockChangeLine> StockChangeLines { get; set; } = null!;
        public DbSet<ProductStockBalance> ProductStockBalances { get; set; } = null!;
        public DbSet<StockBalance> StockBalances { get; set; } = null!;
        public DbSet<StockMovement> StockMovements { get; set; } = null!;

        // 7. Accounts & Payments
        public DbSet<OpeningAccountBalance> OpeningAccountBalances { get; set; } = null!;
        public DbSet<AccountEntry> AccountEntries { get; set; } = null!;
        public DbSet<AccountAllocation> AccountAllocations { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<PaymentReversal> PaymentReversals { get; set; } = null!;
        public DbSet<PaymentSettlementEvent> PaymentSettlementEvents { get; set; } = null!;

        // 8. Background Jobs, Forecasting & Replenishment
        public DbSet<JobRun> JobRuns { get; set; } = null!;
        public DbSet<ForecastRun> ForecastRuns { get; set; } = null!;
        public DbSet<ForecastObservation> ForecastObservations { get; set; } = null!;
        public DbSet<ForecastMetric> ForecastMetrics { get; set; } = null!;
        public DbSet<ForecastPoint> ForecastPoints { get; set; } = null!;
        public DbSet<ReorderSuggestion> ReorderSuggestions { get; set; } = null!;
        public DbSet<SuggestionConversion> SuggestionConversions { get; set; } = null!;

        // 9. AI Assistant, Audit & Notifications
        public DbSet<AiChatLog> AiChatLogs { get; set; } = null!;
        public DbSet<AiToolCallLog> AiToolCallLogs { get; set; } = null!;
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;
        public DbSet<Alert> Alerts { get; set; } = null!;
        public DbSet<NotificationDelivery> NotificationDeliveries { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Every business table in SmartStock-Database.sql belongs to schema ss.
            // ASP.NET Core Identity tables remain in dbo.
            modelBuilder.Entity<UserProfile>().ToTable("UserProfile", "ss");
            modelBuilder.Entity<BusinessSettings>().ToTable("BusinessSettings", "ss");
            modelBuilder.Entity<Category>().ToTable("Category", "ss");
            modelBuilder.Entity<Supplier>().ToTable("Supplier", "ss");
            modelBuilder.Entity<Customer>().ToTable("Customer", "ss");
            modelBuilder.Entity<Product>().ToTable("Product", "ss");
            modelBuilder.Entity<ProductBatch>().ToTable("ProductBatch", "ss");
            modelBuilder.Entity<IdempotencyRequest>().ToTable("IdempotencyRequest", "ss");
            modelBuilder.Entity<ImportBatch>().ToTable("ImportBatch", "ss");
            modelBuilder.Entity<Document>().ToTable("Document", "ss");
            modelBuilder.Entity<DocumentVersion>().ToTable("DocumentVersion", "ss");
            modelBuilder.Entity<Approval>().ToTable("Approval", "ss");
            modelBuilder.Entity<Posting>().ToTable("Posting", "ss");
            modelBuilder.Entity<DocumentLine>().ToTable("DocumentLine", "ss");
            modelBuilder.Entity<PurchaseOrder>().ToTable("PurchaseOrder", "ss");
            modelBuilder.Entity<PurchaseOrderLine>().ToTable("PurchaseOrderLine", "ss");
            modelBuilder.Entity<GoodsReceipt>().ToTable("GoodsReceipt", "ss");
            modelBuilder.Entity<GoodsReceiptLine>().ToTable("GoodsReceiptLine", "ss");
            modelBuilder.Entity<GoodsReceiptReversal>().ToTable("GoodsReceiptReversal", "ss");
            modelBuilder.Entity<Sale>().ToTable("Sale", "ss");
            modelBuilder.Entity<SaleLine>().ToTable("SaleLine", "ss");
            modelBuilder.Entity<SaleBatchAllocation>().ToTable("SaleBatchAllocation", "ss");
            modelBuilder.Entity<SalesReturn>().ToTable("SalesReturn", "ss");
            modelBuilder.Entity<SalesReturnLine>().ToTable("SalesReturnLine", "ss");
            modelBuilder.Entity<StockChange>().ToTable("StockChange", "ss");
            modelBuilder.Entity<StockChangeLine>().ToTable("StockChangeLine", "ss");
            modelBuilder.Entity<ProductStockBalance>().ToTable("ProductStockBalance", "ss");
            modelBuilder.Entity<StockBalance>().ToTable("StockBalance", "ss");
            modelBuilder.Entity<StockMovement>().ToTable("StockMovement", "ss");
            modelBuilder.Entity<OpeningAccountBalance>().ToTable("OpeningAccountBalance", "ss");
            modelBuilder.Entity<AccountEntry>().ToTable("AccountEntry", "ss");
            modelBuilder.Entity<AccountAllocation>().ToTable("AccountAllocation", "ss");
            modelBuilder.Entity<Payment>().ToTable("Payment", "ss");
            modelBuilder.Entity<PaymentReversal>().ToTable("PaymentReversal", "ss");
            modelBuilder.Entity<PaymentSettlementEvent>().ToTable("PaymentSettlementEvent", "ss");
            modelBuilder.Entity<JobRun>().ToTable("JobRun", "ss");
            modelBuilder.Entity<ForecastRun>().ToTable("ForecastRun", "ss");
            modelBuilder.Entity<ForecastObservation>().ToTable("ForecastObservation", "ss");
            modelBuilder.Entity<ForecastMetric>().ToTable("ForecastMetric", "ss");
            modelBuilder.Entity<ForecastPoint>().ToTable("ForecastPoint", "ss");
            modelBuilder.Entity<ReorderSuggestion>().ToTable("ReorderSuggestion", "ss");
            modelBuilder.Entity<SuggestionConversion>().ToTable("SuggestionConversion", "ss");
            modelBuilder.Entity<AiChatLog>().ToTable("AiChatLog", "ss");
            modelBuilder.Entity<AiToolCallLog>().ToTable("AiToolCallLog", "ss");
            modelBuilder.Entity<AuditLog>().ToTable("AuditLog", "ss");
            modelBuilder.Entity<Alert>().ToTable("Alert", "ss");
            modelBuilder.Entity<NotificationDelivery>().ToTable("NotificationDelivery", "ss");

            // =====================================================
            // 1. Primary Keys (match SmartStock-Database.sql)
            // =====================================================
            modelBuilder.Entity<UserProfile>().HasKey(x => x.UserId);
            modelBuilder.Entity<BusinessSettings>().HasKey(x => x.SettingsId);
            modelBuilder.Entity<Category>().HasKey(x => x.CategoryId);
            modelBuilder.Entity<Supplier>().HasKey(x => x.SupplierId);
            modelBuilder.Entity<Customer>().HasKey(x => x.CustomerId);
            modelBuilder.Entity<Product>().HasKey(x => x.ProductId);
            modelBuilder.Entity<ProductBatch>().HasKey(x => x.ProductBatchId);
            modelBuilder.Entity<IdempotencyRequest>().HasKey(x => x.RequestId);
            modelBuilder.Entity<ImportBatch>().HasKey(x => x.ImportBatchId);
            modelBuilder.Entity<Document>().HasKey(x => x.DocumentId);
            modelBuilder.Entity<DocumentVersion>().HasKey(x => new { x.DocumentId, x.VersionNo });
            modelBuilder.Entity<Approval>().HasKey(x => x.ApprovalId);
            modelBuilder.Entity<Posting>().HasKey(x => x.PostingId);
            modelBuilder.Entity<DocumentLine>().HasKey(x => x.LineId);
            modelBuilder.Entity<PurchaseOrder>().HasKey(x => x.DocumentId);
            modelBuilder.Entity<PurchaseOrderLine>().HasKey(x => x.LineId);
            modelBuilder.Entity<GoodsReceipt>().HasKey(x => x.DocumentId);
            modelBuilder.Entity<GoodsReceiptLine>().HasKey(x => x.LineId);
            modelBuilder.Entity<GoodsReceiptReversal>().HasKey(x => x.DocumentId);
            modelBuilder.Entity<Sale>().HasKey(x => x.DocumentId);
            modelBuilder.Entity<SaleLine>().HasKey(x => x.LineId);
            modelBuilder.Entity<SaleBatchAllocation>().HasKey(x => x.AllocationId);
            modelBuilder.Entity<SalesReturn>().HasKey(x => x.DocumentId);
            modelBuilder.Entity<SalesReturnLine>().HasKey(x => x.LineId);
            modelBuilder.Entity<StockChange>().HasKey(x => x.DocumentId);
            modelBuilder.Entity<StockChangeLine>().HasKey(x => x.LineId);
            modelBuilder.Entity<ProductStockBalance>().HasKey(x => x.ProductId);
            modelBuilder.Entity<StockBalance>().HasKey(x => new { x.ProductBatchId, x.Bucket });
            modelBuilder.Entity<StockMovement>().HasKey(x => x.StockMovementId);
            modelBuilder.Entity<OpeningAccountBalance>().HasKey(x => x.DocumentId);
            modelBuilder.Entity<AccountEntry>().HasKey(x => x.AccountEntryId);
            modelBuilder.Entity<AccountAllocation>().HasKey(x => x.AllocationId);
            modelBuilder.Entity<Payment>().HasKey(x => x.DocumentId);
            modelBuilder.Entity<PaymentReversal>().HasKey(x => x.DocumentId);
            modelBuilder.Entity<PaymentSettlementEvent>().HasKey(x => x.EventId);
            modelBuilder.Entity<JobRun>().HasKey(x => x.JobRunId);
            modelBuilder.Entity<ForecastRun>().HasKey(x => x.ForecastRunId);
            modelBuilder.Entity<ForecastObservation>().HasKey(x => new { x.ForecastRunId, x.SnapshotPhase, x.ObservationDate });
            modelBuilder.Entity<ForecastMetric>().HasKey(x => new { x.ForecastRunId, x.Method });
            modelBuilder.Entity<ForecastPoint>().HasKey(x => new { x.ForecastRunId, x.HorizonDays });
            modelBuilder.Entity<ReorderSuggestion>().HasKey(x => x.SuggestionId);
            modelBuilder.Entity<SuggestionConversion>().HasKey(x => x.ConversionId);
            modelBuilder.Entity<AiChatLog>().HasKey(x => x.AiChatLogId);
            modelBuilder.Entity<AiToolCallLog>().HasKey(x => x.ToolCallId);
            modelBuilder.Entity<AuditLog>().HasKey(x => x.AuditLogId);
            modelBuilder.Entity<Alert>().HasKey(x => x.AlertId);
            modelBuilder.Entity<NotificationDelivery>().HasKey(x => x.DeliveryId);

            // =====================================================
            // 2. Alternate Keys used by composite SQL foreign keys
            // =====================================================
            modelBuilder.Entity<ProductBatch>()
                .HasAlternateKey(x => new { x.ProductBatchId, x.ProductId })
                .HasName("UQ_Batch_Product");

            modelBuilder.Entity<Posting>()
                .HasAlternateKey(x => new { x.PostingId, x.DocumentId })
                .HasName("UQ_Posting_IdDocument");

            modelBuilder.Entity<DocumentLine>()
                .HasAlternateKey(x => new { x.LineId, x.DocumentId, x.ProductId })
                .HasName("UQ_DocumentLine_Identity");
            modelBuilder.Entity<DocumentLine>()
                .HasAlternateKey(x => new { x.LineId, x.ProductId })
                .HasName("UQ_DocumentLine_Product");

            modelBuilder.Entity<PurchaseOrderLine>()
                .HasAlternateKey(x => new { x.LineId, x.DocumentId, x.ProductId })
                .HasName("UQ_POLine_Identity");

            modelBuilder.Entity<GoodsReceipt>()
                .HasAlternateKey(x => new { x.DocumentId, x.PurchaseOrderId })
                .HasName("UQ_GRN_PO");

            modelBuilder.Entity<SaleLine>()
                .HasAlternateKey(x => new { x.LineId, x.DocumentId, x.ProductId })
                .HasName("UQ_SaleLine_Identity");

            modelBuilder.Entity<SaleBatchAllocation>()
                .HasAlternateKey(x => new { x.AllocationId, x.SaleId, x.ProductId })
                .HasName("UQ_SaleAllocation_Identity");

            modelBuilder.Entity<SalesReturn>()
                .HasAlternateKey(x => new { x.DocumentId, x.OriginalSaleId })
                .HasName("UQ_Return_Sale");

            modelBuilder.Entity<ForecastRun>()
                .HasAlternateKey(x => new { x.ForecastRunId, x.ProductId })
                .HasName("UQ_Forecast_Product");

            modelBuilder.Entity<ReorderSuggestion>()
                .HasAlternateKey(x => new { x.SuggestionId, x.OpportunityKey })
                .HasName("UQ_Suggestion_Opportunity");

            // =====================================================
            // 3. Identity, Settings & Catalog Relationships
            // =====================================================
            modelBuilder.Entity<UserProfile>()
                .HasOne(x => x.IdentityUser)
                .WithOne(x => x.Profile)
                .HasForeignKey<UserProfile>(x => x.UserId)
                .HasConstraintName("FK_UserProfile_IdentityUser")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BusinessSettings>()
                .HasOne(x => x.UpdatedByUser)
                .WithMany(x => x.BusinessSettings)
                .HasForeignKey(x => x.UpdatedByUserId)
                .HasConstraintName("FK_Settings_User")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Category>()
                .HasOne(x => x.ParentCategory)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.ParentCategoryId)
                .HasConstraintName("FK_Category_Parent")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .HasConstraintName("FK_Product_Category")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.PreferredSupplier)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.PreferredSupplierId)
                .HasConstraintName("FK_Product_Supplier")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductBatch>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductBatches)
                .HasForeignKey(x => x.ProductId)
                .HasConstraintName("FK_Batch_Product")
                .OnDelete(DeleteBehavior.NoAction);

            // =====================================================
            // 4. Idempotency, Imports & Shared Document Relationships
            // =====================================================
            modelBuilder.Entity<IdempotencyRequest>()
                .HasOne(x => x.ActorUser)
                .WithMany(x => x.IdempotencyRequests)
                .HasForeignKey(x => x.ActorUserId)
                .HasConstraintName("FK_Request_User")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ImportBatch>()
                .HasOne(x => x.CreatedByUser)
                .WithMany(x => x.ImportBatches)
                .HasForeignKey(x => x.CreatedByUserId)
                .HasConstraintName("FK_Import_Creator")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ImportBatch>()
                .HasOne(x => x.ConfirmedByUser)
                .WithMany(x => x.ImportBatchesAsConfirmedByUser)
                .HasForeignKey(x => x.ConfirmedByUserId)
                .HasConstraintName("FK_Import_Confirmer")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ImportBatch>()
                .HasOne(x => x.IdempotencyRequest)
                .WithMany(x => x.ImportBatches)
                .HasForeignKey(x => x.RequestId)
                .HasConstraintName("FK_Import_Request")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Document>()
                .HasOne(x => x.CreatedByUser)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.CreatedByUserId)
                .HasConstraintName("FK_Document_User")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Document>()
                .HasOne(x => x.CreationRequest)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.CreationRequestId)
                .HasConstraintName("FK_Document_Request")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DocumentVersion>()
                .HasOne(x => x.Document)
                .WithMany(x => x.DocumentVersions)
                .HasForeignKey(x => x.DocumentId)
                .HasConstraintName("FK_DocumentVersion_Document")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DocumentVersion>()
                .HasOne(x => x.CapturedByUser)
                .WithMany(x => x.DocumentVersions)
                .HasForeignKey(x => x.CapturedByUserId)
                .HasConstraintName("FK_DocumentVersion_User")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Approval>()
                .HasOne(x => x.DocumentVersion)
                .WithMany(x => x.Approvals)
                .HasForeignKey(x => new { x.DocumentId, x.VersionNo })
                .HasPrincipalKey(x => new { x.DocumentId, x.VersionNo })
                .HasConstraintName("FK_Approval_Version")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Approval>()
                .HasOne(x => x.ReviewedByUser)
                .WithMany(x => x.Approvals)
                .HasForeignKey(x => x.ReviewedByUserId)
                .HasConstraintName("FK_Approval_User")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Posting>()
                .HasOne(x => x.DocumentVersion)
                .WithOne(x => x.Posting)
                .HasForeignKey<Posting>(x => new { x.DocumentId, x.VersionNo })
                .HasPrincipalKey<DocumentVersion>(x => new { x.DocumentId, x.VersionNo })
                .HasConstraintName("FK_Posting_Version")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Posting>()
                .HasOne(x => x.IdempotencyRequest)
                .WithMany(x => x.Postings)
                .HasForeignKey(x => x.RequestId)
                .HasConstraintName("FK_Posting_Request")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Posting>()
                .HasOne(x => x.ActorUser)
                .WithMany(x => x.Postings)
                .HasForeignKey(x => x.ActorUserId)
                .HasConstraintName("FK_Posting_User")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DocumentLine>()
                .HasOne(x => x.Document)
                .WithMany(x => x.DocumentLines)
                .HasForeignKey(x => x.DocumentId)
                .HasConstraintName("FK_DocumentLine_Document")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DocumentLine>()
                .HasOne(x => x.Product)
                .WithMany(x => x.DocumentLines)
                .HasForeignKey(x => x.ProductId)
                .HasConstraintName("FK_DocumentLine_Product")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DocumentLine>()
                .HasOne(x => x.ImportBatch)
                .WithMany(x => x.DocumentLines)
                .HasForeignKey(x => x.ImportBatchId)
                .HasConstraintName("FK_DocumentLine_Import")
                .OnDelete(DeleteBehavior.NoAction);

            // =====================================================
            // 5. Purchasing & Goods Receipt Relationships
            // =====================================================
            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(x => x.Document)
                .WithOne(x => x.PurchaseOrder)
                .HasForeignKey<PurchaseOrder>(x => x.DocumentId)
                .HasConstraintName("FK_PO_Document")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(x => x.Supplier)
                .WithMany(x => x.PurchaseOrders)
                .HasForeignKey(x => x.SupplierId)
                .HasConstraintName("FK_PO_Supplier")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(x => x.ClosedOrCancelledByUser)
                .WithMany(x => x.PurchaseOrders)
                .HasForeignKey(x => x.ClosedOrCancelledByUserId)
                .HasConstraintName("FK_PO_ClosingUser")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrderLine>()
                .HasOne(x => x.DocumentLine)
                .WithOne(x => x.PurchaseOrderLine)
                .HasForeignKey<PurchaseOrderLine>(x => new { x.LineId, x.DocumentId, x.ProductId })
                .HasPrincipalKey<DocumentLine>(x => new { x.LineId, x.DocumentId, x.ProductId })
                .HasConstraintName("FK_POLine_Line")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrderLine>()
                .HasOne(x => x.PurchaseOrder)
                .WithMany(x => x.PurchaseOrderLines)
                .HasForeignKey(x => x.DocumentId)
                .HasConstraintName("FK_POLine_PO")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GoodsReceipt>()
                .HasOne(x => x.Document)
                .WithOne(x => x.GoodsReceipt)
                .HasForeignKey<GoodsReceipt>(x => x.DocumentId)
                .HasConstraintName("FK_GRN_Document")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GoodsReceipt>()
                .HasOne(x => x.PurchaseOrder)
                .WithMany(x => x.GoodsReceipts)
                .HasForeignKey(x => x.PurchaseOrderId)
                .HasConstraintName("FK_GRN_PO")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GoodsReceiptLine>()
                .HasOne(x => x.DocumentLine)
                .WithOne(x => x.GoodsReceiptLine)
                .HasForeignKey<GoodsReceiptLine>(x => new { x.LineId, x.DocumentId, x.ProductId })
                .HasPrincipalKey<DocumentLine>(x => new { x.LineId, x.DocumentId, x.ProductId })
                .HasConstraintName("FK_GRNLine_Line")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GoodsReceiptLine>()
                .HasOne(x => x.GoodsReceipt)
                .WithMany(x => x.GoodsReceiptLines)
                .HasForeignKey(x => new { x.DocumentId, x.PurchaseOrderId })
                .HasPrincipalKey(x => new { x.DocumentId, x.PurchaseOrderId })
                .HasConstraintName("FK_GRNLine_GRN")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GoodsReceiptLine>()
                .HasOne(x => x.PurchaseOrderLine)
                .WithMany(x => x.GoodsReceiptLines)
                .HasForeignKey(x => new { x.PurchaseOrderLineId, x.PurchaseOrderId, x.ProductId })
                .HasPrincipalKey(x => new { x.LineId, x.DocumentId, x.ProductId })
                .HasConstraintName("FK_GRNLine_POLine")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GoodsReceiptLine>()
                .HasOne(x => x.ProductBatch)
                .WithMany(x => x.GoodsReceiptLines)
                .HasForeignKey(x => new { x.ProductBatchId, x.ProductId })
                .HasPrincipalKey(x => new { x.ProductBatchId, x.ProductId })
                .HasConstraintName("FK_GRNLine_Batch")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GoodsReceiptReversal>()
                .HasOne(x => x.Document)
                .WithOne(x => x.GoodsReceiptReversal)
                .HasForeignKey<GoodsReceiptReversal>(x => x.DocumentId)
                .HasConstraintName("FK_GRNReversal_Document")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GoodsReceiptReversal>()
                .HasOne(x => x.OriginalGoodsReceipt)
                .WithOne(x => x.GoodsReceiptReversal)
                .HasForeignKey<GoodsReceiptReversal>(x => x.OriginalGoodsReceiptId)
                .HasConstraintName("FK_GRNReversal_Original")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GoodsReceiptReversal>()
                .HasOne(x => x.Posting)
                .WithOne(x => x.GoodsReceiptReversal)
                .HasForeignKey<GoodsReceiptReversal>(x => new { x.PostingId, x.DocumentId })
                .HasPrincipalKey<Posting>(x => new { x.PostingId, x.DocumentId })
                .HasConstraintName("FK_GRNReversal_Posting")
                .OnDelete(DeleteBehavior.NoAction);

            // =====================================================
            // 6. Sales, Allocations & Returns Relationships
            // =====================================================
            modelBuilder.Entity<Sale>()
                .HasOne(x => x.Document)
                .WithOne(x => x.Sale)
                .HasForeignKey<Sale>(x => x.DocumentId)
                .HasConstraintName("FK_Sale_Document")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sale>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.Sales)
                .HasForeignKey(x => x.CustomerId)
                .HasConstraintName("FK_Sale_Customer")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SaleLine>()
                .HasOne(x => x.DocumentLine)
                .WithOne(x => x.SaleLine)
                .HasForeignKey<SaleLine>(x => new { x.LineId, x.DocumentId, x.ProductId })
                .HasPrincipalKey<DocumentLine>(x => new { x.LineId, x.DocumentId, x.ProductId })
                .HasConstraintName("FK_SaleLine_Line")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SaleLine>()
                .HasOne(x => x.Sale)
                .WithMany(x => x.SaleLines)
                .HasForeignKey(x => x.DocumentId)
                .HasConstraintName("FK_SaleLine_Sale")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SaleBatchAllocation>()
                .HasOne(x => x.SaleLine)
                .WithMany(x => x.SaleBatchAllocations)
                .HasForeignKey(x => new { x.SaleLineId, x.SaleId, x.ProductId })
                .HasPrincipalKey(x => new { x.LineId, x.DocumentId, x.ProductId })
                .HasConstraintName("FK_SaleAllocation_Line")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SaleBatchAllocation>()
                .HasOne(x => x.ProductBatch)
                .WithMany(x => x.SaleBatchAllocations)
                .HasForeignKey(x => new { x.ProductBatchId, x.ProductId })
                .HasPrincipalKey(x => new { x.ProductBatchId, x.ProductId })
                .HasConstraintName("FK_SaleAllocation_Batch")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SalesReturn>()
                .HasOne(x => x.Document)
                .WithOne(x => x.SalesReturn)
                .HasForeignKey<SalesReturn>(x => x.DocumentId)
                .HasConstraintName("FK_Return_Document")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SalesReturn>()
                .HasOne(x => x.OriginalSale)
                .WithMany(x => x.SalesReturns)
                .HasForeignKey(x => x.OriginalSaleId)
                .HasConstraintName("FK_Return_Sale")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SalesReturn>()
    .HasOne(x => x.Posting)
    .WithOne(x => x.SalesReturn)
    .HasForeignKey<SalesReturn>(
        x => new { x.PostingId, x.DocumentId })
    .HasPrincipalKey<Posting>(
        x => new { x.PostingId, x.DocumentId })
    .HasConstraintName("FK_SalesReturn_Posting")
    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SalesReturnLine>()
                .HasOne(x => x.DocumentLine)
                .WithOne(x => x.SalesReturnLine)
                .HasForeignKey<SalesReturnLine>(x => new { x.LineId, x.DocumentId, x.ProductId })
                .HasPrincipalKey<DocumentLine>(x => new { x.LineId, x.DocumentId, x.ProductId })
                .HasConstraintName("FK_ReturnLine_Line")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SalesReturnLine>()
                .HasOne(x => x.SalesReturn)
                .WithMany(x => x.SalesReturnLines)
                .HasForeignKey(x => new { x.DocumentId, x.OriginalSaleId })
                .HasPrincipalKey(x => new { x.DocumentId, x.OriginalSaleId })
                .HasConstraintName("FK_ReturnLine_Return")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SalesReturnLine>()
                .HasOne(x => x.OriginalAllocation)
                .WithMany(x => x.SalesReturnLines)
                .HasForeignKey(x => new { x.OriginalAllocationId, x.OriginalSaleId, x.ProductId })
                .HasPrincipalKey(x => new { x.AllocationId, x.SaleId, x.ProductId })
                .HasConstraintName("FK_ReturnLine_Allocation")
                .OnDelete(DeleteBehavior.NoAction);

            // =====================================================
            // 7. Stock Change, Balance & Movement Relationships
            // =====================================================
            modelBuilder.Entity<StockChange>()
                .HasOne(x => x.Document)
                .WithOne(x => x.StockChange)
                .HasForeignKey<StockChange>(x => x.DocumentId)
                .HasConstraintName("FK_StockChange_Document")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StockChangeLine>()
                .HasOne(x => x.DocumentLine)
                .WithOne(x => x.StockChangeLine)
                .HasForeignKey<StockChangeLine>(x => new { x.LineId, x.DocumentId, x.ProductId })
                .HasPrincipalKey<DocumentLine>(x => new { x.LineId, x.DocumentId, x.ProductId })
                .HasConstraintName("FK_StockChangeLine_Line")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StockChangeLine>()
                .HasOne(x => x.StockChange)
                .WithMany(x => x.StockChangeLines)
                .HasForeignKey(x => x.DocumentId)
                .HasConstraintName("FK_StockChangeLine_Header")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StockChangeLine>()
                .HasOne(x => x.ProductBatch)
                .WithMany(x => x.StockChangeLines)
                .HasForeignKey(x => new { x.ProductBatchId, x.ProductId })
                .HasPrincipalKey(x => new { x.ProductBatchId, x.ProductId })
                .HasConstraintName("FK_StockChangeLine_Batch")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductStockBalance>()
                .HasOne(x => x.Product)
                .WithOne(x => x.ProductStockBalance)
                .HasForeignKey<ProductStockBalance>(x => x.ProductId)
                .HasConstraintName("FK_ProductBalance_Product")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductStockBalance>()
                .HasOne(x => x.LastPosting)
                .WithMany(x => x.ProductStockBalances)
                .HasForeignKey(x => x.LastPostingId)
                .HasConstraintName("FK_ProductBalance_Posting")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StockBalance>()
                .HasOne(x => x.ProductBatch)
                .WithMany(x => x.StockBalances)
                .HasForeignKey(x => new { x.ProductBatchId, x.ProductId })
                .HasPrincipalKey(x => new { x.ProductBatchId, x.ProductId })
                .HasConstraintName("FK_StockBalance_Batch")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StockBalance>()
                .HasOne(x => x.LastPosting)
                .WithMany(x => x.StockBalances)
                .HasForeignKey(x => x.LastPostingId)
                .HasConstraintName("FK_StockBalance_Posting")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StockMovement>()
                .HasOne(x => x.Posting)
                .WithMany(x => x.StockMovements)
                .HasForeignKey(x => x.PostingId)
                .HasConstraintName("FK_Movement_Posting")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StockMovement>()
                .HasOne(x => x.SourceLine)
                .WithMany(x => x.StockMovements)
                .HasForeignKey(x => new { x.SourceLineId, x.ProductId })
                .HasPrincipalKey(x => new { x.LineId, x.ProductId })
                .HasConstraintName("FK_Movement_Source")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StockMovement>()
                .HasOne(x => x.ProductBatch)
                .WithMany(x => x.StockMovements)
                .HasForeignKey(x => new { x.ProductBatchId, x.ProductId })
                .HasPrincipalKey(x => new { x.ProductBatchId, x.ProductId })
                .HasConstraintName("FK_Movement_Batch")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StockMovement>()
                .HasOne(x => x.ReversesMovement)
                .WithOne(x => x.StockMovementAsReversesMovement)
                .HasForeignKey<StockMovement>(x => x.ReversesMovementId)
                .HasConstraintName("FK_Movement_Reversed")
                .OnDelete(DeleteBehavior.NoAction);

            // =====================================================
            // 8. Opening Balances, Subledger & Payment Relationships
            // =====================================================
            modelBuilder.Entity<OpeningAccountBalance>()
                .HasOne(x => x.Document)
                .WithOne(x => x.OpeningAccountBalance)
                .HasForeignKey<OpeningAccountBalance>(x => x.DocumentId)
                .HasConstraintName("FK_OpeningAccount_Document")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OpeningAccountBalance>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.OpeningAccountBalances)
                .HasForeignKey(x => x.CustomerId)
                .HasConstraintName("FK_OpeningAccount_Customer")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OpeningAccountBalance>()
                .HasOne(x => x.Supplier)
                .WithMany(x => x.OpeningAccountBalances)
                .HasForeignKey(x => x.SupplierId)
                .HasConstraintName("FK_OpeningAccount_Supplier")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OpeningAccountBalance>()
                .HasOne(x => x.ImportBatch)
                .WithMany(x => x.OpeningAccountBalances)
                .HasForeignKey(x => x.ImportBatchId)
                .HasConstraintName("FK_OpeningAccount_Import")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AccountEntry>()
                .HasOne(x => x.Posting)
                .WithMany(x => x.AccountEntries)
                .HasForeignKey(x => x.PostingId)
                .HasConstraintName("FK_AccountEntry_Posting")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AccountEntry>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.AccountEntries)
                .HasForeignKey(x => x.CustomerId)
                .HasConstraintName("FK_AccountEntry_Customer")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AccountEntry>()
                .HasOne(x => x.Supplier)
                .WithMany(x => x.AccountEntries)
                .HasForeignKey(x => x.SupplierId)
                .HasConstraintName("FK_AccountEntry_Supplier")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AccountAllocation>()
                .HasOne(x => x.CreditEntry)
                .WithMany(x => x.AccountAllocations)
                .HasForeignKey(x => x.CreditEntryId)
                .HasConstraintName("FK_AccountAllocation_Credit")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AccountAllocation>()
                .HasOne(x => x.DebitEntry)
                .WithMany(x => x.AccountAllocationsAsDebitEntry)
                .HasForeignKey(x => x.DebitEntryId)
                .HasConstraintName("FK_AccountAllocation_Debit")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AccountAllocation>()
                .HasOne(x => x.ReversesAllocation)
                .WithOne(x => x.AccountAllocationAsReversesAllocation)
                .HasForeignKey<AccountAllocation>(x => x.ReversesAllocationId)
                .HasConstraintName("FK_AccountAllocation_Reversed")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AccountAllocation>()
                .HasOne(x => x.IdempotencyRequest)
                .WithMany(x => x.AccountAllocations)
                .HasForeignKey(x => x.RequestId)
                .HasConstraintName("FK_AccountAllocation_Request")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AccountAllocation>()
                .HasOne(x => x.ActorUser)
                .WithMany(x => x.AccountAllocations)
                .HasForeignKey(x => x.ActorUserId)
                .HasConstraintName("FK_AccountAllocation_User")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(x => x.Document)
                .WithOne(x => x.Payment)
                .HasForeignKey<Payment>(x => x.DocumentId)
                .HasConstraintName("FK_Payment_Document")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.CustomerId)
                .HasConstraintName("FK_Payment_Customer")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(x => x.Supplier)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.SupplierId)
                .HasConstraintName("FK_Payment_Supplier")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(x => x.PurchaseOrder)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.PurchaseOrderId)
                .HasConstraintName("FK_Payment_PO")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PaymentReversal>()
                .HasOne(x => x.Document)
                .WithOne(x => x.PaymentReversal)
                .HasForeignKey<PaymentReversal>(x => x.DocumentId)
                .HasConstraintName("FK_PaymentReversal_Document")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PaymentReversal>()
                .HasOne(x => x.OriginalPayment)
                .WithOne(x => x.PaymentReversal)
                .HasForeignKey<PaymentReversal>(x => x.OriginalPaymentId)
                .HasConstraintName("FK_PaymentReversal_Original")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PaymentReversal>()
                .HasOne(x => x.Posting)
                .WithOne(x => x.PaymentReversal)
                .HasForeignKey<PaymentReversal>(x => new { x.PostingId, x.DocumentId })
                .HasPrincipalKey<Posting>(x => new { x.PostingId, x.DocumentId })
                .HasConstraintName("FK_PaymentReversal_Posting")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PaymentSettlementEvent>()
                .HasOne(x => x.Payment)
                .WithMany(x => x.PaymentSettlementEvents)
                .HasForeignKey(x => x.PaymentId)
                .HasPrincipalKey(x => x.DocumentId)
                .HasConstraintName("FK_PaymentSettlement_Payment")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PaymentSettlementEvent>()
                .HasOne(x => x.RecordedByUser)
                .WithMany(x => x.PaymentSettlementEvents)
                .HasForeignKey(x => x.RecordedByUserId)
                .HasConstraintName("FK_PaymentSettlement_User")
                .OnDelete(DeleteBehavior.NoAction);

            // =====================================================
            // 9. Jobs, Forecasting & Replenishment Relationships
            // =====================================================
            modelBuilder.Entity<JobRun>()
                .HasOne(x => x.RequestedByUser)
                .WithMany(x => x.JobRuns)
                .HasForeignKey(x => x.RequestedByUserId)
                .HasConstraintName("FK_Job_User")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<JobRun>()
                .HasOne(x => x.IdempotencyRequest)
                .WithMany(x => x.JobRuns)
                .HasForeignKey(x => x.RequestId)
                .HasConstraintName("FK_Job_Request")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ForecastRun>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ForecastRuns)
                .HasForeignKey(x => x.ProductId)
                .HasConstraintName("FK_Forecast_Product")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ForecastRun>()
                .HasOne(x => x.JobRun)
                .WithMany(x => x.ForecastRuns)
                .HasForeignKey(x => x.JobRunId)
                .HasConstraintName("FK_Forecast_Job")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ForecastObservation>()
                .HasOne(x => x.ForecastRun)
                .WithMany(x => x.ForecastObservations)
                .HasForeignKey(x => x.ForecastRunId)
                .HasConstraintName("FK_Observation_Run")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ForecastMetric>()
                .HasOne(x => x.ForecastRun)
                .WithMany(x => x.ForecastMetrics)
                .HasForeignKey(x => x.ForecastRunId)
                .HasConstraintName("FK_Metric_Run")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ForecastPoint>()
                .HasOne(x => x.ForecastRun)
                .WithMany(x => x.ForecastPoints)
                .HasForeignKey(x => x.ForecastRunId)
                .HasConstraintName("FK_ForecastPoint_Run")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReorderSuggestion>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ReorderSuggestions)
                .HasForeignKey(x => x.ProductId)
                .HasConstraintName("FK_Suggestion_Product")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReorderSuggestion>()
                .HasOne(x => x.Supplier)
                .WithMany(x => x.ReorderSuggestions)
                .HasForeignKey(x => x.SupplierId)
                .HasConstraintName("FK_Suggestion_Supplier")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReorderSuggestion>()
                .HasOne(x => x.ForecastRun)
                .WithMany(x => x.ReorderSuggestions)
                .HasForeignKey(x => new { x.ForecastRunId, x.ProductId })
                .HasPrincipalKey(x => new { x.ForecastRunId, x.ProductId })
                .HasConstraintName("FK_Suggestion_Forecast")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReorderSuggestion>()
                .HasOne(x => x.OutcomeByUser)
                .WithMany(x => x.ReorderSuggestions)
                .HasForeignKey(x => x.OutcomeByUserId)
                .HasConstraintName("FK_Suggestion_OutcomeUser")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SuggestionConversion>()
                .HasOne(x => x.ReorderSuggestion)
                .WithMany(x => x.SuggestionConversions)
                .HasForeignKey(x => new { x.SuggestionId, x.OpportunityKey })
                .HasPrincipalKey(x => new { x.SuggestionId, x.OpportunityKey })
                .HasConstraintName("FK_Conversion_Suggestion")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SuggestionConversion>()
                .HasOne(x => x.PurchaseOrderLine)
                .WithMany(x => x.SuggestionConversions)
                .HasForeignKey(x => x.PurchaseOrderLineId)
                .HasPrincipalKey(x => x.LineId)
                .HasConstraintName("FK_Conversion_POLine")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SuggestionConversion>()
                .HasOne(x => x.ReviewedByUser)
                .WithMany(x => x.SuggestionConversions)
                .HasForeignKey(x => x.ReviewedByUserId)
                .HasConstraintName("FK_Conversion_User")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SuggestionConversion>()
                .HasOne(x => x.IdempotencyRequest)
                .WithMany(x => x.SuggestionConversions)
                .HasForeignKey(x => x.RequestId)
                .HasConstraintName("FK_Conversion_Request")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SuggestionConversion>()
                .HasOne(x => x.ReleasedByUser)
                .WithMany(x => x.SuggestionConversionsAsReleasedByUser)
                .HasForeignKey(x => x.ReleasedByUserId)
                .HasConstraintName("FK_Conversion_ReleaseUser")
                .OnDelete(DeleteBehavior.NoAction);

            // =====================================================
            // 10. AI Logs, Audit, Alerts & Notifications
            // =====================================================
            modelBuilder.Entity<AiChatLog>()
                .HasOne(x => x.UserProfile)
                .WithMany(x => x.AiChatLogs)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_Chat_User")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AiToolCallLog>()
                .HasOne(x => x.AiChatLog)
                .WithMany(x => x.AiToolCallLogs)
                .HasForeignKey(x => x.AiChatLogId)
                .HasConstraintName("FK_ToolCall_Chat")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AiToolCallLog>()
                .HasOne(x => x.SourcePosting)
                .WithMany(x => x.AiToolCallLogs)
                .HasForeignKey(x => x.SourcePostingId)
                .HasConstraintName("FK_ToolCall_Posting")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AiToolCallLog>()
                .HasOne(x => x.ForecastRun)
                .WithMany(x => x.AiToolCallLogs)
                .HasForeignKey(x => x.ForecastRunId)
                .HasConstraintName("FK_ToolCall_Forecast")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AiToolCallLog>()
                .HasOne(x => x.ReorderSuggestion)
                .WithMany(x => x.AiToolCallLogs)
                .HasForeignKey(x => x.SuggestionId)
                .HasPrincipalKey(x => x.SuggestionId)
                .HasConstraintName("FK_ToolCall_Suggestion")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AuditLog>()
                .HasOne(x => x.ActorUser)
                .WithMany(x => x.AuditLogs)
                .HasForeignKey(x => x.ActorUserId)
                .HasConstraintName("FK_Audit_Actor")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AuditLog>()
                .HasOne(x => x.Document)
                .WithMany(x => x.AuditLogs)
                .HasForeignKey(x => x.DocumentId)
                .HasConstraintName("FK_Audit_Document")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AuditLog>()
                .HasOne(x => x.Posting)
                .WithMany(x => x.AuditLogs)
                .HasForeignKey(x => x.PostingId)
                .HasConstraintName("FK_Audit_Posting")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AuditLog>()
                .HasOne(x => x.Product)
                .WithMany(x => x.AuditLogs)
                .HasForeignKey(x => x.ProductId)
                .HasConstraintName("FK_Audit_Product")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AuditLog>()
                .HasOne(x => x.TargetUser)
                .WithMany(x => x.AuditLogsAsTargetUser)
                .HasForeignKey(x => x.TargetUserId)
                .HasConstraintName("FK_Audit_TargetUser")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AuditLog>()
                .HasOne(x => x.ImportBatch)
                .WithMany(x => x.AuditLogs)
                .HasForeignKey(x => x.ImportBatchId)
                .HasConstraintName("FK_Audit_Import")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AuditLog>()
                .HasOne(x => x.JobRun)
                .WithMany(x => x.AuditLogs)
                .HasForeignKey(x => x.JobRunId)
                .HasConstraintName("FK_Audit_Job")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Alert>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Alerts)
                .HasForeignKey(x => x.ProductId)
                .HasConstraintName("FK_Alert_Product")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Alert>()
                .HasOne(x => x.ProductBatch)
                .WithMany(x => x.Alerts)
                .HasForeignKey(x => new { x.ProductBatchId, x.ProductId })
                .HasPrincipalKey(x => new { x.ProductBatchId, x.ProductId })
                .HasConstraintName("FK_Alert_Batch")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Alert>()
                .HasOne(x => x.JobRun)
                .WithMany(x => x.Alerts)
                .HasForeignKey(x => x.JobRunId)
                .HasConstraintName("FK_Alert_Job")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Alert>()
                .HasOne(x => x.AcknowledgedByUser)
                .WithMany(x => x.Alerts)
                .HasForeignKey(x => x.AcknowledgedByUserId)
                .HasConstraintName("FK_Alert_User")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NotificationDelivery>()
                .HasOne(x => x.Alert)
                .WithMany(x => x.NotificationDeliveries)
                .HasForeignKey(x => x.AlertId)
                .HasConstraintName("FK_Notification_Alert")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NotificationDelivery>()
                .HasOne(x => x.JobRun)
                .WithMany(x => x.NotificationDeliveries)
                .HasForeignKey(x => x.JobRunId)
                .HasConstraintName("FK_Notification_Job")
                .OnDelete(DeleteBehavior.NoAction);

            // SQL rowversion columns are concurrency tokens, not date/time columns.
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var rowVersionProperty = entityType.FindProperty("RowVersion");
                if (rowVersionProperty != null)
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<byte[]>("RowVersion")
                        .IsRowVersion();
                }
            }
        }
    }
}
