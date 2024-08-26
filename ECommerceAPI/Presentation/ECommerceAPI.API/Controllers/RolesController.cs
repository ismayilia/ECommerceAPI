﻿using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.DTOs.Configuration;
using ECommerceAPI.Application.Enums;
using ECommerceAPI.Application.Features.Commands.Role.CreateRole;
using ECommerceAPI.Application.Features.Commands.Role.DeleteRole;
using ECommerceAPI.Application.Features.Commands.Role.UpdateRole;
using ECommerceAPI.Application.Features.Queries.Role.GetRoleById;
using ECommerceAPI.Application.Features.Queries.Role.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "Admin")]

	public class RolesController : ControllerBase
	{
		readonly IMediator _mediator;

		public RolesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Roles", Menu = "Roles")]
		public async Task<IActionResult> GetRoles([FromQuery] GetRolesQueryRequest getRolesQueryRequest)
		{
			GetRolesQueryResponse response = await _mediator.Send(getRolesQueryRequest);
			return Ok(response);
		}

		[HttpGet("{Id}")]
		[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Role By Id", Menu = "Roles")]

		public async Task<IActionResult> GetRoles([FromRoute] GetRoleByIdQueryRequest getRoleByIdQueryRequest)
		{
			GetRoleByIdQueryResponse response = await _mediator.Send(getRoleByIdQueryRequest);
			return Ok(response);
		}

		[HttpPost()]
		[AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Role", Menu = "Roles")]

		public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest createRoleCommandRequest)
		{
			CreateRoleCommandResponse response = await _mediator.Send(createRoleCommandRequest);
			return Ok(response);
		}

		[HttpPut("{Id}")]
		[AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Role", Menu = "Roles")]

		public async Task<IActionResult> UpdateRole([FromBody, FromRoute] UpdateRoleCommandRequest updateRoleCommandRequest)
		{
			UpdateRoleCommandResponse response = await _mediator.Send(updateRoleCommandRequest);
			return Ok(response);
		}

		[HttpDelete("{name}")]
		[AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Role", Menu = "Roles")]

		public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest deleteRoleCommandRequest)
		{
			DeleteRoleCommandResponse response = await _mediator.Send(deleteRoleCommandRequest);
			return Ok(response);
		}
	}
}
