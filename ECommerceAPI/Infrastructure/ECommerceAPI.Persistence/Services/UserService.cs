using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPI.Application.Helpers;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
		readonly IEndpointReadRepository _endpointReadRepository;
		public UserService(UserManager<AppUser> userManager, IEndpointReadRepository endpointReadRepository)
		{
			_userManager = userManager;
			_endpointReadRepository = endpointReadRepository;
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

		public async Task<List<ListUser>> GetAllUsersAsync(int page, int size)
		{
			var users = await _userManager.Users
				.Skip(page * size)
				.Take(size)
				.ToListAsync();

			return users.Select(user => new ListUser
			{
				Id = user.Id,
				Email = user.Email,
				NameSurname = user.NameSurname,
				TwoFactorEnabled = user.TwoFactorEnabled,
				UserName = user.UserName
			}).ToList();
		}

		public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
		{
			AppUser user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				resetToken = resetToken.UrlDecode();
				IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
				if (result.Succeeded)
					await _userManager.UpdateSecurityStampAsync(user);//securitystamp deyeri deyishdirilecek
				else
					throw new PasswordChangeFailedException();
			}
		}

		public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate,
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


		public int TotalUsersCount => _userManager.Users.Count();

		public async Task AssignRoleToUserAsync(string userId, string[] roles)
		{
			AppUser user = await _userManager.FindByIdAsync(userId);

			if (user != null)
			{
				var userRoles = await _userManager.GetRolesAsync(user);
				await _userManager.RemoveFromRolesAsync(user, userRoles);
				await _userManager.AddToRolesAsync(user, roles);
			}
		}

		public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
		{
			AppUser user = await _userManager.FindByIdAsync(userIdOrName);
			if (user == null)
				user = await _userManager.FindByNameAsync(userIdOrName);
			if (user != null)
			{
				var userRoles = await _userManager.GetRolesAsync(user);
				return userRoles.ToArray();
			}
			return new string[] { };

		}

		public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
		{
			var userRoles = await GetRolesToUserAsync(name);

			if (!userRoles.Any())
				return false;

			Endpoint endpoint = await _endpointReadRepository.Table
															 .Include(m => m.Roles)
															 .FirstOrDefaultAsync(e => e.Code == code);
			if (endpoint == null)
				return false;

			var hasRole = false;
			var endpointsRoles = endpoint.Roles.Select(r => r.Name);

			// first version
			//foreach (var userRole in userRoles)
			//{
			//	if (!hasRole)
			//	{
			//		foreach (var endpointsRole in endpointsRoles)
			//			if (userRole == endpointsRole)
			//			{
			//				hasRole = true;
			//				break;
			//			}
			//	}
			//	else
			//		break;
			//}

			//return hasRole;


			// second version

			foreach (var userRole in userRoles)
			{
				foreach (var endpointsRole in endpointsRoles)
					if (userRole == endpointsRole)
						return true;
			}

			return false;
		}
	}
}
