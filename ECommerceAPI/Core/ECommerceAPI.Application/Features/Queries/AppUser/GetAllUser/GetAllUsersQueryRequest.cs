using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.AppUser.GetAllUser
{
	public class GetAllUsersQueryRequest : IRequest<GetAllUsersQueryResponse>
	{
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
