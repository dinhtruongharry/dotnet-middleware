using OrderCloud.SDK;
using System.Threading.Tasks;
using Customer.OrderCloud.Common.Models;

namespace Customer.OrderCloud.Common.Commands
{
	public interface IProductCommand
	{
		Task Create(Product product);
		Task Inactive(string productId);
		Task Delete(string productId);
	}

	public class ProductCommand : IProductCommand
	{
		private readonly IOrderCloudClient _oc;
		public ProductCommand(IOrderCloudClient oc)
		{
			_oc = oc;
		}

		public async Task Create(Product product)
		{
			await _oc.Products.CreateAsync(product);
		}
		
		public async Task Inactive(string productId)
		{
			await _oc.Products.PatchAsync<MyProduct>(productId, new PartialProduct()
			{
				Active = false
			});
		}

		public async Task Delete(string productId)
		{
			await _oc.Products.DeleteAsync(productId);
		}
	}
}
