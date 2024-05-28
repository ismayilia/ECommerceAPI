using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFIle.UploadProductImage
{
	public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
	{
		readonly IStorageService _storageService;
		readonly IProductReadRepository _productReadRepository;
		readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

		public UploadProductImageCommandHandler(IProductReadRepository productReadRepository, IStorageService storageService,
												IProductImageFileWriteRepository productImageFileWriteRepository)
		{
			_productReadRepository = productReadRepository;
			_storageService = storageService;
			_productImageFileWriteRepository = productImageFileWriteRepository;
		}

		public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
		{
			List<(string fileName, string pathOrContainerName)> result
			 = await _storageService.UploadAsync("photo-images", request.Files);

			Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);


			//entityframework-un ustunlukleri
			//foreach (var d in result)
			//{
			//	product.ProductImageFiles.Add(new()
			//	{
			//		FileName = d.fileName,
			//		Path = d.pathOrContainerName,
			//		Storage = _storageService.StorageName,
			//		Products = new List<Product> { product }
			//	});
			//}

			await _productImageFileWriteRepository.AddRangeAsync(result.Select(d => new ProductImageFile
			{
				FileName = d.fileName,
				Path = d.pathOrContainerName,
				Storage = _storageService.StorageName,
				Products = new List<Domain.Entities.Product> { product }
			}).ToList());
			await _productImageFileWriteRepository.SaveAsync();

			return new();
		}
	}
}
