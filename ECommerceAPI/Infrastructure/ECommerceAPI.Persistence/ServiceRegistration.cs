using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Services.Authentications;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Persistence.Context;
using ECommerceAPI.Persistence.Repositories;
using ECommerceAPI.Persistence.Services;
using Microsoft.AspNetCore.Identity;
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
			services.AddIdentity<AppUser, AppRole>(options =>
			{
				options.Password.RequiredLength = 3;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;

			}).AddEntityFrameworkStores<ECommerceAPIDbContext>()
			.AddDefaultTokenProviders(); //Identity mexeanizmi userinden reset token yaratmaga imkan yaradan servisdir-autservice GeneratePasswordResetTokenAsync

			services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
			services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
			services.AddScoped<IOrderReadRepository, OrderReadRepository>();
			services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
			services.AddScoped<IProductReadRepository, ProductReadRepository>();
			services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
			services.AddScoped<IFileReadRepository, FileReadRepository>();
			services.AddScoped<IFileWriteRepository, FileWriteRepository>();
			services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
			services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
			services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
			services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
			services.AddScoped<IBasketReadRepository, BasketReadRepository>();
			services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
			services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
			services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
			services.AddScoped<ICompletedOrderReadRepository, CompletedOrderReadRepository>();
			services.AddScoped<ICompletedOrderWriteRepository, CompletedOrderWriteRepository>();
			services.AddScoped<IMenuReadRepository, MenuReadRepository>();
			services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();
			services.AddScoped<IEndpointWriteRepository, EndpointWriteRepository>();
			services.AddScoped<IEndpointReadRepository, EndpointReadRepository>();
			//services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
			//services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

			services.AddScoped<IUserSevice, UserService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IExternalAuthentication, AuthService>();
			services.AddScoped<IInternalAuthentication, AuthService>();
			services.AddScoped<IBasketService, BasketService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<IAuthorizationEndpointService, AuthorizationEndpointService>();
			services.AddScoped<IProductService, ProductService>();
				
		}
	}
}
