﻿using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.DTOs.Facebook;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using ECommerceAPI.Application.Helpers;
using ECommerceAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
	public class AuthService : IAuthService
	{
		readonly HttpClient _httpClient;
		readonly IConfiguration _configuration;
		readonly UserManager<AppUser> _userManager;
		readonly ITokenHandler _tokenHandler;
		readonly SignInManager<AppUser> _signInManager;
		readonly IUserSevice _userService;
		readonly IMailService _mailService;

		public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration,
						   UserManager<AppUser> userManager, ITokenHandler tokenHandler,
						   SignInManager<AppUser> signInManager, IUserSevice userService, IMailService mailService)
		{
			_httpClient = httpClientFactory.CreateClient();
			_configuration = configuration;
			_userManager = userManager;
			_tokenHandler = tokenHandler;
			_signInManager = signInManager;
			_userService = userService;
			_mailService = mailService;
		}

		async Task<Token> CreateUserExternalAsync(AppUser user, string email, string name,
												  UserLoginInfo info, int accessTokenLifeTime)
		{
			bool result = user != null;

			if (user == null)
			{
				user = await _userManager.FindByEmailAsync(email);

				if (user == null)
				{
					user = new()
					{
						Id = Guid.NewGuid().ToString(),
						Email = email,
						UserName = email,
						NameSurname = name
					};

					var identityResult = await _userManager.CreateAsync(user);
					result = identityResult.Succeeded;
				}
			}

			if (result)
			{
				await _userManager.AddLoginAsync(user, info); //AspNetUserLogins table add
				Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);

				await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);

				return token;
			}

			throw new Exception("Invalid  external authentication.");

		}


		public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
		{
			//create access token
			string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");

			FacebookAccessTokenResponse facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);
			string userAccessTokenValidation =
			await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse.AccessToken}");

			FacebookUserAccessTokenValidation validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);

			if (validation?.Data.IsValid != null)
			{
				string userInforResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

				FacebookUserInfoResponse userInfo =
					JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInforResponse);

				//xarici saytlardan login olan zaman db-da userlogins table save olunmasina ona gore UserLoginInfo ist edirik
				var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");

				Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

				return await CreateUserExternalAsync(user, userInfo.Email, userInfo.Name, info, accessTokenLifeTime);

			}
			throw new Exception("Invalid  external authentication.");

		}

		public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
		{
			var settings = new GoogleJsonWebSignature.ValidationSettings()
			{
				Audience = new List<string> { _configuration["Google:Client"] }
			};

			var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

			//xarici saytlardan login olan zaman db-da userlogins table save olunmasina ona gore UserLoginInfo ist edirik
			var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

			Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

			return await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTime);

		}

		public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
		{
			var user = await _userManager.FindByNameAsync(usernameOrEmail);
			if (user == null)
				user = await _userManager.FindByEmailAsync(usernameOrEmail);

			if (user == null)
				throw new NotFoundUserException();


			SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

			if (result.Succeeded) // Authentication success!
			{
				Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);

				await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);

				return token;
			}
			//return new LoginUserErrorComandResponse()
			//{
			//	Message = "Login or Password is wrong!!"
			//};

			throw new AuthenticationErrorException();
		}

		public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
		{
			AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
			if (user != null && user.RefreshTokenEndDate > DateTime.Now)
			{
				Token token = _tokenHandler.CreateAccessToken(15, user);
				await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 300);
				return token;
			}
			else
				throw new NotFoundUserException();
		}

		public async Task PasswordResetAsync(string email)
		{
			AppUser user = await _userManager.FindByEmailAsync(email);
			if (user != null)
			{
				string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
				//encoding
				resetToken = resetToken.UrlEncode();
				await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
			}
		}

		public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
		{
			AppUser user = await _userManager.FindByIdAsync(userId);

			if (user != null)
			{
				//decoding
				resetToken = resetToken.UrlDecode();

				return await _userManager.VerifyUserTokenAsync
					(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);

			}
			return false;

		}
	}
}
