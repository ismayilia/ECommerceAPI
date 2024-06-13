using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.DTOs.Facebook;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.AppUser.FacebookLogin
{
	public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
	{
		readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
		readonly ITokenHandler _tokenHandler;
		readonly HttpClient _httpClient;

		public FacebookLoginCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager,
																			ITokenHandler tokenHandler,
																			IHttpClientFactory httpClientFactory)
		{
			_userManager = userManager;
			_tokenHandler = tokenHandler;
			_httpClient = httpClientFactory.CreateClient();
		}

		public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
		{

			//create access token
			string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id=3561591464091153&client_secret=d22024a67aa22c6b049e09d7e056cb66&grant_type=client_credentials");

			FacebookAccessToken facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessToken>(accessTokenResponse);



			string userAccessTokenValidation =
			await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={request.AuthToken}&access_token={facebookAccessTokenResponse.AccessToken}");

			FacebookUserAccessTokenValidation validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);

			if (validation.Data.IsValid)
			{
				string userInforResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={request.AuthToken}");

				FacebookUserInfoResponse userInfo =
					JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInforResponse);

				//xarici saytlardan login olan zaman db-da userlogins table save olunmasina ona gore UserLoginInfo ist edirik
				var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");

				Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

				bool result = user != null;


				if (user == null)
				{
					user = await _userManager.FindByEmailAsync(userInfo.Email);

					if (user == null)
					{
						user = new()
						{
							Id = Guid.NewGuid().ToString(),
							Email = userInfo.Email,
							UserName = userInfo.Email,
							NameSurname = userInfo.Name
						};

						var identityResult = await _userManager.CreateAsync(user);
						result = identityResult.Succeeded;
					}
				}

				if (result)
				{
					await _userManager.AddLoginAsync(user, info); //AspNetUserLogins table add
					Token token = _tokenHandler.CreateAccessToken(5);

					return new()
					{
						Token = token
					};
				}
			}
			throw new Exception("Invalid  external authentication.");

		}
	}
}
