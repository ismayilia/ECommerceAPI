﻿using ECommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint
{
	public class GetRolesToEndpointQueryHandler : IRequestHandler<GetRolesToEndpointQueryRequest, GetRolesToEndpointQueryResponse>
	{
		readonly IAuthorizationEndpointService _authorizationEndpointService;

		public GetRolesToEndpointQueryHandler(IAuthorizationEndpointService authorizationEndpointService)
		{
			_authorizationEndpointService = authorizationEndpointService;
		}

		public async Task<GetRolesToEndpointQueryResponse> Handle(GetRolesToEndpointQueryRequest request, CancellationToken cancellationToken)
		{
			var datas  = await _authorizationEndpointService.GetRolesToEndpointAsync(request.Id);

			return new()
			{
				Roles = datas
			};
		}
	}
}
