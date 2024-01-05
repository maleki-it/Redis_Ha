using Microsoft.AspNetCore.Mvc;
using RediSearch_webapi.Models;
using RediSearch_webapi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RediSearch_webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly ProductService _productService;

		public ProductsController(ProductService productService)
		{
			_productService = productService;
		}
		[HttpGet]
		public Task<IList<Product>> Get(CancellationToken cancellationToken)
		{
			return _productService.GetAll(cancellationToken);
		}

		[HttpPost]
		[Route(nameof(AddRandomProduct))]
		public async Task AddRandomProduct(CancellationToken cancellationToken)
		{
			await _productService.AddRandomProduct(cancellationToken);
		}

		[HttpPost]
		[Route(nameof(AddRandomProducts))]
		public async Task AddRandomProducts(int count,CancellationToken cancellationToken)
		{
			await _productService.AddRandomProducts(count,cancellationToken);
		}

	}
}
