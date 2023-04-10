using ProvaPub.Generic;
using ProvaPub.Models;

namespace ProvaPub.Services
{
    public class PayPalPaymentService : IPaymentService
    {
        public Task<Order> PayOrder(decimal paymentValue, int customerId)
        {
            return Task.FromResult( new Order()
			{
				Value = paymentValue
			});
        }
    }
}
