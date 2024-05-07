using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Infrastructure.Enums;
using ECommerceAPI.Infrastructure.Services;
using ECommerceAPI.Infrastructure.Services.Storage;
using ECommerceAPI.Infrastructure.Services.Storage.Azure;
using ECommerceAPI.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Infrastructure
{
	public static class ServiceRegistration
	{
		public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
		{

			serviceCollection.AddScoped<IStorageService, StorageService>();
		}

		//gelenin referans tipi class olmnalidir ve istorage-den miras almalidir
		// gelen class storage-den miral almali ve istorage-i implement etmeli
		public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
		{
			serviceCollection.AddScoped<IStorage, T>();
		}

		public static void AddStorage<T>(this IServiceCollection serviceCollection, StorageType storageType)
		{
			switch (storageType)
			{
				case StorageType.Local:
					serviceCollection.AddScoped<IStorage, LocalStorage>();
					break;
				case StorageType.Azure:
					serviceCollection.AddScoped<IStorage, AzureStorage>();
					break;
				case StorageType.AWM:
					break;
				default:
					serviceCollection.AddScoped<IStorage, LocalStorage>();
					break;
			}
		}
	}
}
