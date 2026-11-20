using AIMS.DataAccess;
using AIMS.Facade.Implementation;
using AIMS.Facade.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AIMS.Facade
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAIMSFacade(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAIMSDataAccess(configuration);

            services.AddScoped<IAccountAllocationFacade, AccountAllocationFacade>();
            services.AddScoped<IAccountEntryFacade, AccountEntryFacade>();
            services.AddScoped<IAiChatLogFacade, AiChatLogFacade>();
            services.AddScoped<IAiToolCallLogFacade, AiToolCallLogFacade>();
            services.AddScoped<IAlertFacade, AlertFacade>();
            services.AddScoped<IApprovalFacade, ApprovalFacade>();
            services.AddScoped<IAuditLogFacade, AuditLogFacade>();
            services.AddScoped<IBusinessSettingsFacade, BusinessSettingsFacade>();
            services.AddScoped<ICategoryFacade, CategoryFacade>();
            services.AddScoped<ICustomerFacade, CustomerFacade>();
            services.AddScoped<IDocumentFacade, DocumentFacade>();
            services.AddScoped<IDocumentLineFacade, DocumentLineFacade>();
            services.AddScoped<IDocumentVersionFacade, DocumentVersionFacade>();
            services.AddScoped<IForecastMetricFacade, ForecastMetricFacade>();
            services.AddScoped<IForecastObservationFacade, ForecastObservationFacade>();
            services.AddScoped<IForecastPointFacade, ForecastPointFacade>();
            services.AddScoped<IForecastRunFacade, ForecastRunFacade>();
            services.AddScoped<IGoodsReceiptFacade, GoodsReceiptFacade>();
            services.AddScoped<IGoodsReceiptLineFacade, GoodsReceiptLineFacade>();
            services.AddScoped<IGoodsReceiptReversalFacade, GoodsReceiptReversalFacade>();
            services.AddScoped<IIdempotencyRequestFacade, IdempotencyRequestFacade>();
            services.AddScoped<IImportBatchFacade, ImportBatchFacade>();
            services.AddScoped<IJobRunFacade, JobRunFacade>();
            services.AddScoped<INotificationDeliveryFacade, NotificationDeliveryFacade>();
            services.AddScoped<IOpeningAccountBalanceFacade, OpeningAccountBalanceFacade>();
            services.AddScoped<IPaymentFacade, PaymentFacade>();
            services.AddScoped<IPaymentReversalFacade, PaymentReversalFacade>();
            services.AddScoped<IPaymentSettlementEventFacade, PaymentSettlementEventFacade>();
            services.AddScoped<IPermissionFacade, PermissionFacade>();
            services.AddScoped<IPermissionGroupFacade, PermissionGroupFacade>();
            services.AddScoped<IPermissionGroupMapFacade, PermissionGroupMapFacade>();
            services.AddScoped<IPostingFacade, PostingFacade>();
            services.AddScoped<IProductFacade, ProductFacade>();
            services.AddScoped<IProductBatchFacade, ProductBatchFacade>();
            services.AddScoped<IProductStockBalanceFacade, ProductStockBalanceFacade>();
            services.AddScoped<IPurchaseOrderFacade, PurchaseOrderFacade>();
            services.AddScoped<IPurchaseOrderLineFacade, PurchaseOrderLineFacade>();
            services.AddScoped<IReorderSuggestionFacade, ReorderSuggestionFacade>();
            services.AddScoped<ISaleFacade, SaleFacade>();
            services.AddScoped<ISaleBatchAllocationFacade, SaleBatchAllocationFacade>();
            services.AddScoped<ISaleLineFacade, SaleLineFacade>();
            services.AddScoped<ISalesReturnFacade, SalesReturnFacade>();
            services.AddScoped<ISalesReturnLineFacade, SalesReturnLineFacade>();
            services.AddScoped<IStockBalanceFacade, StockBalanceFacade>();
            services.AddScoped<IStockChangeFacade, StockChangeFacade>();
            services.AddScoped<IStockChangeLineFacade, StockChangeLineFacade>();
            services.AddScoped<IStockMovementFacade, StockMovementFacade>();
            services.AddScoped<ISuggestionConversionFacade, SuggestionConversionFacade>();
            services.AddScoped<ISupplierFacade, SupplierFacade>();
            services.AddScoped<IUserPermissionFacade, UserPermissionFacade>();
            services.AddScoped<IUserProfileFacade, UserProfileFacade>();
            return services;
        }
    }
}
