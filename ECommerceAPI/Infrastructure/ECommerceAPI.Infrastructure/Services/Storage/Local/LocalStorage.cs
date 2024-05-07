using ECommerceAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Storage.Local
{

	public class LocalStorage :  Storage, ILocalStorage
	{

		readonly private IWebHostEnvironment _webHostEnvironment;
		public LocalStorage(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task DeleteAsync(string path, string fileName)

			=> File.Delete($"{path}\\{fileName}");


		public List<string> GetFiles(string path)
		{
			//path haqqinda melumatlari getirir
			DirectoryInfo directory = new(path);
			return directory.GetFiles().Select(f => f.Name).ToList();
		}


		// path-e gore yoxlayir
		public bool HasFile(string path, string fileName)
			=> File.Exists($"{path}\\{fileName}");

		public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
		{
			string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

			//file-lara gore yoxlayir
			if (!Directory.Exists(uploadPath))
				Directory.CreateDirectory(uploadPath);

			List<(string fileName, string path)> datas = new();
			List<bool> results = new();

			foreach (IFormFile file in files)
			{
				//string fileNewName = await FileRenameAsync(uploadPath, file.FileName);

				string fileNewName = await FileRenameAsync(path, file.Name, HasFile);

				await CopyFIleAsync($"{uploadPath}\\{fileNewName}", file);
				datas.Add((fileNewName, $"{path}\\{fileNewName}"));

			}

			//// hamisinin true olub olmamamgin yoxlayir
			//if (results.TrueForAll(r => r.Equals(true)))
			//	return datas;

			return datas;

			//todo egerki yuxaridaki if kecerli deyilse burda file-larin kompda(serverden) yuklenerken xeta aldigina dair
			//xebardaredici bir exception yaradilib geri dondermek lazimdir;
		}

		async Task<bool> CopyFIleAsync(string path, IFormFile file)
		{
			try
			{
				await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024
					, useAsync: false);

				await file.CopyToAsync(fileStream);
				await fileStream.FlushAsync();
				return true;
			}
			catch (Exception ex)
			{
				//todo log!

				throw ex;
			}
		}
	}
}
