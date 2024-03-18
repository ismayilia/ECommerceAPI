using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
	public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
	{
		Task<bool> AddAsync(T model);
		Task<bool> AddRangeAsync(List<T> datas);
		bool Update(T model);
		bool Remove(T model);
		bool RemoveRange(List<T> datas);
		Task<bool> RemoveAsync(int? id);
		Task<int> SaveAsync();
	}
	
}
 