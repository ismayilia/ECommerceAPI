﻿using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.Order;
using ECommerceAPI.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
	public class OrderService : IOrderService
	{
		readonly IOrderWriteRepository _orderWriteRepository;
		readonly IOrderReadRepository _orderReadRepository;

		public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
		{
			_orderWriteRepository = orderWriteRepository;
			_orderReadRepository = orderReadRepository;
		}
		public async Task CreateOrder(CreateOrder createOrder)
		{
			var orderCode = (new Random().NextDouble() * 1000).ToString();
			orderCode = orderCode.Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);

			await _orderWriteRepository.AddAsync(new()
			{
				Address = createOrder.Address,
				Id = Guid.Parse(createOrder.BasketId),
				Description = createOrder.Description,
				OrderCode = orderCode
			});

			await _orderWriteRepository.SaveAsync();
		}

		public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
		{
			var query = _orderReadRepository.Table.Include(o => o.Basket).ThenInclude(b => b.User)
				.Include(o => o.Basket)
				.ThenInclude(b => b.BasketItems)
				.ThenInclude(b => b.Product);

			var data = query.Skip(page * size).Take(size);
			//.Take((page * size)..size)

			return new()
			{
				TotalOrderCount = await query.CountAsync(),
				Orders = await data.Select(o => new
				{
					Id = o.Id,
					CreatedDate = o.CreatedDate,
					OrderCode = o.OrderCode,
					TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
					UserName = o.Basket.User.UserName
				}).ToListAsync()
			};
		}

		public async Task<SingleOrder> GetOrderByIdAsync(string id)
		{
			var data = await _orderReadRepository.Table
				.Include(o => o.Basket)
				.ThenInclude(b => b.BasketItems)
				.ThenInclude(bi => bi.Product)
				.FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

			return new()
			{
				Id = data.Id.ToString(),
				BasketItems = data.Basket.BasketItems.Select(bi=> new
				{
					bi.Product.Name,
					bi.Product.Price,
					bi.Quantity
				}),
				Address = data.Address,
				CreatedDate = data.CreatedDate,
				Description = data.Description,
				OrderCode = data.OrderCode
			};
		}
	}
}


