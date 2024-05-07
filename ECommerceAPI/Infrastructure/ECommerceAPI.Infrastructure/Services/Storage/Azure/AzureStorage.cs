using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ECommerceAPI.Application.Abstractions.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Storage.Azure
{
	public class AzureStorage : Storage, IAzureStorage
	{
		// azure storage accounta connect ucuncundur
		readonly BlobServiceClient _blobServiceClient;
		// hemin accountda container uzerinde fayl ishlerimini etmemiz ucundur
		BlobContainerClient _blobContainerClient;

		public AzureStorage(IConfiguration configuration)
		{
			_blobServiceClient = new(configuration["Storage:Azure"]);
		}


		public async Task DeleteAsync(string containerName, string fileName)
		{
			_blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
			BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
			await blobClient.DeleteAsync();
		}

		public List<string> GetFiles(string containerName)
		{
			_blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
			// butun bloblarin adlarini cekirik
			return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
		}

		public bool HasFile(string containerName, string fileName)
		{
			_blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
			return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
		}

		public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName,
																						   IFormFileCollection files)
		{
			//blogcontainercliente qarlishig objecti aliriq
			_blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
			await _blobContainerClient.CreateIfNotExistsAsync();
			// Erishim icazesi
			await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

			List<(string fileName, string pathOrContainerName)> datas = new();

			foreach (IFormFile file in files)
			{
				string fileNewName = await FileRenameAsync(containerName, file.Name, HasFile);
				//upload edeceyimiz file gosteririk ve ya gotururuk 
				BlobClient blobClient = _blobContainerClient.GetBlobClient(file.Name);
				//iformfile tipinden olan butun fayllari "stream"-e cevire bilirik bunun ucun "openreadstream" methodudu!
				await blobClient.UploadAsync(file.OpenReadStream());
				datas.Add((fileNewName, $"{containerName}/{fileNewName}"));
			}

			return datas;
		}
	}
}
