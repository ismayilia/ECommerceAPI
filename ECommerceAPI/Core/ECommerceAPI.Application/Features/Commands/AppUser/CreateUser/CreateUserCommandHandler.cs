using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.AppUser.CreateUser
{

	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
	{
		readonly IUserSevice _userSevice;

		public CreateUserCommandHandler(IUserSevice userSevice)
		{
			_userSevice = userSevice;
		}

		public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
		{
			CreatUserResponse response = await _userSevice.CreatAsync(new()
			{
				Email = request.Email,
				NameSurname = request.NameSurname,
				Password = request.Password,
				ConfirmPassword = request.ConfirmPassword,
				Username = request.Username
			});
			
			return new()
			{
				Message = response.Message,
				Succeeded = response.Succeeded
			};
		}
	}
}
