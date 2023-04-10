using ProvaPub.Models;
using ProvaPub.Generic;

namespace ProvaPub.Utilities
{
	public class PurchaseRules : IPurchaseRules
    {
		const int MAX_VALUE_IN_FIRST_PURCHASE = 100;

		public bool BoughtInMonthAlready(Customer customer, DateTime currentDate)
		{
			var baseDate = currentDate.AddMonths(-1);
			return customer.Orders.Count(w => w.OrderDate >= baseDate) > 1;
		}

		public bool FirstPurchaseHasGreatterValue(Customer customer, decimal purchaseValue)
		{
			return customer.Orders.Any() && purchaseValue > MAX_VALUE_IN_FIRST_PURCHASE;
		}
	}
}
