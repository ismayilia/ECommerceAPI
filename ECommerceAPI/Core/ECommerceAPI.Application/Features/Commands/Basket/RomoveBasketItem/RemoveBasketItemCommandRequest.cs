using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Basket.RomoveBasketItem
{
	public class RemoveBasketItemCommandRequest : IRequest<RomoveBasketItemCommandResponse>
	{
        public string BasketItemId { get; set; }
    }
}
