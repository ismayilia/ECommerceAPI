﻿using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Services.Authentications;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin
{
	public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
	{
		readonly IAuthService _authService;
		//readonly IExternalAuthentication _authService;---bur formadada yaza bilerik cunki serviceregistrationda map etmishik
		 

		public GoogleLoginCommandHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
		{
			var token = await _authService.GoogleLoginAsync(request.IdToken, 15);
			return new()
			{
				Token = token
			};
		}
	}
}
