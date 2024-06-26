using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
	public class UserService : IUserSevice
	{
		readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<CreatUserResponse> CreatAsync(CreatUser model)
		{
			IdentityResult result = await _userManager.CreateAsync(new()
			{
				Id = Guid.NewGuid().ToString(),
				UserName = model.Username,
				Email = model.Email,
				NameSurname = model.NameSurname
			}, model.Password);

			CreatUserResponse response = new() { Succeeded = result.Succeeded };

			if (result.Succeeded)
				response.Message = "User created success!";

			else
			{
				foreach (var error in result.Errors)
				{
					response.Message += $"{error.Code} - {error.Description}\n";
				}
			}

			return response;
		}

		public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate,
			int addOnAccessTokenDate)
		{

			if (user != null)
			{
				user.RefreshToken = refreshToken;
				user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
				await _userManager.UpdateAsync(user);
			}
			else
				throw new NotFoundUserException();

		}
	}
}
