using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		readonly private IProductWriteRepository _productWriteRepository;
		readonly private IProductReadRepository _productReadRepository;

		readonly private IOrderWriteRepository _orderWriteRepository;
		readonly private IOrderReadRepository _orderReadRepository;

		readonly private ICustomerWriteRepository _customerWriteRepository;
		public ProductsController(IProductWriteRepository productWriteRepository,
									IProductReadRepository productReadRepository,
									IOrderWriteRepository orderWriteRepository,
									ICustomerWriteRepository customerWriteRepository,
									IOrderReadRepository orderReadRepository)
		{
			_productReadRepository = productReadRepository;
			_productWriteRepository = productWriteRepository;
			_orderWriteRepository = orderWriteRepository;
			_customerWriteRepository = customerWriteRepository;
			_orderReadRepository = orderReadRepository;
		}

		[HttpGet]
		public async Task Get()
		{
			
			//Order order = await _orderReadRepository.GetByIdAsync(4);
			//order.Address = "Ehmedli";
			//await _orderWriteRepository.SaveAsync();
		}

		//[HttpGet("{id}")]

		//public async Task<IActionResult> Get(int id)
		//{
		//	Product product = await _productReadRepository.GetByIdAsync(id);
		//	return Ok(product); 
		//}
	}
}
