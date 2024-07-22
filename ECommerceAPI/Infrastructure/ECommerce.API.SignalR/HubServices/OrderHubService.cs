﻿using ECommerce.API.SignalR.Hubs;
using ECommerceAPI.Application.Abstractions.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.API.SignalR.HubServices
{
	public class OrderHubService : IOrderHubService
	{
		readonly IHubContext<ProductHub> _hubContext;

		public OrderHubService(IHubContext<ProductHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task OrderAddedMessageAsync(string message)
		=> await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.OrderAddedMessage, message);
	}
}
