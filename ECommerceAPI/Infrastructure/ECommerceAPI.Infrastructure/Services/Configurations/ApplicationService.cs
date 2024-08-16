using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.DTOs.Configuration;
using ECommerceAPI.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace ECommerceAPI.Infrastructure.Services.Configurations
{
	public class ApplicationService : IApplicationService
	{
		public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
		{
			// butun sistsemdeki typelara catiriq classlar ve s. Type-da program.cs gonderirik ordaki typelari verir ancaq API
			Assembly assembly = Assembly.GetAssembly(type);
			var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));

			List<Menu> menus = new();

			if (controllers != null)
				foreach (var controller in controllers)
				{
					var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitonAttribute)));

					if (actions != null)
						foreach (var action in actions)
						{
							var attributes = action.GetCustomAttributes(true);

							if (attributes != null)
							{
								Menu menu = new();

								var authorizeDefinitonAttribute = attributes.FirstOrDefault(a => a.GetType() ==
								typeof(AuthorizeDefinitonAttribute)) as AuthorizeDefinitonAttribute;

								if (!menus.Any(m => m.Name == authorizeDefinitonAttribute.Menu))
								{
									menu = new() { Name = authorizeDefinitonAttribute.Menu };
									menus.Add(menu);
								}
								else
									menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitonAttribute.Menu);

								Application.DTOs.Configuration.Action _action = new()
								{
									ActionType = Enum.GetName(typeof(ActionType), authorizeDefinitonAttribute.ActionType),
									Definiton = authorizeDefinitonAttribute.Definiton,
								};

								var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute)))
																												as HttpMethodAttribute;
								if (httpAttribute != null)
									_action.HttpType = httpAttribute.HttpMethods.First();
								else
									_action.HttpType = HttpMethods.Get;

								_action.Code = $"{_action.HttpType}.{_action.ActionType}.{_action.Definiton.Replace(" ","")}";

								menu.Actions.Add(_action);

							}
						}
				}
			return menus;
		}
	}
}
