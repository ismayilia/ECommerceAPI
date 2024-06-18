using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstractions.Services
{
	public interface IUserSevice
	{
		Task<CreatUserResponse> CreatAsync(CreatUser model);
		Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessToken);
	}
}
