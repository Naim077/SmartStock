using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class CustomerFacade : ICustomerFacade
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerFacade(ICustomerRepository customerRepository)
        {
            ArgumentNullException.ThrowIfNull(customerRepository);
            _customerRepository = customerRepository;
        }

        public Task<List<Customer>> GetCustomersAsync(CancellationToken cancellationToken = default)
        {
            return _customerRepository.GetAllAsync(cancellationToken);
        }

        public Task<Customer?> GetCustomerByIdAsync(long customerId, CancellationToken cancellationToken = default)
        {
            return _customerRepository.GetByIdAsync(customerId, cancellationToken);
        }

        public Task<List<Customer>> GetCustomersPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _customerRepository.GetPageAsync(skip, take, cancellationToken);
        }

        public async Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(customer);
            CatalogueValidation.NewId(customer.CustomerId, nameof(customer.CustomerId));
            Validate(customer);
            if (customer.IsWalkIn && await _customerRepository.AnyAsync(other => other.IsWalkIn, cancellationToken))
                throw new InvalidOperationException("A Walk-in customer already exists.");
            await _customerRepository.AddAsync(customer, cancellationToken);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(customer);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(customer.CustomerId);
            CatalogueValidation.RowVersion(customer.RowVersion);
            var existing = await _customerRepository.GetByIdAsync(customer.CustomerId, cancellationToken);
            if (existing is null) return false;

            if (customer.IsWalkIn != existing.IsWalkIn)
                throw new InvalidOperationException("The Walk-in designation cannot be changed.");
            if (existing.IsWalkIn && (!customer.IsActive || customer.CreditLimit != existing.CreditLimit))
                throw new InvalidOperationException("The Walk-in customer cannot be deactivated or have its credit limit changed.");

            Validate(customer);
            return await _customerRepository.UpdateAsync(customer, cancellationToken);
        }

        public async Task<bool> DeleteCustomerAsync(long customerId, byte[] rowVersion, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(customerId);
            CatalogueValidation.RowVersion(rowVersion);
            var customer = await _customerRepository.GetByIdAsync(customerId, cancellationToken);
            if (customer is null) return false;
            if (customer.IsWalkIn)
                throw new InvalidOperationException("The Walk-in customer cannot be archived or deleted.");

            customer.IsActive = false;
            customer.RowVersion = rowVersion.ToArray();
            return await _customerRepository.UpdateAsync(customer, cancellationToken);
        }

        private static void Validate(Customer customer)
        {
            customer.Name = CatalogueValidation.RequiredText(customer.Name, 200, nameof(customer.Name));
            customer.Email = CatalogueValidation.OptionalEmail(customer.Email, nameof(customer.Email));
            customer.Phone = CatalogueValidation.OptionalText(customer.Phone, 40, nameof(customer.Phone));
            customer.Address = CatalogueValidation.OptionalText(customer.Address, 500, nameof(customer.Address));
            CatalogueValidation.NonNegativeDecimal(customer.CreditLimit, 2, nameof(customer.CreditLimit));
            if (customer.IsWalkIn && (!customer.IsActive || customer.CreditLimit != 0m))
                throw new ArgumentException("The Walk-in customer must be active with a zero credit limit.", nameof(customer));
        }
    }
}
