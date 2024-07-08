using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.ViewModels.Baskets;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
	public class BasketService : IBasketService
	{
		readonly IHttpContextAccessor _httpContextAccessor;
		readonly UserManager<AppUser> _userManager;

		public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
		{
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}

		private async Task<AppUser> ContextUser()
		{
			var username = _httpContextAccessor.HttpContext.User.Identity.Name;
			if (!string.IsNullOrEmpty(username))
			{
				AppUser user = await _userManager.Users.Include(m => m.Baskets)
						.FirstOrDefaultAsync(u => u.UserName == username);
			}
		}

		public Task AddItemToBasketAsync(VM_Create_BasketItem basketItem)
		{
			var username = _httpContextAccessor.HttpContext.User.Identity.Name;
			if (!string.IsNullOrEmpty(username))
			{

			}
		}

		public Task<List<BasketItem>> GetBasketItemAsync()
		{
			throw new NotImplementedException();
		}

		public Task RemoveBasketItemAsync(string basketItemId)
		{
			throw new NotImplementedException();
		}

		public Task UpdateQuantityAsync(VM_Update_BasketItem basketItem)
		{
			throw new NotImplementedException();
		}
	}
}
