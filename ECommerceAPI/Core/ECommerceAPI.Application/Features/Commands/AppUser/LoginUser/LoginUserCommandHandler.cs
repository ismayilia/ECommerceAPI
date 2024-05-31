﻿using ECommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
	public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
	{
		readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
		readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;

		public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, 
										SignInManager<Domain.Entities.Identity.AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
		{
			var user =  await _userManager.FindByNameAsync(request.UsernameOrEmail);
			if (user == null)
				user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

			if (user == null)
				throw new NotFoundUserException("User or Password is wrong....");


			SignInResult result =  await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

			if (result.Succeeded) // Authentication success!
			{
				// yetkileri vermek lazimdir---roles
			}
				
			return null;
		}
	}
}
