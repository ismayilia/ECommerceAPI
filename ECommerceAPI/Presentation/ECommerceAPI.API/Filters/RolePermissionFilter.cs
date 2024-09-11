using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace ECommerceAPI.API.Filters
{
	public class RolePermissionFilter : IAsyncActionFilter
	{
		readonly IUserSevice _userSevice;

		public RolePermissionFilter(IUserSevice userSevice)
		{
			_userSevice = userSevice;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var name = context.HttpContext.User.Identity.Name;
			if (!string.IsNullOrEmpty(name) && name != "isiko")
			{
				// request hansi action-a gedirse onunla elaqeli melumatlari elde edilir
				var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

				var attribute = descriptor.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute))
																			  as AuthorizeDefinitionAttribute;

				var httpAttribute = descriptor.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute))
																			  as HttpMethodAttribute;

				var code = $"{(httpAttribute != null ? httpAttribute.HttpMethods.First() : HttpMethods.Get)}." +
					$"{attribute.ActionType}.{attribute.Definition.Replace(" ", "")}";

				var hasRole = await _userSevice.HasRolePermissionToEndpointAsync(name, code);

				if (!hasRole)
					context.Result = new UnauthorizedResult();
				else
					await next();
			}
			else
				await next();
		}
	}
}
