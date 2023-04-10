using ProvaPub.Models;

namespace ProvaPub.Generic
{
	public interface ICustomerRepository
    {
        Task<Customer> FindAsync(int customerId);

        IQueryable<Customer> GetAll();
    }
}