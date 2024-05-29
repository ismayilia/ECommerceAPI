﻿using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFIle.RemoveProductImage
{
	public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
	{
		readonly IProductReadRepository _productReadRepository;
		readonly IProductWriteRepository _productWriteRepository;

		public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
		{
			_productReadRepository = productReadRepository;
			_productWriteRepository = productWriteRepository;
		}

		public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
		{
			Domain.Entities.Product product = await _productReadRepository.Table.Include(m => m.ProductImageFiles)
											  .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

			ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));
			if(productImageFile != null)
				product?.ProductImageFiles.Remove(productImageFile);

			await _productWriteRepository.SaveAsync();
			return new();
		}
	}
}