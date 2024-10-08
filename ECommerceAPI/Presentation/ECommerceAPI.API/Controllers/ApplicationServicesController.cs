﻿using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.DTOs.Configuration;
using ECommerceAPI.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "Admin")]

	public class ApplicationServicesController : ControllerBase
	{
		readonly IApplicationService _applicationService;

		public ApplicationServicesController(IApplicationService applicationService)
		{
			_applicationService = applicationService;
		}

		[HttpGet]
		[AuthorizeDefinition(Menu = "Application Services", ActionType = Application.Enums.ActionType.Reading,
			Definition = "Get Authorize Definition Endpoints")]
		public IActionResult GetAuthorizeDefinitionEndpoints() 
		{
			// controllere catmag ucun
			var datas = _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
			return Ok(datas);
		}
	}
}
