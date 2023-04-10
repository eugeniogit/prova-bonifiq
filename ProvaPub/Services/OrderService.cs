using ProvaPub.Models;

namespace ProvaPub.Services
{
	public class OrderService
	{
		public Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
			return PaymentFactory.GetService(paymentMethod).PayOrder(paymentValue, customerId);
		}
	}
}
