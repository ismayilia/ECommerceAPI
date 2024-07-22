﻿using ECommerce.API.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.API.SignalR
{
	public static class HubRegistration
	{
		public static void MapHubs(this WebApplication webApplication)
		{
			webApplication.MapHub<ProductHub>("/products-hub");
			webApplication.MapHub<OrderHub>("/order-hub");
		}
	}
}
