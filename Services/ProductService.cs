using ProvaPub.Generic;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class ProductService
	{
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public PagedResult<Product>  GetList(int page)
		{
			return _ctx.Products.GetPaged(page, Constants.MAX_PAGE_RESULT);
		}
	}
}
