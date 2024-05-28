using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Product.RemoveProduct;
using ECommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using ECommerceAPI.Application.Features.Commands.ProductImageFIle.RemoveProductImage;
using ECommerceAPI.Application.Features.Commands.ProductImageFIle.UploadProductImage;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Application.ViewModels.Products;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
		readonly IConfiguration configuration; //appsettinge catmaq ucun


		readonly IMediator _mediator;

		public ProductsController(IProductWriteRepository productWriteRepository,
									IProductReadRepository productReadRepository,
									IWebHostEnvironment webHostEnvironment,
									IInvoiceFileReadRepository invoiceFileReadRepository,
									IInvoiceFileWriteRepository invoiceFileWriteRepository,
									IProductImageFileReadRepository productImageFileReadRepository,
									IProductImageFileWriteRepository productImageFileWriteRepository,
									IFileWriteRepository fileWriteRepository,
									IFileReadRepository fileReadRepository,
									IStorageService storageService,
									IConfiguration configuration,
									IMediator mediator)
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
			this.configuration = configuration;
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
		{

			GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
			return Ok(response);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
		{
			 GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
		{


			CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);

			//return StatusCode((int)HttpStatusCode.Created);
			return Ok();
		}

		[HttpPut]

		public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest updateProductCommandRequest)
		{
			UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
			return Ok();
		}



		[HttpDelete("{Id}")]

		public async Task<IActionResult> Delete([FromRoute]RemoveProductCommandRequest removeProductCommandRequest)
		{
			RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);

			return Ok();
		}

		[HttpPost("[action]")]

		public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommand)
		{
			uploadProductImageCommand.Files = Request.Form.Files;
			UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommand);

			return Ok();

		}

		[HttpGet("[action]/{id}")]
		public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
		{
			List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
			return Ok(response);
		}

		[HttpDelete("[action]/{Id}")]

		public async Task<IActionResult> DeleteProductImage([FromRoute]RemoveProductImageCommandRequest removeProductImageCommandRequest,
															[FromQuery] string imageId)
		{
			removeProductImageCommandRequest.ImageId = imageId;
			RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);
			return Ok();
		}




	}
}
