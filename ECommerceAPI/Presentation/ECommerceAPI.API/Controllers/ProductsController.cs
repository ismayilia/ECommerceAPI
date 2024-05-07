using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Application.ViewModels.Products;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		readonly private IProductWriteRepository _productWriteRepository;
		readonly private IProductReadRepository _productReadRepository;
		readonly private IWebHostEnvironment _webHostEnvironment;
		readonly private IFileWriteRepository _fileWriteRepository;
		readonly private IFileReadRepository _fileReadRepository;
		readonly private IProductImageFileWriteRepository _productImageFileWriteRepository;
		readonly private IProductImageFileReadRepository _productImageFileReadRepository;
		readonly private IInvoiceFileReadRepository _invoiceFileReadRepository;
		readonly private IInvoiceFileWriteRepository _invoiceFileWriteRepository;
		readonly private IStorageService _storageService;

		public ProductsController(IProductWriteRepository productWriteRepository,
									IProductReadRepository productReadRepository,
									IWebHostEnvironment webHostEnvironment,
									IInvoiceFileReadRepository invoiceFileReadRepository,
									IInvoiceFileWriteRepository invoiceFileWriteRepository,
									IProductImageFileReadRepository productImageFileReadRepository,
									IProductImageFileWriteRepository productImageFileWriteRepository,
									IFileWriteRepository fileWriteRepository,
									IFileReadRepository fileReadRepository,
									IStorageService storageService)
		{
			_productReadRepository = productReadRepository;
			_productWriteRepository = productWriteRepository;
			_webHostEnvironment = webHostEnvironment;
			_invoiceFileReadRepository = invoiceFileReadRepository;
			_invoiceFileWriteRepository = invoiceFileWriteRepository;
			_fileWriteRepository = fileWriteRepository;
			_fileReadRepository = fileReadRepository;
			_productImageFileReadRepository = productImageFileReadRepository;
			_productImageFileWriteRepository = productImageFileWriteRepository;
			_storageService = storageService;
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] Pagination pagination)
		{

			var totalCount = _productReadRepository.GetAll(false).Count();

			var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
			{
				p.Id,
				p.Name,
				p.Stock,
				p.Price,
				p.CreatedDate,
				p.UpdatedDate
			}).ToList();

			return Ok(new
			{
				totalCount,
				products
			});
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			return Ok(await _productReadRepository.GetByIdAsync(id, false));
		}

		[HttpPost]
		public async Task<IActionResult> Post(VM_Create_Product model)
		{


			await _productWriteRepository.AddAsync(new()
			{
				Name = model.Name,
				Price = model.Price,
				Stock = model.Stock
			});
			await _productWriteRepository.SaveAsync();
			return Ok();
		}

		[HttpPut]

		public async Task<IActionResult> Put(VM_Update_Product model)
		{
			Product product = await _productReadRepository.GetByIdAsync(model.Id);
			product.Stock = model.Stock;
			product.Name = model.Name;
			product.Price = model.Price;
			await _productWriteRepository.SaveAsync();
			return Ok();
		}

		[HttpDelete("{id}")]

		public async Task<IActionResult> Delete(string id)
		{
			await _productWriteRepository.RemoveAsync(id);
			await _productWriteRepository.SaveAsync();
			return Ok();
		}

		[HttpPost("[action]")]

		public async Task<IActionResult> Upload()
		{
			var datas = await _storageService.UploadAsync("files", Request.Form.Files);
			//var datas = await _storageService.UploadAsync("resource/files", Request.Form.Files);
			//var datas = await _fileService.UploadAsync("resource/files", Request.Form.Files);

			await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile
			{
				FileName = d.fileName,
				Path = d.pathOrContainerName,
				Storage = _storageService.StorageName
			}).ToList());
			await _productImageFileWriteRepository.SaveAsync();

			//await _invoiceFileWriteRepository.AddRangeAsync(datas.Select(d => new InvoiceFile
			//{
			//	FileName = d.fileName,
			//	Path = d.path,
			//	Price = new Random().Next()
			//}).ToList());
			//await _invoiceFileWriteRepository.SaveAsync();

			//await _fileWriteRepository.AddRangeAsync(datas.Select(d => new Domain.Entities.File
			//{
			//	FileName = d.fileName,
			//	Path = d.path,
			//}).ToList());
			//await _fileWriteRepository.SaveAsync();

			//var d1 = _fileReadRepository.GetAll(false);
			//var d2 = _invoiceFileReadRepository.GetAll(false);
			//var d3 = _productImageFileReadRepository.GetAll(false);

			return Ok();

		}


	}
}
