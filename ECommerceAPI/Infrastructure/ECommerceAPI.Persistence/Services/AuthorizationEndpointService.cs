using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
	public class AuthorizationEndpointService : IAuthorizationEndpointService
	{
		readonly IApplicationService _applicationService;
		readonly IEndpointReadRepository _endpointReadRepository;

		public AuthorizationEndpointService(IApplicationService applicationService, IEndpointReadRepository endpointReadRepository)
		{
			_applicationService = applicationService;
			_endpointReadRepository = endpointReadRepository;
		}

		public async Task AssignRoleEndpointAsync(string[] roles, string code)
		{
			Endpoint endpoint = await _endpointReadRepository.GetSingleAsync(e => e.Code == code);
			if (endpoint == null)
			{

			}
		}
	}
}
