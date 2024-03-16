using ECommerceAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceAPIDbContext>
	{


		//With Powershell migration
		public ECommerceAPIDbContext CreateDbContext(string[] args)
		{


			DbContextOptionsBuilder<ECommerceAPIDbContext> dbContextOptionsBuilder = new();
			dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);
			return new(dbContextOptionsBuilder.Options);
		}
	}
}
