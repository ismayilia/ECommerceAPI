﻿using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.AppUser.UpdatePassword
{
	public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
	{
		readonly IUserSevice _userSevice;

		public UpdatePasswordCommandHandler(IUserSevice userSevice)
		{
			_userSevice = userSevice;
		}

		public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
		{
			if (!request.Password.Equals(request.PasswordConfirm))
				throw new PasswordChangeFailedException("Please confirm password!");


			await _userSevice.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);
			return new();
		}
	}
}
