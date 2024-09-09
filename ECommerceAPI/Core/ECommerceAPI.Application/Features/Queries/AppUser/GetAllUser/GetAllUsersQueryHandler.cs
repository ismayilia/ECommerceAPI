using ECommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.AppUser.GetAllUser
{
	public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResponse>
	{
		readonly IUserSevice _userSevice;

		public GetAllUsersQueryHandler(IUserSevice userSevice)
		{
			_userSevice = userSevice;
		}

		public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
		{
			var users = await _userSevice.GetAllUsersAsync(request.Page, request.Size);

			return new()
			{
				Users = users,
				TotalUsersCount = _userSevice.TotalUsersCount
			};
		}
	}
}
