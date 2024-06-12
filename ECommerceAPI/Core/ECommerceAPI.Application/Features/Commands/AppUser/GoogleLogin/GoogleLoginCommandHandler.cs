﻿using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
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
		readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
		readonly ITokenHandler _tokenHandler;

		public GoogleLoginCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager,
																		ITokenHandler tokenHandler)
		{
			_userManager = userManager;
			_tokenHandler = tokenHandler;
		}

		public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
		{
			var settings = new GoogleJsonWebSignature.ValidationSettings()
			{
				Audience = new List<string> { "1042823587560-82j7vcpbfv41pop75s3kjonegi9dmkfh.apps.googleusercontent.com" }
			};

			var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

			//xarici saytlardan login olan zaman db-da userlogins table save olunmasina ona gore UserLoginInfo ist edirik
			var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);

			Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

			bool result = user != null;
			
			
			if (user == null)
			{
				user = await _userManager.FindByEmailAsync(payload.Email);

				if (user == null)
				{
					user = new()
					{
						Id = Guid.NewGuid().ToString(),
						Email = payload.Email,
						UserName = payload.Email,
						NameSurname = payload.Name
					};

					var identityResult = await _userManager.CreateAsync(user);
					result = identityResult.Succeeded;
				}
			}

			if (result)
			{
				await _userManager.AddLoginAsync(user, info); //AspNetUserLogins table add
			}
			else
				throw new Exception("Invalid  external authentication.");

			Token token = _tokenHandler.CreateAccessToken(5);

			return new()
			{
				Token = token
			};
		}
	}
}
