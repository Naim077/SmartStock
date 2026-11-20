using System.Security.Claims;
using AIMS.Entities;
using AIMS.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIMS.Web.Ui.Controllers
{
    [Authorize]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseOrderFacade _purchaseOrderFacade;
        private readonly IDocumentFacade _documentFacade;
        private readonly IPurchaseOrderLineFacade _purchaseOrderLineFacade;
        private readonly IDocumentLineFacade _documentLineFacade;
        private readonly ISupplierFacade _supplierFacade;
        private readonly IProductFacade _productFacade;

        public PurchaseController(
            IPurchaseOrderFacade purchaseOrderFacade,
            IDocumentFacade documentFacade,
            IPurchaseOrderLineFacade purchaseOrderLineFacade,
            IDocumentLineFacade documentLineFacade,
            ISupplierFacade supplierFacade,
            IProductFacade productFacade)
        {
            _purchaseOrderFacade = purchaseOrderFacade;
            _documentFacade = documentFacade;
            _purchaseOrderLineFacade = purchaseOrderLineFacade;
            _documentLineFacade = documentLineFacade;
            _supplierFacade = supplierFacade;
            _productFacade = productFacade;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search, string? status)
        {
            var purchaseOrders = await _purchaseOrderFacade.GetPurchaseOrdersAsync();
            var documents = await _documentFacade.GetDocumentsAsync();
            var lines = await _purchaseOrderLineFacade.GetPurchaseOrderLinesAsync();

            var purchaseDocumentIds = purchaseOrders.Select(x => x.DocumentId).ToHashSet();
            var purchaseDocuments = documents
                .Where(x => purchaseDocumentIds.Contains(x.DocumentId) &&
                            string.Equals(x.DocumentKind, "PurchaseOrder", StringComparison.OrdinalIgnoreCase))
                .ToDictionary(x => x.DocumentId);

            var purchaseLines = lines
                .Where(x => purchaseDocumentIds.Contains(x.DocumentId))
                .GroupBy(x => x.DocumentId)
                .ToDictionary(x => x.Key, x => x.ToList());

            var rows = purchaseOrders
                .Where(po => purchaseDocuments.ContainsKey(po.DocumentId))
                .OrderByDescending(po => purchaseDocuments[po.DocumentId].CreatedUtc)
                .ToList();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                rows = rows.Where(po =>
                {
                    var document = purchaseDocuments[po.DocumentId];
                    return (document.DocumentNo ?? string.Empty).Contains(term, StringComparison.OrdinalIgnoreCase)
                        || (po.SupplierNameSnapshot ?? string.Empty).Contains(term, StringComparison.OrdinalIgnoreCase);
                }).ToList();
            }

            if (!string.IsNullOrWhiteSpace(status) &&
                !status.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                rows = rows.Where(po =>
                    string.Equals(purchaseDocuments[po.DocumentId].Status, status, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.Documents = purchaseDocuments;
            ViewBag.PurchaseLines = purchaseLines;
            ViewBag.Search = search ?? string.Empty;
            ViewBag.SelectedStatus = status ?? "All";
            ViewBag.TotalOrders = purchaseOrders.Count;
            ViewBag.DraftOrders = purchaseDocuments.Values.Count(x => x.Status == "Draft");
            ViewBag.ApprovedOrders = purchaseDocuments.Values.Count(x => x.Status == "Approved");
            ViewBag.PartiallyReceivedOrders = purchaseDocuments.Values.Count(x =>
                x.Status.Equals("PartiallyReceived", StringComparison.OrdinalIgnoreCase) ||
                x.Status.Equals("Partially Received", StringComparison.OrdinalIgnoreCase));
            ViewBag.ReceivedOrders = purchaseDocuments.Values.Count(x => x.Status == "Received");

            return View(rows);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadPurchaseFormData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            long supplierId,
            DateTime? expectedDate,
            long[] productIds,
            decimal[] quantities,
            decimal[] unitCosts,
            string? notes)
        {
            await LoadPurchaseFormData();

            if (supplierId <= 0)
            {
                ModelState.AddModelError(nameof(supplierId), "Please select a supplier.");
            }

            if (productIds == null || quantities == null || unitCosts == null ||
                productIds.Length == 0 || productIds.Length != quantities.Length ||
                productIds.Length != unitCosts.Length)
            {
                ModelState.AddModelError(string.Empty, "Add at least one valid product line.");
            }
            else
            {
                for (var i = 0; i < productIds.Length; i++)
                {
                    if (productIds[i] <= 0 || quantities[i] <= 0 || unitCosts[i] < 0)
                    {
                        ModelState.AddModelError(string.Empty, $"Please check purchase item row {i + 1}.");
                    }
                }
            }

            var supplier = await _supplierFacade.GetSupplierByIdAsync(supplierId);
            if (supplier == null || !supplier.IsActive)
            {
                ModelState.AddModelError(nameof(supplierId), "The selected supplier is not active or does not exist.");
            }

            var products = await _productFacade.GetProductsAsync();
            var selectedProducts = products
                .Where(p => productIds != null && productIds.Contains(p.ProductId) && p.IsActive)
                .ToDictionary(p => p.ProductId);

            if (productIds != null && productIds.Any(id => !selectedProducts.ContainsKey(id)))
            {
                ModelState.AddModelError(string.Empty, "One or more selected products are unavailable.");
            }

            if (!ModelState.IsValid || supplier == null)
            {
                return View();
            }

            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdValue, out var userId))
            {
                ModelState.AddModelError(string.Empty, "Your user identity could not be verified. Please sign in again.");
                return View();
            }

            // Create the shared document header first so SQL Server generates DocumentId.
            var document = new Document
            {
                DocumentKind = "PurchaseOrder",
                DocumentNo = null, // Keep null until your document-numbering workflow assigns a PO number.
                Status = "Draft",
                CurrentVersionNo = 1,
                CurrencyCode = "BDT",
                CreatedByUserId = userId,
                CreatedUtc = DateTime.UtcNow
            };

            await _documentFacade.AddDocumentAsync(document);

            var purchaseOrder = new PurchaseOrder
            {
                DocumentId = document.DocumentId,
                SupplierId = supplier.SupplierId,
                ExpectedDate = expectedDate?.Date,
                SupplierNameSnapshot = supplier.Name,
                SupplierAddressSnapshot = supplier.Address,
                PaymentTermsDaysSnapshot = supplier.PaymentTermsDays
            };

            await _purchaseOrderFacade.AddPurchaseOrderAsync(purchaseOrder);

            for (var i = 0; i < productIds.Length; i++)
            {
                var product = selectedProducts[productIds[i]];

                // DocumentLine owns the identity LineId. PurchaseOrderLine reuses it
                // because the SQL schema links these two rows by (LineId, DocumentId, ProductId).
                var documentLine = new DocumentLine
                {
                    DocumentId = document.DocumentId,
                    LineNumber = checked((short)(i + 1)),
                    ProductId = product.ProductId
                };

                await _documentLineFacade.AddDocumentLineAsync(documentLine);

                var purchaseOrderLine = new PurchaseOrderLine
                {
                    LineId = documentLine.LineId,
                    DocumentId = document.DocumentId,
                    ProductId = product.ProductId,
                    ProductNameSnapshot = product.Name,
                    UnitSnapshot = product.BaseUnit,
                    OrderedQuantity = quantities[i],
                    UnitCost = unitCosts[i],
                    NetReceivedQuantity = 0,
                    ClosedQuantity = 0
                };

                await _purchaseOrderLineFacade.AddPurchaseOrderLineAsync(purchaseOrderLine);
            }

            TempData["SuccessMessage"] = "Purchase order saved as Draft.";
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadPurchaseFormData()
        {
            ViewBag.Suppliers = (await _supplierFacade.GetSuppliersAsync())
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .ToList();

            ViewBag.Products = (await _productFacade.GetProductsAsync())
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}
