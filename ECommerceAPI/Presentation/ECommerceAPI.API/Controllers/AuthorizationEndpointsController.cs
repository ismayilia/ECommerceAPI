﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorizationEndpointsController : ControllerBase
	{

		[HttpPost]
		public async Task<IActionResult> AssignRoleEndpoint()
		{
			return Ok();
		}
	}
}
