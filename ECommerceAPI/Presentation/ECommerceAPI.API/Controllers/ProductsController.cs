using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		readonly private IProductWriteRepository _productWriteRepository;
		readonly private IProductReadRepository _productReadRepository;

		public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
		{
			_productReadRepository = productReadRepository;
			_productWriteRepository = productWriteRepository;
		}

		[HttpGet]
		public async Task Get()
		{
			await _productWriteRepository.AddRangeAsync(new()
			{
				new() { Name="Product 1", Price= 100, CreatedDate=DateTime.Now, Stock = 10},
				new() { Name="Product 2", Price= 200, CreatedDate=DateTime.Now, Stock = 20},
				new() { Name="Product 3", Price= 300, CreatedDate=DateTime.Now, Stock = 130},
			});
			var count = await _productWriteRepository.SaveAsync();

		}

		[HttpGet("{id}")]

		public async Task<IActionResult> Get(int id)
		{
			Product product = await _productReadRepository.GetByIdAsync(id);
			return Ok(product); 
		}
    }
}
