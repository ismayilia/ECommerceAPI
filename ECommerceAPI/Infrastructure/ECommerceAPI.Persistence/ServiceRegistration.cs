using ECommerceAPI.Application.Repositories;
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
		options.UseSqlServer(Configuration.ConnectionString), ServiceLifetime.Singleton);

			services.AddSingleton<ICustomerReadRepository,CustomerReadRepository>();
			services.AddSingleton<ICustomerWriteRepository,CustomerWriteRepository>();
			services.AddSingleton<IOrderReadRepository,OrderReadRepository>();
			services.AddSingleton<IOrderWriteRepository,OrderWriteRepository>();
			services.AddSingleton<IProductReadRepository,ProductReadRepository>();
			services.AddSingleton<IProductWriteRepository,ProductWriteRepository>();
			//services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
			//services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
		}
	}
}
