using AIMS.DataAccess.Implementation;
using AIMS.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AIMS.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAIMSDataAccess(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("ConnectionStrings:DefaultConnection is missing.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IAccountAllocationRepository, AccountAllocationRepository>();
            services.AddScoped<IAccountEntryRepository, AccountEntryRepository>();
            services.AddScoped<IAiChatLogRepository, AiChatLogRepository>();
            services.AddScoped<IAiToolCallLogRepository, AiToolCallLogRepository>();
            services.AddScoped<IAlertRepository, AlertRepository>();
            services.AddScoped<IApprovalRepository, ApprovalRepository>();
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<IBusinessSettingsRepository, BusinessSettingsRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IDocumentLineRepository, DocumentLineRepository>();
            services.AddScoped<IDocumentVersionRepository, DocumentVersionRepository>();
            services.AddScoped<IForecastMetricRepository, ForecastMetricRepository>();
            services.AddScoped<IForecastObservationRepository, ForecastObservationRepository>();
            services.AddScoped<IForecastPointRepository, ForecastPointRepository>();
            services.AddScoped<IForecastRunRepository, ForecastRunRepository>();
            services.AddScoped<IGoodsReceiptRepository, GoodsReceiptRepository>();
            services.AddScoped<IGoodsReceiptLineRepository, GoodsReceiptLineRepository>();
            services.AddScoped<IGoodsReceiptReversalRepository, GoodsReceiptReversalRepository>();
            services.AddScoped<IIdempotencyRequestRepository, IdempotencyRequestRepository>();
            services.AddScoped<IImportBatchRepository, ImportBatchRepository>();
            services.AddScoped<IJobRunRepository, JobRunRepository>();
            services.AddScoped<INotificationDeliveryRepository, NotificationDeliveryRepository>();
            services.AddScoped<IOpeningAccountBalanceRepository, OpeningAccountBalanceRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentReversalRepository, PaymentReversalRepository>();
            services.AddScoped<IPaymentSettlementEventRepository, PaymentSettlementEventRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionGroupRepository, PermissionGroupRepository>();
            services.AddScoped<IPermissionGroupMapRepository, PermissionGroupMapRepository>();
            services.AddScoped<IPostingRepository, PostingRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductBatchRepository, ProductBatchRepository>();
            services.AddScoped<IProductStockBalanceRepository, ProductStockBalanceRepository>();
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddScoped<IPurchaseOrderLineRepository, PurchaseOrderLineRepository>();
            services.AddScoped<IReorderSuggestionRepository, ReorderSuggestionRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ISaleBatchAllocationRepository, SaleBatchAllocationRepository>();
            services.AddScoped<ISaleLineRepository, SaleLineRepository>();
            services.AddScoped<ISalesReturnRepository, SalesReturnRepository>();
            services.AddScoped<ISalesReturnLineRepository, SalesReturnLineRepository>();
            services.AddScoped<IStockBalanceRepository, StockBalanceRepository>();
            services.AddScoped<IStockChangeRepository, StockChangeRepository>();
            services.AddScoped<IStockChangeLineRepository, StockChangeLineRepository>();
            services.AddScoped<IStockMovementRepository, StockMovementRepository>();
            services.AddScoped<ISuggestionConversionRepository, SuggestionConversionRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            return services;
        }
    }
}
