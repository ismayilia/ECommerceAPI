﻿using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Enums;
using ECommerceAPI.Application.Features.Commands.AppUser.AssignRoleToUser;
using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPI.Application.Features.Commands.AppUser.FacebookLogin;
using ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using ECommerceAPI.Application.Features.Commands.AppUser.UpdatePassword;
using ECommerceAPI.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;
using ECommerceAPI.Application.Features.Queries.AppUser.GetAllUser;
using ECommerceAPI.Application.Features.Queries.AppUser.GetRolesToUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "Admin")]

	public class UsersController : ControllerBase
	{
		readonly IMediator _mediator;
		readonly IMailService _mailService;

		public UsersController(IMediator mediator, IMailService mailService)
		{
			_mediator = mediator;
			_mailService = mailService;
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
		{

			CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
			return Ok(response);

		}

		//[HttpGet]
		//public async Task<IActionResult> ExampleMailTest()
		//{
		//	await _mailService.SendMailAsync("ismail8083@gmail.com", "test mail", "<strong>this is test mail.</strong>");
		//	return Ok();
		//}

		[HttpPost("update-password")]
		public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
		{

			UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
			return Ok(response);

		}

		[HttpGet]
		[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Users", Menu = "Users")]
		public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest getAllUsersQueryRequest)
		{

			GetAllUsersQueryResponse response = await _mediator.Send(getAllUsersQueryRequest);
			return Ok(response);

		}

		[HttpGet("get-roles-to-user/{UserId}")]
		[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Roles To Users", Menu = "Users")]
		public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserQueryRequest getRolesToUserQueryRequest)
		{
			GetRolesToUserQueryResponse response = await _mediator.Send(getRolesToUserQueryRequest);
			return Ok(response);

		}

		[HttpPost("assign-role-to-user")]
		[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Assign Role To User", Menu = "Users")]
		public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
		{
			AssignRoleToUserCommandResponse response = await _mediator.Send(assignRoleToUserCommandRequest);
			return Ok(response);
		}

	}
}
