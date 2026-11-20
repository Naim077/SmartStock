using AIMS.Entities;
using AIMS.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIMS.Web.Ui.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerFacade _customerFacade;

        public CustomerController(ICustomerFacade customerFacade)
        {
            _customerFacade = customerFacade;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search, string? status, string? type)
        {
            var allCustomers = await _customerFacade.GetCustomersAsync();
            var customers = allCustomers.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                customers = customers.Where(c =>
                    (c.Name ?? string.Empty).Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    (c.Email ?? string.Empty).Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    (c.Phone ?? string.Empty).Contains(term, StringComparison.OrdinalIgnoreCase));
            }

            if (string.Equals(status, "Active", StringComparison.OrdinalIgnoreCase))
                customers = customers.Where(c => c.IsActive);
            else if (string.Equals(status, "Inactive", StringComparison.OrdinalIgnoreCase))
                customers = customers.Where(c => !c.IsActive);

            if (string.Equals(type, "WalkIn", StringComparison.OrdinalIgnoreCase))
                customers = customers.Where(c => c.IsWalkIn);
            else if (string.Equals(type, "Registered", StringComparison.OrdinalIgnoreCase))
                customers = customers.Where(c => !c.IsWalkIn);

            ViewBag.Search = search ?? string.Empty;
            ViewBag.SelectedStatus = status ?? "All";
            ViewBag.SelectedType = type ?? "All";
            ViewBag.TotalCustomers = allCustomers.Count;
            ViewBag.ActiveCustomers = allCustomers.Count(c => c.IsActive);
            ViewBag.InactiveCustomers = allCustomers.Count(c => !c.IsActive);
            ViewBag.WalkInCustomers = allCustomers.Count(c => c.IsWalkIn);

            return View(customers.OrderBy(c => c.Name).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Customer { IsActive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            ModelState.Remove(nameof(Customer.CustomerId));
            ModelState.Remove(nameof(Customer.RowVersion));

            if (!ModelState.IsValid)
                return View(customer);

            customer.Name = customer.Name.Trim();
            customer.Email = string.IsNullOrWhiteSpace(customer.Email) ? null : customer.Email.Trim();
            customer.Phone = string.IsNullOrWhiteSpace(customer.Phone) ? null : customer.Phone.Trim();
            customer.Address = string.IsNullOrWhiteSpace(customer.Address) ? null : customer.Address.Trim();

            await _customerFacade.AddCustomerAsync(customer);
            TempData["SuccessMessage"] = "Customer created successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var customer = await _customerFacade.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Customer customer)
        {
            if (id != customer.CustomerId)
                return BadRequest();

            ModelState.Remove(nameof(Customer.RowVersion));
            if (!ModelState.IsValid)
                return View(customer);

            // Fetch the existing tracked entity and update only editable fields.
            var existing = await _customerFacade.GetCustomerByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.Name = customer.Name.Trim();
            existing.Email = string.IsNullOrWhiteSpace(customer.Email) ? null : customer.Email.Trim();
            existing.Phone = string.IsNullOrWhiteSpace(customer.Phone) ? null : customer.Phone.Trim();
            existing.Address = string.IsNullOrWhiteSpace(customer.Address) ? null : customer.Address.Trim();
            existing.CreditLimit = customer.CreditLimit;
            existing.IsWalkIn = customer.IsWalkIn;
            existing.IsActive = customer.IsActive;

            await _customerFacade.UpdateCustomerAsync(existing);
            TempData["SuccessMessage"] = "Customer updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(long id)
        {
            var customer = await _customerFacade.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            customer.IsActive = !customer.IsActive;
            await _customerFacade.UpdateCustomerAsync(customer);

            TempData["SuccessMessage"] = customer.IsActive
                ? "Customer activated successfully."
                : "Customer deactivated successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
