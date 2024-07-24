﻿using ECommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Order.GetAllOrder
{
	public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, List<GetAllOrdersQueryResponse>>
	{
		readonly IOrderService _orderService;

		public GetAllOrdersQueryHandler(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public async Task<List<GetAllOrdersQueryResponse>> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
		{
			var data = await _orderService.GetAllOrdersAsync(request.Page, request.Size);
			return data.Select(o => new GetAllOrdersQueryResponse
			{
				CreatedDate = o.CreatedDate,
				OrderCode = o.OrderCode,
				TotalPrice = o.TotalPrice,
				UserName = o.UserName
			}).ToList();
		}
	}
}
