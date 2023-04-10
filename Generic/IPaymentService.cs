using ProvaPub.Models;

namespace ProvaPub.Generic
{
	public interface IPaymentService
    {
        Task<Order> PayOrder(decimal paymentValue, int customerId);
    }
}