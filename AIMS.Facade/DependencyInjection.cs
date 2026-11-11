using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AIMS.DataAccess;
using AIMS.DataAccess.Implementation;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Implementation;
using AIMS.Facade.Interface;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Infrastructure;
using AIMS.Entities;


namespace AIMS.Facade
{
    public static class DependencyInjection
    {
        /// <summary>Registers the new SQL Server data-access context, repositories, facades and GUID Identity stores.</summary>
        /// <remarks>No connection, migrations, seeds, authentication endpoints or authorization workflows are started.</remarks>
        public static IServiceCollection AddAIMSFacade(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("ConnectionStrings:DefaultConnection must be configured before calling AddAIMSFacade.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, sql => sql.UseCompatibilityLevel(150)));
            services.TryAddSingleton<TimeProvider>(TimeProvider.System);
            services.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<Interface.IUserProfileFacade, UserProfileFacade>();

            services.AddScoped<IBusinessSettingsRepository, BusinessSettingsRepository>();
            services.AddScoped<IBusinessSettingsFacade, BusinessSettingsFacade>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryFacade, CategoryFacade>();

            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ISupplierFacade, SupplierFacade>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerFacade, CustomerFacade>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductFacade, ProductFacade>();

            services.AddScoped<IProductBatchRepository, ProductBatchRepository>();
            services.AddScoped<IProductBatchFacade, ProductBatchFacade>();

            services.AddScoped<IIdempotencyRequestRepository, IdempotencyRequestRepository>();
            services.AddScoped<IIdempotencyRequestFacade, IdempotencyRequestFacade>();

            services.AddScoped<IImportBatchRepository, ImportBatchRepository>();
            services.AddScoped<IImportBatchFacade, ImportBatchFacade>();

            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IDocumentFacade, DocumentFacade>();

            services.AddScoped<IDocumentVersionRepository, DocumentVersionRepository>();
            services.AddScoped<IDocumentVersionFacade, DocumentVersionFacade>();

            services.AddScoped<IApprovalRepository, ApprovalRepository>();
            services.AddScoped<IApprovalFacade, ApprovalFacade>();

            services.AddScoped<IPostingRepository, PostingRepository>();
            services.AddScoped<IPostingFacade, PostingFacade>();

            services.AddScoped<IDocumentLineRepository, DocumentLineRepository>();
            services.AddScoped<IDocumentLineFacade, DocumentLineFacade>();

            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddScoped<IPurchaseOrderFacade, PurchaseOrderFacade>();

            services.AddScoped<IPurchaseOrderLineRepository, PurchaseOrderLineRepository>();
            services.AddScoped<IPurchaseOrderLineFacade, PurchaseOrderLineFacade>();

            services.AddScoped<IGoodsReceiptRepository, GoodsReceiptRepository>();
            services.AddScoped<IGoodsReceiptFacade, GoodsReceiptFacade>();

            services.AddScoped<IGoodsReceiptLineRepository, GoodsReceiptLineRepository>();
            services.AddScoped<IGoodsReceiptLineFacade, GoodsReceiptLineFacade>();

            services.AddScoped<IGoodsReceiptReversalRepository, GoodsReceiptReversalRepository>();
            services.AddScoped<IGoodsReceiptReversalFacade, GoodsReceiptReversalFacade>();

            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ISaleFacade, SaleFacade>();

            services.AddScoped<ISaleLineRepository, SaleLineRepository>();
            services.AddScoped<ISaleLineFacade, SaleLineFacade>();

            services.AddScoped<ISaleBatchAllocationRepository, SaleBatchAllocationRepository>();
            services.AddScoped<ISaleBatchAllocationFacade, SaleBatchAllocationFacade>();

            services.AddScoped<ISalesReturnRepository, SalesReturnRepository>();
            services.AddScoped<ISalesReturnFacade, SalesReturnFacade>();

            services.AddScoped<ISalesReturnLineRepository, SalesReturnLineRepository>();
            services.AddScoped<ISalesReturnLineFacade, SalesReturnLineFacade>();

            services.AddScoped<IStockChangeRepository, StockChangeRepository>();
            services.AddScoped<IStockChangeFacade, StockChangeFacade>();

            services.AddScoped<IStockChangeLineRepository, StockChangeLineRepository>();
            services.AddScoped<IStockChangeLineFacade, StockChangeLineFacade>();

            services.AddScoped<IProductStockBalanceRepository, ProductStockBalanceRepository>();
            services.AddScoped<IProductStockBalanceFacade, ProductStockBalanceFacade>();

            services.AddScoped<IStockBalanceRepository, StockBalanceRepository>();
            services.AddScoped<IStockBalanceFacade, StockBalanceFacade>();

            services.AddScoped<IStockMovementRepository, StockMovementRepository>();
            services.AddScoped<IStockMovementFacade, StockMovementFacade>();

            services.AddScoped<IOpeningAccountBalanceRepository, OpeningAccountBalanceRepository>();
            services.AddScoped<IOpeningAccountBalanceFacade, OpeningAccountBalanceFacade>();

            services.AddScoped<IAccountEntryRepository, AccountEntryRepository>();
            services.AddScoped<IAccountEntryFacade, AccountEntryFacade>();

            services.AddScoped<IAccountAllocationRepository, AccountAllocationRepository>();
            services.AddScoped<IAccountAllocationFacade, AccountAllocationFacade>();

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentFacade, PaymentFacade>();

            services.AddScoped<IPaymentReversalRepository, PaymentReversalRepository>();
            services.AddScoped<IPaymentReversalFacade, PaymentReversalFacade>();

            services.AddScoped<IPaymentSettlementEventRepository, PaymentSettlementEventRepository>();
            services.AddScoped<IPaymentSettlementEventFacade, PaymentSettlementEventFacade>();

            services.AddScoped<IJobRunRepository, JobRunRepository>();
            services.AddScoped<IJobRunFacade, JobRunFacade>();

            services.AddScoped<IForecastRunRepository, ForecastRunRepository>();
            services.AddScoped<IForecastRunFacade, ForecastRunFacade>();

            services.AddScoped<IForecastObservationRepository, ForecastObservationRepository>();
            services.AddScoped<IForecastObservationFacade, ForecastObservationFacade>();

            services.AddScoped<IForecastMetricRepository, ForecastMetricRepository>();
            services.AddScoped<IForecastMetricFacade, ForecastMetricFacade>();

            services.AddScoped<IForecastPointRepository, ForecastPointRepository>();
            services.AddScoped<IForecastPointFacade, ForecastPointFacade>();

            services.AddScoped<IReorderSuggestionRepository, ReorderSuggestionRepository>();
            services.AddScoped<IReorderSuggestionFacade, ReorderSuggestionFacade>();

            services.AddScoped<ISuggestionConversionRepository, SuggestionConversionRepository>();
            services.AddScoped<ISuggestionConversionFacade, SuggestionConversionFacade>();

            services.AddScoped<IAiChatLogRepository, AiChatLogRepository>();
            services.AddScoped<IAiChatLogFacade, AiChatLogFacade>();

            services.AddScoped<IAiToolCallLogRepository, AiToolCallLogRepository>();
            services.AddScoped<IAiToolCallLogFacade, AiToolCallLogFacade>();

            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<IAuditLogFacade, AuditLogFacade>();

            services.AddScoped<IAlertRepository, AlertRepository>();
            services.AddScoped<IAlertFacade, AlertFacade>();

            services.AddScoped<INotificationDeliveryRepository, NotificationDeliveryRepository>();
            services.AddScoped<INotificationDeliveryFacade, NotificationDeliveryFacade>();

            services.AddDataProtection();
            services.AddIdentityCore<ApplicationUser>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequiredLength = 12;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
