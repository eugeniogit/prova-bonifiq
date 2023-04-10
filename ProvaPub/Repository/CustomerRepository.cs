using ProvaPub.Generic;
using ProvaPub.Models;
using System.Linq;


namespace ProvaPub.Repository
{
	public class CustomerRepository : ICustomerRepository
    {
        private readonly TestDbContext _ctx;
        public CustomerRepository(TestDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Customer> FindAsync(int customerId)
        {
            return await _ctx.Customers.FindAsync(customerId);
        }

        public IQueryable<Customer> GetAll()
        {
            return _ctx.Customers;
        }
    }
}
