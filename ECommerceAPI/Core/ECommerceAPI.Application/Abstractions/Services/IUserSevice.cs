using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstractions.Services
{
	public interface IUserSevice
	{
		Task<CreatUserResponse> CreatAsync(CreatUser model);
		Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessToken);
		Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
		Task<List<ListUser>> GetAllUsersAsync(int page, int size);
		int TotalUsersCount { get; }
	}
}
