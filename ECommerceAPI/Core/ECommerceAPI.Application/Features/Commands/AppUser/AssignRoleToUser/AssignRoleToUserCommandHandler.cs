using ECommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.AppUser.AssignRoleToUser
{
	public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommandRequest, AssignRoleToUserCommandResponse>
	{
		readonly IUserSevice _userSevice;

		public AssignRoleToUserCommandHandler(IUserSevice userSevice)
		{
			_userSevice = userSevice;
		}

		public async Task<AssignRoleToUserCommandResponse> Handle(AssignRoleToUserCommandRequest request, CancellationToken cancellationToken)
		{
			await _userSevice.AssignRoleToUserAsync(request.UserId, request.Roles);
			return new();
		}
	}
}
