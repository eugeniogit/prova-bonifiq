using ProvaPub.Models;

namespace ProvaPub.Generic
{
	public interface IPurchaseRules
    {
        bool BoughtInMonthAlready(Customer customer, DateTime currentDate);
        bool FirstPurchaseHasGreatterValue(Customer customer, decimal purchaseValue);
    }
}