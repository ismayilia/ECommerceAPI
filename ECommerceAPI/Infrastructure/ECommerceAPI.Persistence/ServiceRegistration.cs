﻿using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Persistence.Context;
using ECommerceAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ECommerceAPI.Persistence
{
	public static class ServiceRegistration
	{
		public static void AddPersistenceServices(this IServiceCollection services)
		{
			services.AddDbContext<ECommerceAPIDbContext>(options =>
		options.UseSqlServer(Configuration.ConnectionString));

			services.AddScoped<ICustomerReadRepository,CustomerReadRepository>();
			services.AddScoped<ICustomerWriteRepository,CustomerWriteRepository>();
			services.AddScoped<IOrderReadRepository,OrderReadRepository>();
			services.AddScoped<IOrderWriteRepository,OrderWriteRepository>();
			services.AddScoped<IProductReadRepository,ProductReadRepository>();
			services.AddScoped<IProductWriteRepository,ProductWriteRepository>();
			//services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
			//services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
		}
	}
}
