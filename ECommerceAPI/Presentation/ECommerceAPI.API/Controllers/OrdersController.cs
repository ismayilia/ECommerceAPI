using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Enums;
using ECommerceAPI.Application.Features.Commands.Order.CompleteOrder;
using ECommerceAPI.Application.Features.Commands.Order.CreateOrder;
using ECommerceAPI.Application.Features.Queries.Order.GetAllOrder;
using ECommerceAPI.Application.Features.Queries.Order.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "Admin")]

	public class OrdersController : ControllerBase
	{
		readonly IMediator _mediator;

		public OrdersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[AuthorizeDefiniton(Menu = AuthorizeDefinitonConstants.Orders, ActionType = Application.Enums.ActionType.Writing,
			Definiton = "Create Order")]
		public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
		{
			CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
			return Ok(response);
		}

		[HttpGet]
		[AuthorizeDefiniton(Menu = AuthorizeDefinitonConstants.Orders, ActionType = Application.Enums.ActionType.Reading,
			Definiton = "Get All Orders")]
		public async Task<IActionResult> GetAllOrders([FromQuery]GetAllOrdersQueryRequest getAllOrdersQueryRequest)
		{
			GetAllOrdersQueryResponse response = await _mediator.Send(getAllOrdersQueryRequest);
			return Ok(response);
		}

		[HttpGet("{Id}")]
		[AuthorizeDefiniton(Menu = AuthorizeDefinitonConstants.Orders, ActionType = Application.Enums.ActionType.Reading,
			Definiton = "Get Order By Id")]
		public async Task<IActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest getOrderByIdQueryRequest)
		{
			GetOrderByIdQueryResponse response = await _mediator.Send(getOrderByIdQueryRequest);
			return Ok(response);
		}

		[HttpGet("complete-order/{id}")]
		[AuthorizeDefiniton(Menu = AuthorizeDefinitonConstants.Orders, ActionType = Application.Enums.ActionType.Updating,
			Definiton = "Complete Order")]
		public async Task<IActionResult> CompleteOrder([FromRoute] CompleteOrderCommandRequest completeOrderCommandRequest)
		{
			CompleteOrderCommandResponse response = await _mediator.Send(completeOrderCommandRequest);
			return Ok(response);
		}
	}
}
