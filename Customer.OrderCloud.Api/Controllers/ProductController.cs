using Customer.OrderCloud.Common.Commands;
using Customer.OrderCloud.Common.Models;
using Microsoft.AspNetCore.Mvc;
using OrderCloud.Catalyst;
using OrderCloud.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.OrderCloud.Api.Controllers
{
	[Route("api/products")]
	public class ProductController : CatalystController
	{
		private readonly IOrderCloudClient _oc;
		private readonly IProductCommand _productCommand;

		public ProductController(IOrderCloudClient oc, IProductCommand productCommand)
		{
			_oc = oc;
			_productCommand = productCommand;
		}

		[HttpGet]
		// [OrderCloudUserAuth(ApiRole.Shopper), UserTypeRestrictedTo(CommerceRole.Buyer)]
		public async Task<ListPageWithFacets<MyProduct>> GetAllProduct(string keyword)
		{
			var xFacets = await _oc.Products.ListAsync<MyProduct>(opt => opt.SearchFor(keyword));

			return xFacets;
		}
		
		[HttpGet, Route("{id}")]
		public async Task<MyProduct> GetProduct(string id)
		{
			var item = await _oc.Products.GetAsync<MyProduct>(id);
			return item;
		}
		
		[HttpPost]
		// [OrderCloudUserAuth(ApiRole.Shopper), UserTypeRestrictedTo(CommerceRole.Buyer)]
		public async Task<IActionResult> CreateProduct(string productName)
		{
			var rand = new Random(Guid.NewGuid().GetHashCode());
			int r = rand.Next(0, 100000);
			var product = new MyProduct
			{
				Name = $"{productName} {r}",
				Active = true,
				xp = new MyProductXp
				{
					Status = null,
					Brand = "Apple",
					UnitOfMeasure = null,
					CCID = null,
					Images = null,
					Currency = null,
					ProductType = null,
					ProductUrl = null
				}
			};
			
			await _productCommand.Create(product);
			return Created(string.Empty, null);
		}
		
		[HttpPatch, Route("{id}/inactive")]
		// [OrderCloudUserAuth(ApiRole.Shopper), UserTypeRestrictedTo(CommerceRole.Buyer)]
		public async Task<IActionResult> InactiveProduct(string id)
		{
			var product = await _oc.Products.GetAsync<MyProduct>(id);
			await _productCommand.Inactive(product.ID);
			return Ok();
		}
		
		[HttpDelete, Route("{id}")]
		// [OrderCloudUserAuth(ApiRole.Shopper), UserTypeRestrictedTo(CommerceRole.Buyer)]
		public async Task<IActionResult> DeleteProduct(string id)
		{
			var product = await _oc.Products.GetAsync<MyProduct>(id);
			await _productCommand.Delete(product.ID);
			return Ok();
		}
	}
}
