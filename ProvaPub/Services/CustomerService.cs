using ProvaPub.Generic;
using ProvaPub.Models;
using ProvaPub.Extensions;
using ProvaPub.Utilities;

namespace ProvaPub.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IPurchaseRules _purchaseRules;

        public CustomerService(ICustomerRepository repository, IPurchaseRules purchaseRules)
        {
            _repository = repository;
            _purchaseRules = purchaseRules;
        }

        public PagedResult<Customer> GetList(int page)
        {
            return _repository.GetAll().GetPaged(page, Constants.MAX_PAGE_RESULT);
        }

        public async Task<bool> CanPurchaseAsync(int customerId, decimal purchaseValue, DateTime currentDate)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));
            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            var customer = await _repository.FindAsync(customerId);
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

            if (_purchaseRules.BoughtInMonthAlready(customer, currentDate))
                return false;

            if (_purchaseRules.FirstPurchaseHasGreatterValue(customer, purchaseValue))
                return false;

            return true;
        }
    }
}
