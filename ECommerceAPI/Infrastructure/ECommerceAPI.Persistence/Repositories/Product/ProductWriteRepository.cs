﻿using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories
{
	public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
	{
		public ProductWriteRepository(ECommerceAPIDbContext context) : base(context)
		{
		}
	}
}
