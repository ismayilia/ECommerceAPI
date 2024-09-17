using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Enums;
using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Product.RemoveProduct;
using ECommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using ECommerceAPI.Application.Features.Commands.Product.UpdateStockQrCodeToProduct;
using ECommerceAPI.Application.Features.Commands.ProductImageFIle.ChangeShowcaseImage;
using ECommerceAPI.Application.Features.Commands.ProductImageFIle.RemoveProductImage;
using ECommerceAPI.Application.Features.Commands.ProductImageFIle.UploadProductImage;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using ECommerceAPI.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{

	[Route("api/[controller]")]
	[ApiController]

	public class ProductsController : ControllerBase	
	{
		
		readonly IConfiguration configuration; //appsettinge catmaq ucun
		readonly IMediator _mediator;
		readonly ILogger<ProductsController> _logger;
		readonly IProductService _productService;

		public ProductsController(
									IConfiguration configuration,
									IMediator mediator,
									ILogger<ProductsController> logger,
									IProductService productService)
		{

			this.configuration = configuration;
			_mediator = mediator;
			_logger = logger;
			_productService = productService;
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
		{
			GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
			return Ok(response);
		}

		[HttpGet("qrcode/{productId}")]
		public async Task<IActionResult> GetQRCodeToProduct([FromRoute] string productId)
		{
			var data  = await _productService.QRCodeToProductAsync(productId);
			return File(data, "image/png");
		}

		[HttpPut("qrcode")]
		public async Task<IActionResult> UpdateStockQrCodeToProduct([FromBody] UpdateStockQrCodeToProductCommandRequest 
																	updateStockQrCodeToProductCommandRequest)
		{
			UpdateStockQrCodeToProductCommandResponse response = 
				await _mediator.Send(updateStockQrCodeToProductCommandRequest);
			return Ok(response);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
		{
			 GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
			return Ok(response);
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = Application.Enums.ActionType.Writing,
			Definition = "Create Product")]
		public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
		{


			CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);

			//return StatusCode((int)HttpStatusCode.Created);
			return Ok();
		}

		[HttpPut]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = Application.Enums.ActionType.Updating,
			Definition = "Update Product")]
		public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest updateProductCommandRequest)
		{
			UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
			return Ok();
		}



		[HttpDelete("{Id}")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = Application.Enums.ActionType.Deleting,
			Definition = "Delete Product")]
		public async Task<IActionResult> Delete([FromRoute]RemoveProductCommandRequest removeProductCommandRequest)
		{
			RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);

			return Ok();
		}

		[HttpPost("[action]")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = Application.Enums.ActionType.Writing,
			Definition = "Upload Product File")]
		public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommand)
		{
			uploadProductImageCommand.Files = Request.Form.Files;
			UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommand);

			return Ok();

		}

		[HttpGet("[action]/{id}")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = Application.Enums.ActionType.Reading,
			Definition = "Get Product Images")]
		public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
		{
			List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
			return Ok(response);
		}

		[HttpDelete("[action]/{Id}")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = Application.Enums.ActionType.Deleting,
			Definition = "Delete Product Image")]
		public async Task<IActionResult> DeleteProductImage([FromRoute]RemoveProductImageCommandRequest removeProductImageCommandRequest,
															[FromQuery] string imageId)
		{
			removeProductImageCommandRequest.ImageId = imageId;
			RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);
			return Ok();
		}

		[HttpGet("[action]")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = Application.Enums.ActionType.Updating,
			Definition = "Change Show Case Image")]
		public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommandRequest changeShowcaseImageCommandRequest)
		{
			ChangeShowcaseImageCommandResponse response = await _mediator.Send(changeShowcaseImageCommandRequest);

			return Ok();
		}







	}
}
