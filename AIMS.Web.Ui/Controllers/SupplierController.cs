using AIMS.Entities;
using AIMS.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIMS.Web.Ui.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private readonly ISupplierFacade _supplierFacade;

        public SupplierController(ISupplierFacade supplierFacade)
        {
            _supplierFacade = supplierFacade;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search, string? status)
        {
            var suppliers = await _supplierFacade.GetSuppliersAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                suppliers = suppliers.Where(s =>
                    (s.Name ?? string.Empty).Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    (s.ContactName ?? string.Empty).Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    (s.Email ?? string.Empty).Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    (s.Phone ?? string.Empty).Contains(term, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (string.Equals(status, "Active", StringComparison.OrdinalIgnoreCase))
                suppliers = suppliers.Where(s => s.IsActive).ToList();
            else if (string.Equals(status, "Inactive", StringComparison.OrdinalIgnoreCase))
                suppliers = suppliers.Where(s => !s.IsActive).ToList();

            ViewBag.Search = search ?? string.Empty;
            ViewBag.SelectedStatus = status ?? "All";
            ViewBag.TotalSuppliers = (await _supplierFacade.GetSuppliersAsync()).Count;
            ViewBag.ActiveSuppliers = (await _supplierFacade.GetSuppliersAsync()).Count(s => s.IsActive);
            ViewBag.InactiveSuppliers = (await _supplierFacade.GetSuppliersAsync()).Count(s => !s.IsActive);

            return View(suppliers.OrderBy(s => s.Name).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Supplier
            {
                IsActive = true,
                PaymentTermsDays = 0
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            ModelState.Remove(nameof(Supplier.SupplierId));
            ModelState.Remove(nameof(Supplier.RowVersion));

            if (!ModelState.IsValid)
                return View(supplier);

            supplier.Name = supplier.Name.Trim();
            supplier.Email = string.IsNullOrWhiteSpace(supplier.Email) ? null : supplier.Email.Trim();
            supplier.Phone = string.IsNullOrWhiteSpace(supplier.Phone) ? null : supplier.Phone.Trim();
            supplier.ContactName = string.IsNullOrWhiteSpace(supplier.ContactName) ? null : supplier.ContactName.Trim();
            supplier.Address = string.IsNullOrWhiteSpace(supplier.Address) ? null : supplier.Address.Trim();

            await _supplierFacade.AddSupplierAsync(supplier);
            TempData["SuccessMessage"] = "Supplier created successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var supplier = await _supplierFacade.GetSupplierByIdAsync(id);
            if (supplier == null)
                return NotFound();

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Supplier supplier)
        {
            if (id != supplier.SupplierId)
                return BadRequest();

            ModelState.Remove(nameof(Supplier.RowVersion));
            if (!ModelState.IsValid)
                return View(supplier);

            // Load the tracked entity through the facade, then copy only editable fields.
            var existing = await _supplierFacade.GetSupplierByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.Name = supplier.Name.Trim();
            existing.ContactName = supplier.ContactName?.Trim();
            existing.Email = supplier.Email?.Trim();
            existing.Phone = supplier.Phone?.Trim();
            existing.Address = supplier.Address?.Trim();
            existing.PaymentTermsDays = supplier.PaymentTermsDays;
            existing.ConfiguredLeadTimeDays = supplier.ConfiguredLeadTimeDays;
            existing.IsActive = supplier.IsActive;

            await _supplierFacade.UpdateSupplierAsync(existing);
            TempData["SuccessMessage"] = "Supplier updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(long id)
        {
            var supplier = await _supplierFacade.GetSupplierByIdAsync(id);
            if (supplier == null)
                return NotFound();

            supplier.IsActive = !supplier.IsActive;
            await _supplierFacade.UpdateSupplierAsync(supplier);

            TempData["SuccessMessage"] = supplier.IsActive
                ? "Supplier activated successfully."
                : "Supplier deactivated successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
