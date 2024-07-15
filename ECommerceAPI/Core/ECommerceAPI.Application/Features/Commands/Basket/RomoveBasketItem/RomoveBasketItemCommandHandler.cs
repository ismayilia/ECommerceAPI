using ECommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Basket.RomoveBasketItem
{
	public class RomoveBasketItemCommandHandler : IRequestHandler<RemoveBasketItemCommandRequest, RomoveBasketItemCommandResponse>
	{
		readonly IBasketService _basketService;

		public RomoveBasketItemCommandHandler(IBasketService basketService)
		{
			_basketService = basketService;
		}

		public async Task<RomoveBasketItemCommandResponse> Handle(RemoveBasketItemCommandRequest request, CancellationToken cancellationToken)
		{
			await _basketService.RemoveBasketItemAsync(request.BasketItemId);
			return new();
		}
	}
}
