using ECommerce.API.SignalR.HubServices;
using ECommerceAPI.Application.Abstractions.Hubs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.API.SignalR
{
	public static class ServiceRegistration
	{
		public static void AddSignalRServices(this IServiceCollection collection)
		{
			collection.AddTransient<IProductHubService, ProductHubService>();
			collection.AddSignalR();
		}
	}
}
