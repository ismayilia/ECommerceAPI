﻿using ECommerceAPI.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;
using ECommerceAPI.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorizationEndpointsController : ControllerBase
	{
		readonly IMediator _mediator;

		public AuthorizationEndpointsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetRolesToEndpoint
			([FromRoute] GetRolesToEndpointQueryRequest getRolesToEndpointQueryRequest)
		{
			GetRolesToEndpointQueryResponse response = await _mediator.Send(getRolesToEndpointQueryRequest);
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> AssignRoleEndpoint
			(AssignRoleEndpointCommandRequest assignRoleEndpointCommandRequest)
		{
			assignRoleEndpointCommandRequest.Type = typeof(Program);
			AssignRoleEndpointCommandResponse response = await _mediator.Send(assignRoleEndpointCommandRequest);
			return Ok(response);
		}
	}
}

