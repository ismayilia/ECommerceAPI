using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.Commands.Basket.AddItemToBasket;
using ECommerceAPI.Application.Features.Commands.Basket.RomoveBasketItem;
using ECommerceAPI.Application.Features.Commands.Basket.UpdateQuantity;
using ECommerceAPI.Application.Features.Commands.Product.RemoveProduct;
using ECommerceAPI.Application.Features.Queries.Basket.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "Admin")]
	public class BasketsController : ControllerBase
	{
		readonly IMediator _mediator;

		public BasketsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[AuthorizeDefiniton(Menu = AuthorizeDefinitonConstants.Baskets, ActionType = Application.Enums.ActionType.Reading,
			Definiton = "Get Basket Items")]
		public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemsQueryRequest getBasketItemsQueryRequest)
		{
			List<GetBasketItemsQueryResponse> response = await _mediator.Send(getBasketItemsQueryRequest);
			return Ok(response);
		}

		[HttpPost]
		[AuthorizeDefiniton(Menu = AuthorizeDefinitonConstants.Baskets, ActionType = Application.Enums.ActionType.Writing,
			Definiton = "Add Item To Basket")]
		public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
		{
			AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommandRequest);
			return Ok(response);
		}

		[HttpPut]
		[AuthorizeDefiniton(Menu = AuthorizeDefinitonConstants.Baskets, ActionType = Application.Enums.ActionType.Updating,
			Definiton = "Update Quantity")]
		public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest updateQuantityCommandRequest)
		{
			UpdateQuantityCommandResponse response = await _mediator.Send(updateQuantityCommandRequest);
			return Ok(response);
		}

		[HttpDelete("{BasketItemId}")]
		[AuthorizeDefiniton(Menu = AuthorizeDefinitonConstants.Baskets, ActionType = Application.Enums.ActionType.Deleting,
			Definiton = "Remove Basket Item")]
		public async Task<IActionResult> RemoveBasketItem([FromRoute] RemoveBasketItemCommandRequest removeBasketItemCommandRequest)
		{
			RemoveBasketItemCommandResponse response = await _mediator.Send(removeBasketItemCommandRequest);
			return Ok(response);
		}
	}
}
