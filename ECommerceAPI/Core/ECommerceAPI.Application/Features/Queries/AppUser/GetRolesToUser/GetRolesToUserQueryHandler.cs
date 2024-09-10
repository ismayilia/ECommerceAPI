using ECommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.AppUser.GetRolesToUser
{
	public class GetRolesToUserQueryHandler : IRequestHandler<GetRolesToUserQueryRequest, GetRolesToUserQueryResponse>
	{
		readonly IUserSevice _userSevice;

		public GetRolesToUserQueryHandler(IUserSevice userSevice)
		{
			_userSevice = userSevice;
		}

		public async Task<GetRolesToUserQueryResponse> Handle(GetRolesToUserQueryRequest request, CancellationToken cancellationToken)
		{
			var userRoles = await _userSevice.GetRolesToUserAsync(request.UserId);
			return new()
			{
				UserRoles = userRoles
			};
		}
	}
}
