using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Context
{
	public class ECommerceAPIDbContext : DbContext
	{
		public ECommerceAPIDbContext(DbContextOptions<ECommerceAPIDbContext> options) : base(options) { }

		public DbSet<Product> Products { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderProduct> OrderProducts { get; set; }
		public DbSet<Domain.Entities.File> Files { get; set; }
		public DbSet<ProductImageFile> ProductImageFiles { get; set; }
		public DbSet<InvoiceFile> InvoiceFiles { get; set; }

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			//dbcontext-den gelir. Entityler uzerinden edilen deyishikliklere yada yeni eleva olunacaq datanin tutulmasina sebeb olan propertidir,
			// Update operasyonlarinda Track edilen datalari tutub elde etmeyimizi teshkil edir
			var datas = ChangeTracker.Entries<BaseEntity>();
			
			foreach (var data in datas)
			{
				switch (data.State)
				{
					case EntityState.Added:
						data.Entity.CreatedDate = DateTime.Now;
						break;
					case EntityState.Modified:
						data.Entity.UpdatedDate = DateTime.Now;
						break;
						
				}
			}
			
			return await base.SaveChangesAsync(cancellationToken);
		}

	}
}
